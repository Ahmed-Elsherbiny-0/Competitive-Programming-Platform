using Domain.Entities;
using Domain.Enums;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plans.Specifications
{
    public class PlanByLevelSpec(PlanLevel level)
      : BaseSpecifications<Plan>(p => p.Level == level);
}
