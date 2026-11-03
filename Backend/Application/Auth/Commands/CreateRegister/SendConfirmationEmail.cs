using Domain.Entities;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.CreateRegister
{
    public  record SendConfirmationEmailCommand(ApplicationUser user, string code):IRequest;
        public class SendConfirmationEmailCommandHandler(IHttpContextAccessor _httpContextAccessor,
        IEmailSender _emailSender,
         IEmailBodyBuilder _emailBodyBuilder)
    : IRequestHandler<SendConfirmationEmailCommand>
        {

            public async Task Handle(
                SendConfirmationEmailCommand request,
                CancellationToken cancellationToken)
            {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = _emailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
            {
                { "{{name}}", request.user.FirstName },
                    { "{{action_url}}", $"{origin}/verify-email?userId={request.user.Id}&code={request.code}" }
            });


            await _emailSender.SendEmailAsync(request.user.Email, "✅ Mostaqel: Email Confirmation", emailBody);
            await Task.CompletedTask;
        }
        }
}
