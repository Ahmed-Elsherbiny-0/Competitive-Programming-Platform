using Application.Comman;
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

namespace Application.Auth.Commands.ResetPassword
{
    public  record  ResetPasswordCommand(ResetPasswordRequestDto request):IRequest<Result>;
    public class ResetPasswordCommandHandler(IIdentityService _identityService) : IRequestHandler<ResetPasswordCommand, Result>
    {
        public async  Task<Result> Handle(ResetPasswordCommand command , CancellationToken cancellationToken)
        {
            var request = command.request;
            var user = await _identityService.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _identityService.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(  new IdentityError
                        {
                            Code = "InvalidToken",
                            Description = "Invalid token."
                        });
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));

        }
    }
}
