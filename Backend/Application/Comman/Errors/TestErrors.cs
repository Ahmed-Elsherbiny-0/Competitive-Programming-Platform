using Domain.Comman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comman.Errors
{
    public static  class TestErrors
    {
        public static readonly Error CantCreateTest = new("Test.CantCreateTest", "Can't Create Test ..plz Try again ", StatusCodes.Status400BadRequest);

    }
}
