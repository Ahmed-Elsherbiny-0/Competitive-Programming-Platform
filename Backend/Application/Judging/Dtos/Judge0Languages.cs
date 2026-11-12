using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Judging.Dtos
{
    public static class Judge0Languages
    {
        public static int ToId(Language l) => l switch
        {
            Language.Cpp => 54,
            Language.Python => 71,
            _ => throw new ArgumentOutOfRangeException(nameof(l))
        };
    }
}
