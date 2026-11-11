using Application.Comman.Errors;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Comman;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.RevokeRefreshToken
{
    public  record RevokeRefreshTokenCommand(string token, string refreshToken):IRequest<Result>;
    public class RevokeRefreshTokenCommandHandler(IJwtProvider _jwtProvider,
        IApplicationDbContext _context,IIdentityService _identityService) 
        : IRequestHandler<RevokeRefreshTokenCommand, Result>
    {
        public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.token;
            var refreshToken = request.refreshToken;
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var user = await _context.Users.Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokeOn = DateTime.UtcNow;

            _context.Users.Update(user);
           await  _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
