using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plans.Dtos
{
    public record CreatePlanDto(
     string Title,
     string? Description,
     PlanLevel Level,
     List<int> TopicIds,
     List<int> ProblemIds
        );
}
