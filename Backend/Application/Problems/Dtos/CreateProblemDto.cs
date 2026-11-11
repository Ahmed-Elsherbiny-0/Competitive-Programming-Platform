using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Problems.Dtos
{
    public  record CreateProblemDto(
    string Title,
    string Statement,
    string InputFormat,
    string OutputFormat,
    string? Constraints,
    ProblemDifficulty Difficulty,
    int TimeLimitMs,
    int MemoryLimitMb,
    int? TopicId,
    List<CreateTestCaseDto> TestCases);
}
