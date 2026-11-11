using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Topics.Commands.CreateTopic
{
    public  class CreateTopicCommandValidator:AbstractValidator<CreateTopicCommand>
    {
        public CreateTopicCommandValidator()
        {
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty(); 
        }
    }
}
