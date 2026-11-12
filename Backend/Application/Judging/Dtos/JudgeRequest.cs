using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Judging.Dtos
{
    public record JudgeRequest
  (string SourceCode, Language Language, string Input,
                           string ExpectedOutput, int TimeLimitMs, int MemoryLimitMb);
}
