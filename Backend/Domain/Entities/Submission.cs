using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Submission
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = default!;
        public ApplicationUser ApplicationUser { get; set; }
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = default!;

        public Language Language { get; set; }
        public string SourceCode { get; set; } = default!;

        public Verdict Verdict { get; set; } = Verdict.Pending;
        public int? TimeMs { get; set; }
        public int? MemoryKb { get; set; }
        public int? FailedTestCaseOrder { get; set; }   // which test failed
        public string? CompilerOutput { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
