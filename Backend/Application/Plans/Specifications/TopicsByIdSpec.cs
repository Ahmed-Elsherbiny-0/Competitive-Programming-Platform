using Domain.Entities;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plans.Specifications
{
    public class TopicsByIdsSpec(List<int> ids)
    : BaseSpecifications<Topic>(t => ids.Contains(t.Id));
}
