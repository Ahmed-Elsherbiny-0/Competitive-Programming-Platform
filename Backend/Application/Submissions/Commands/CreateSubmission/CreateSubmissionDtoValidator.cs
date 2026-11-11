using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Submissions.Commands.CreateSubmission
{
    public  class CreateSubmissionDtoValidator : AbstractValidator<CreateSubmissionDto>
    {
        public CreateSubmissionDtoValidator()
        {
            RuleFor(x => x.ProblemId).GreaterThan(0);
            RuleFor(x => x.Language).IsInEnum();
            RuleFor(x => x.SourceCode)
                .NotEmpty()
                .MaximumLength(65536)
                .WithMessage("Source code must be at most 64 KB.");
        }
    }
}
