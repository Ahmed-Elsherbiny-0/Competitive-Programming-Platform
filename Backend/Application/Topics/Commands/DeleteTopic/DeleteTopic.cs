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

namespace Application.Topics.Commands.DeleteTopic
{
    public record DeleteTopicCommand(int Id):IRequest<Result>;
    public class DeleteTopicCommandHandler(IGenericRepository<Topic> _topicRepo) : IRequestHandler<DeleteTopicCommand,Result>
    {

        public async Task<Result> Handle(DeleteTopicCommand command, CancellationToken cancellationToken)
        {

             _topicRepo.DeleteItemById(command.Id);
            if (await  _topicRepo.SaveChangesAsync())
            {
                return Result.Success();
            }
          return   Result.Failure(TopicErrors.TopicNotFound);
        }
    }
}
