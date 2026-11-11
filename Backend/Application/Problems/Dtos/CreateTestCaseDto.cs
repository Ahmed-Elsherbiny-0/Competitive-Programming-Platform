using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Problems.Dtos
{
    public  record CreateTestCaseDto
    (string Input, string ExpectedOutput, bool IsSample);
}
