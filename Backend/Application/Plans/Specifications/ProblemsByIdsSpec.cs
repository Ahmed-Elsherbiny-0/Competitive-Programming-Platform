using Domain.Entities;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plans.Specifications
{
    public class ProblemsByIdsSpec(List<int> ids)
    : BaseSpecifications<Problem>(p => ids.Contains(p.Id));
}
