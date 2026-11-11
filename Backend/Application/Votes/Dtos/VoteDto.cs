using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Votes.Dtos
{
    public   record VoteDto(int  testId ,List<VoteAnswerDto>VoteAnswers);
    
}
