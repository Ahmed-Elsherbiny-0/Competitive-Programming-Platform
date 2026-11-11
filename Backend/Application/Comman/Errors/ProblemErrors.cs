using Domain.Comman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comman.Errors
{
    public static  class ProblemErrors
    {
       
            public static readonly Error CantCreateProblem = new("Problem.CantCreate", "Could not create the problem.",StatusCodes.Status400BadRequest);
            public static readonly Error NotFound = new("Problem.NotFound", "Problem not found.", StatusCodes.Status404NotFound);
        public static readonly Error ProblemAlreadyExists = new("Problem.ProblemExists", "A Problem with this title already exists.", StatusCodes.Status409Conflict);

    }
}
