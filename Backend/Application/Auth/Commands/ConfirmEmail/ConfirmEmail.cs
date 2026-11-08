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

namespace Application.Auth.Commands.ConfirmEmail
{
    public  record ConfirmEmailCommand(ConfirmationEmailRequestDto request ):IRequest<Result>;



        public class ConfirmEmailCommandHandler(IIdentityService _identityService) : IRequestHandler<ConfirmEmailCommand, Result>
    {
        public async  Task<Result> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
        {
            var request = command.request;
            if (await _identityService.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidCode);
            }

            var result = await _identityService.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                // todo
                //await _signInManager.UserManager.AddToRoleAsync(user, DefaultRoles.Member.Name);
                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
    }
}
