using Application.Comman;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Comman;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.CreateRefreshToken
{
    public record   CreateRefreshTokenCommand(string token, string refreshToken, CancellationToken cancellationToken) :IRequest<Result<AuthResponseDto>>;
    public class CreateRefreshTokenCommandHandler(IJwtProvider _jwtProvider,
        IApplicationDbContext _context,
        IAuthService _authService ,
        IHttpContextAccessor _httpContextAccessor) : IRequestHandler<CreateRefreshTokenCommand, Result<AuthResponseDto>>
    {
        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<AuthResponseDto>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
           var  token = request.token;
            var refreshToken = request.refreshToken;
            var res = _jwtProvider.ValidateToken(token);
            if (res == null)
            {
                return Result.Failure<AuthResponseDto>(UserErrors.InvalidJwtToken);
            }
            var user = await _context.Users.Include(x => x.Photo).Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.Id == res);
            if (user == null)
            {
                return Result.Failure<AuthResponseDto>(UserErrors.InvalidJwtToken);
            }
            if (user.LockoutEnd > DateTime.UtcNow)
            {
                return Result.Failure<AuthResponseDto>(UserErrors.LockedUser);
            }

            var oldRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken && x.IsActive);
            if (oldRefreshToken == null)
            {
                return Result.Failure<AuthResponseDto>(UserErrors.InvalidRefreshToken);
            }

            oldRefreshToken.RevokeOn = DateTime.UtcNow;
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            var newRefreshToken = _authService.GenerateRefreshToken();
            var addRefreshtoken = new RefreshToken
            {
                ExpiresOn = refreshTokenExpiration,
                Token = newRefreshToken,
            };
            //to do
            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, [], []);

            user.RefreshTokens.Add(addRefreshtoken);
            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            var photo = user.Photo;
            var response = new AuthResponseDto(user.Id, user.Email!, user.FirstName, photo?.Url, photo?.PublicId, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

            var accessTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddSeconds(expiresIn)
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("access_token", token, accessTokenCookieOptions);
            return Result.Success(response);
        }
    }
}
