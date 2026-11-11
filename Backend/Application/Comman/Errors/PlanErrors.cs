using Domain.Comman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comman.Errors
{
    public  class PlanErrors
    {
        public static readonly Error LevelAlreadyExists = new("Plan.LevelExists", "A plan with this level already exists.", StatusCodes.Status409Conflict);
        public static readonly Error TopicNotFound = new("Plan.TopicNotFound", "One or more topics do not exist.", StatusCodes.Status404NotFound);
        public static readonly Error ProblemNotFound = new("Plan.ProblemNotFound", "One or more problems do not exist.", StatusCodes.Status404NotFound);
        public static readonly Error CantCreatePlan = new("Plan.CantCreate", "Could not create the plan.", StatusCodes.Status400BadRequest);
        public static readonly Error NotFound = new("Plan.NotFound", "Plan not found.", StatusCodes.Status404NotFound);
    }
}
