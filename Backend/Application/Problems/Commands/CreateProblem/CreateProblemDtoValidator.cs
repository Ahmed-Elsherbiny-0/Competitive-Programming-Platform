using Application.Problems.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Problems.Commands.CreateProblem
{
    internal class CreateProblemDtoValidator : AbstractValidator<CreateProblemDto>
    {
        public CreateProblemDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Statement).NotEmpty();
            RuleFor(x => x.InputFormat).NotEmpty();
            RuleFor(x => x.OutputFormat).NotEmpty();
            RuleFor(x => x.Constraints).MaximumLength(1000);
            RuleFor(x => x.Difficulty).IsInEnum();
            RuleFor(x => x.TimeLimitMs).InclusiveBetween(100, 10000);
            RuleFor(x => x.MemoryLimitMb).InclusiveBetween(16, 1024);
            RuleFor(x => x.TopicId).GreaterThan(0).When(x => x.TopicId.HasValue);

            RuleFor(x => x.TestCases).NotEmpty()
                .WithMessage("At least one test case is required.");
            RuleFor(x => x.TestCases)
                .Must(t => t.Any(c => c.IsSample))
                .When(x => x.TestCases is { Count: > 0 })
                .WithMessage("At least one sample test case is required.");

            RuleForEach(x => x.TestCases).ChildRules(t =>
            {
                t.RuleFor(c => c.Input).NotNull();
                t.RuleFor(c => c.ExpectedOutput).NotEmpty();
            });
        }
    }
}