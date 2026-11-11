using Application.Plans.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plans.Commands.CreatePlan
{
    public  class CreatePlanDtoValidator: AbstractValidator<CreatePlanDto>
    {
        public CreatePlanDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Level).IsInEnum();

            RuleFor(x => x.TopicIds).NotEmpty()
                .WithMessage("A plan needs at least one topic.");
            RuleFor(x => x.TopicIds)
                .Must(l => l.Distinct().Count() == l.Count)
                .When(x =>  x.TopicIds?.Count > 0)
                .WithMessage("Duplicate topics.");

            RuleFor(x => x.ProblemIds)
                .Must(l => l.Distinct().Count() == l.Count)
                .When(x => x.ProblemIds is { Count: > 0 })
                .WithMessage("Duplicate problems.");
        }
    }
}
