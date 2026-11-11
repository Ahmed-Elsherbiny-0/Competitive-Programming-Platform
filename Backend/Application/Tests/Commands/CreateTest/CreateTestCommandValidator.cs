using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Commands.CreateTest
{
    public  class CreateTestCommandValidator:AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidator()
        {
            RuleFor(x => x.TestDto.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.TestDto.Questions).NotEmpty();

            RuleForEach(x => x.TestDto.Questions).ChildRules(q =>
            {
                q.RuleFor(x => x.Content).NotEmpty();
                q.RuleFor(x => x.Answers)
                    .Must(a => a.Count >= 3)
                    .WithMessage("Each question needs at least 3 answers.");
            });
        }
    }
}
