using Domain.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.CreateRegister
{
    public class CreateRegisterCommandValidator : AbstractValidator<CreateRegisterCommand>
    {
        public CreateRegisterCommandValidator()
        {
            RuleFor(x => x.request.Email).NotEmpty().MaximumLength(200);
            RuleFor(x => x.request.Username).NotEmpty().MaximumLength(200);
            RuleFor(x=>x.request.Password).NotEmpty().MaximumLength(200).Matches(RegexPatterns.Password);
            RuleFor(x => x.request.FirstName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.request.LastName).NotEmpty().MaximumLength(200);
        }
    }
}
