using Application.Comman.Errors;
using Application.Dtos;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Comman;
using Domain.Constants;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.CreateRegister
{
    public record CreateRegisterCommand(RegisterRequestDto request) : IRequest<Result>;
    public class CreateRegisterCommandHandler(
        IApplicationDbContext _context,
         IIdentityService _identityService,
         IPhotoService _photoService, 
        ISender _sender,IAuthService _authService) : IRequestHandler<CreateRegisterCommand, Result>
    {
        public async Task<Result> Handle(CreateRegisterCommand command, CancellationToken cancellationToken)
        {
            var request = command.request;
            var userIsFound = await _identityService.FindByEmailAsync(request.Email);
            if (userIsFound != null)
            {
                return Result.Failure(UserErrors.DuplicatedEmail);
            }

            if (request.Photo == null)
            {
                request = request with { Photo = new PhotoDto(PhotoConstant.publicId, PhotoConstant.url) };
            }

            var user = request.Adapt<ApplicationUser>();
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await _identityService.CreateUserAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var identityError = result.Errors.First();
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure(new Error(identityError.Code, identityError.Description, StatusCodes.Status400BadRequest));
                }

                var photoResult = await _photoService.AddPhotoToUser(request.Photo!.Adapt<Photo>(), user.Email);
                if (!photoResult.IsSuccess)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    await _identityService.DeleteUserAsync(user);
                    return Result.Failure<PhotoDto>(photoResult.Error);
                }
                await _context.SaveChangesAsync(cancellationToken);

                var code = await _identityService.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //_logger.LogInformation("Confirmation code: {code}", code);

                //await SendConfirmationEmail(user, code);
            
         await    _authService.SendConfirmationEmail(user,code);
                await transaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch
            {

                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}