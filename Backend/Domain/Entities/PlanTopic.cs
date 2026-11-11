using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class PlanTopic
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = default!;
        public int TopicId { get; set; }
        public Topic Topic { get; set; } = default!;
        public int Order { get; set; }
    }
}
