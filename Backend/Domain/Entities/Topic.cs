using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Topic
    {
        public int Id { get; set; }
        public string Title  { get; set; }
        public string Content { get; set; }
        public IList<Test>? Tests { get; set; } = [];
        public List<Problem> Problems { get; set; } = [];         
        public List<PlanTopic> PlanTopics { get; set; } = [];
    }
}
