using Application.Comman.Errors;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Topics.Commands.CreateTopic
{
    public  record CreateTopicCommand(string Content,string Title ):IRequest<Result> ;

    public class CreateTopicCommandHandler(IGenericRepository<Topic> _topicRepo) : IRequestHandler<CreateTopicCommand, Result>
    {
        public async Task<Result> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Topic
            {
                Content = request.Content,
                Title = request.Title
            };
           await  _topicRepo.AddItem(topic);
            if (await _topicRepo.SaveChangesAsync())
                return Result.Success(topic.Id);
            return Result.Failure(TopicErrors.CantCreateTopic);
        }
    }
}
