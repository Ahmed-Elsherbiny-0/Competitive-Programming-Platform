using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IList<Answer> Answers { get; set; } = [];

        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
