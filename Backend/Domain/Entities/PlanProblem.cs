using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class PlanProblem
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = default!;
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = default!;
        public int Order { get; set; }
    }
}
