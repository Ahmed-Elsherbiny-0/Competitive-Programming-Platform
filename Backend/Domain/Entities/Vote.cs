using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Vote
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; } = default!;
        public IList<VoteAnswer> VoteAnswers { get; set; } = [];
 
    }
}
