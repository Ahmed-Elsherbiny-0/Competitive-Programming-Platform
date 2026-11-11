using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Submissions.Commands.CreateSubmission
{
    public record CreateSubmissionDto(int ProblemId, Language Language, string SourceCode);
}
