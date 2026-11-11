using Domain.Entities;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Problems.Commands.Specifications
{
    internal class GetProblemByTitleSpec:BaseSpecifications<Problem>
    {
        public GetProblemByTitleSpec(string title):base(x=>x.Title.ToLower()==title.ToLower())
        {
            
        }
    }
}
