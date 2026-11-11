using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Problem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Statement { get; set; } = default!;
        public string InputFormat { get; set; } = default!;
        public string OutputFormat { get; set; } = default!;
        public string? Constraints { get; set; }
        public ProblemDifficulty Difficulty { get; set; }
        public int TimeLimitMs { get; set; } = 1000;
        public int MemoryLimitMb { get; set; } = 256;



        public int? TopicId { get; set; }
        public Topic? Topic { get; set; }
        public List<PlanProblem> PlanProblems { get; set; } = [];
        public List<ProblemTestCase> TestCases { get; set; } = [];
        public List<Submission> Submissions { get; set; } = [];

    }
}
