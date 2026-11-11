using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Verdict
    {
        Pending = 1,
        Running = 2,
        Accepted = 3,
        WrongAnswer = 4,
        TimeLimitExceeded = 5,
        MemoryLimitExceeded = 6,
        RuntimeError = 7,
        CompilationError = 8,
        InternalError = 9
    }
}
