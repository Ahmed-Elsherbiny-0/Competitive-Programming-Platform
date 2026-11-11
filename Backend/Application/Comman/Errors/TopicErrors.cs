using Domain.Comman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comman.Errors
{
    public  class TopicErrors
    {
        public static readonly Error CantCreateTopic = new("Topic.CantCreateTopic", "Can't Create Topic ..plz Try again ", StatusCodes.Status400BadRequest);
        public static readonly Error TopicNotFound = new("Topic.TopicNotFound", "Topic Not Found ", StatusCodes.Status404NotFound);
    }
}
