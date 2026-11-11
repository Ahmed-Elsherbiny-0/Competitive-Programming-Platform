using Application.Comman.Errors;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Comman;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.ResendConfirmationEmail
{
    public record ResendConfirmationEmailCommand (ResendConfirmationEmailRequestDto request ):IRequest<Result>;
    public class ResendConfirmationEmailCommandHandler(IAuthService _authService,
        IIdentityService _identityService) : IRequestHandler<ResendConfirmationEmailCommand, Result>
    {
        public async Task<Result> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            if (await _identityService.FindByEmailAsync(request.request.Email) is not { } user)
                    return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _identityService.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //_logger.LogInformation("Confirmation code: {code}", code);
         await _authService.SendConfirmationEmail(user, code);

            return Result.Success();
        }
    }
}
