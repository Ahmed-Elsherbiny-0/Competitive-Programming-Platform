using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; } 
        public IList<Question> Questions { get; set; } = [];
    }
}
