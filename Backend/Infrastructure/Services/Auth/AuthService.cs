using Application.Auth.Commands.ConfirmEmail;
using Application.Auth.Commands.CreateLogin;
using Application.Auth.Commands.CreateRefreshToken;
using Application.Auth.Commands.CreateRegister;
using Application.Auth.Commands.ForgetPassword;
using Application.Auth.Commands.ResendConfirmationEmail;
using Application.Auth.Commands.ResetPassword;
using Application.Auth.Commands.RevokeRefreshToken;
using Application.Comman;
using Application.Dtos;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain;
using Domain.Comman;
using Domain.Constants;
using Domain.Entities;
using Hangfire;
using Infrastructure.Data;
using Infrastructure.Helper.Email;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Auth
{
    public  class AuthService(SignInManager<ApplicationUser> _signInManager,
        ApplicationDbContext _context,
        IJwtProvider _jwtProvider
        ,ILogger<AuthService> _logger,
        IHttpContextAccessor _httpContextAccessor,
        IEmailSender _emailSender,
        IEmailBodyBuilder _emailBodyBuilder,
        IPhotoService _photoService,
        IMediator _mediator) : IAuthService
    {


          private readonly int _refreshTokenExpiryDays = 14;
        public async Task<Result<AuthResponseDto>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new CreateLoginCommand(email, password));
            return res;
        }


        public async Task<Result> Register(RegisterRequestDto request, CancellationToken cancellationToken = default)
        {
          var res=  await _mediator.Send(new CreateRegisterCommand(request));
            return res;
        }


        public async Task<Result<AuthResponseDto>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new CreateRefreshTokenCommand(token,refreshToken,cancellationToken));
            return res;
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new RevokeRefreshTokenCommand(token,refreshToken));
            return res;
        }


        public async Task<Result> ConfirmEmailAsync(ConfirmationEmailRequestDto request)
        {
            var res = await _mediator.Send(new ConfirmEmailCommand(request));
            return res;
        }

        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequestDto request)
        {
            var res = await _mediator.Send(new ResendConfirmationEmailCommand(request));
            return res;
        }

        public async Task<Result> SendResetPasswordCodeAsync(string email)
        {
   var res = await _mediator.Send(new ForgetPasswordCommand(email));
            return res;
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            var res = await _mediator.Send(new ResetPasswordCommand(request));
            return res;

        }
        public  async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
           
            var emailBody = _emailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
            {
                { "{{name}}", user.FirstName },
                    { "{{action_url}}", $"{origin}/verify-email?userId={user.Id}&code={code}" }
            });

         
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email, "✅ Cp-platform: Email Confirmation", emailBody));

            await Task.CompletedTask;    
        }


        public  async Task SendResetPasswordEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = _emailBodyBuilder.GenerateEmailBody("ForgetPassword",
                 new Dictionary<string, string>
                {
                { "{{name}}", user.FirstName },
                { "{{action_url}}", $"{origin}/reset-password?email={user.Email}&code={code}" }
                }
            );
            BackgroundJob.Enqueue(()=>
           _emailSender.SendEmailAsync(user.Email!, "✅ Mostaqel: Change Password", emailBody));

            await Task.CompletedTask;
        }

        public   string GenerateRefreshToken()
        {
            return WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(64));
        }
    }
}
