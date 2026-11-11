using Domain.Comman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comman.Errors
{
    public class VoteErrors
    {
        public static readonly Error CantCreateVote = new("Vote.CantCreateVote", "Can't Create Vote  ..plz Try again ", StatusCodes.Status400BadRequest);

    }
}
