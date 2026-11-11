using Application.Comman.Errors;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Comman;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.CreateLogin
{
   public record CreateLoginCommand(string Email,string Password):IRequest<Result<AuthResponseDto>>;
    public class CreateLoginCommandHandler (
        IIdentityService _identityService,
        IHttpContextAccessor _httpContextAccessor,
        IApplicationDbContext _context ,
        IJwtProvider _jwtProvider,
        IAuthService _authService

        )
        : IRequestHandler<CreateLoginCommand, Result<AuthResponseDto>>
    {
    private readonly int _refreshTokenExpiryDays = 14;
        public async Task<Result<AuthResponseDto>> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
        
            var user = await _identityService.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result.Failure<AuthResponseDto>(UserErrors.InvalidCredentials);
            }

            var result = await _identityService.CheckPasswordSignInAsync(user,request.Password,true);
            if (result.Succeeded)
            {
                ///to do Roles
                var (token, expire) = _jwtProvider.GenerateToken(user, [], []);
                var refreshToken = _authService.GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });
                _context.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                //var userWithPhoto= await _context.Users.Include(x => x.Photos).Include(x=>x.Worker).FirstOrDefaultAsync(x => x.Email == email);
                var photo = _context.Photos.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
                var response = new AuthResponseDto(user.Id, user.Email!, user.FirstName, photo!.Url, photo!.PublicId, user.LastName, token, expire, refreshToken, refreshTokenExpiration);
                var accessTokenCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddSeconds(expire)
                };
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("access_token", token, accessTokenCookieOptions);

                return Result.Success(response);
            }

            var error = result.IsLockedOut ? UserErrors.LockedUser : result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials;
            return Result.Failure<AuthResponseDto>(error);
        }
    }
}
