using Domain.Entities;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Submissions.Specifications
{
    public class GetSubmissionSpec:BaseSpecifications<Submission>
    {
        public GetSubmissionSpec(int id):base(x=>x.Id==id)
        {
            
        }
    }
}
