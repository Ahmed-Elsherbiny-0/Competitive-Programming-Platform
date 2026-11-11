using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class ProblemTestCase
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = default!;

        public string Input { get; set; } = default!;
        public string ExpectedOutput { get; set; } = default!;
        public bool IsSample { get; set; }      // shown to users in the statement
        public int SortOrder { get; set; }
    }
}
