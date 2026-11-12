using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Submissions.Queries
{
    public record SubmissionDto(
        int Id,
        int ProblemId,
        Language Language,
        Verdict Verdict,
        int? TimeMs,
        int? MemoryKb,
        int? FailedTestCaseOrder,
        string? CompilerOutput,
        DateTime CreatedAt);
}