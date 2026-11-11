using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Plan
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public PlanLevel Level { get; set; }
        public List<PlanTopic> PlanTopics { get; set; } = [];
        public List<PlanProblem> PlanProblems { get; set; } = [];
        public List<ApplicationUser> Users { get; set; } = [];
    }
      
}

