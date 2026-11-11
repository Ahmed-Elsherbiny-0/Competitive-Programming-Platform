using Application.Comman.Errors;
using Application.Interfaces;
using Domain.Comman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.ForgetPassword
{
public record  ForgetPasswordCommand(string Email):IRequest<Result>;
    public class ForgetPasswordCommandHandler(IIdentityService _identityService,IAuthService _authService ) : IRequestHandler<ForgetPasswordCommand, Result>
    {
        public async  Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var email=request.Email;
            if (await _identityService.FindByEmailAsync(email) is not { } user)
                    return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            var code = await _identityService.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //_logger.LogInformation("Reset code: {code}", code);

            await _authService.SendResetPasswordEmail(user, code);

            return Result.Success();
        }
    }
}
