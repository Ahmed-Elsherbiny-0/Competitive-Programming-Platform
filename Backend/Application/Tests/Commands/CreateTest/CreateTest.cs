using Application.Comman.Errors;
using Application.Tests.Dtos;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Commands.CreateTest
{
    public  record CreateTestCommand(TestDto TestDto ):IRequest<Result>;
    public class CreateTestCommandHandler(IGenericRepository<Test> _testRepo, IGenericRepository<Topic> _topicRepo) : IRequestHandler<CreateTestCommand, Result>
    {
        public async Task<Result> Handle(CreateTestCommand command, CancellationToken cancellationToken)
        {
            var dto = command.TestDto;
            var topic = await _topicRepo.GetItemById(dto.TopicId);
            if (topic is null)
                return Result.Failure(TopicErrors.TopicNotFound);
            var test = new Test
            {
                Title = dto.Title,
                TopicId = dto.TopicId,
                Questions = dto.Questions.Select(x => new Question
                {
                    Content = x.Content,
                    Answers = x.Answers.Select(y => new Answer
                    {
                        Content = y.Content,
                    }).ToList()
                }).ToList()
            };
          
          await  _testRepo.AddItem(test);
           if( await _testRepo.SaveChangesAsync())
                return Result.Success();
            return Result.Failure(TestErrors.CantCreateTest);
        }
    }
}
