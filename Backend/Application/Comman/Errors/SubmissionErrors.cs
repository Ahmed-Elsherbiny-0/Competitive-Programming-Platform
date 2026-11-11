using Domain.Comman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comman.Errors
{
    public static class SubmissionErrors
    {
        public static readonly Error ProblemNotFound = new("Submission.ProblemNotFound", "The problem does not exist.",StatusCodes.Status404NotFound);
        public static readonly Error CantCreateSubmission = new("Submission.CantCreate", "Could not save the submission.", StatusCodes.Status400BadRequest);
        public static readonly Error NotFound = new("Submission.NotFound", "Submission not found.", StatusCodes.Status404NotFound);
    }
}
