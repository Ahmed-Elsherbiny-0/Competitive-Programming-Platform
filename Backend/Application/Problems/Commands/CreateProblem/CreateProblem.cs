using Application.Comman.Errors;
using Application.Plans.Specifications;
using Application.Problems.Commands.Specifications;
using Application.Problems.Dtos;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Problems.Commands.CreateProblem
{
    public record CreateProblemCommand(CreateProblemDto Problem) : IRequest<Result>;
    public class CreateProblemCommandHandler(
    IGenericRepository<Problem> _problemRepo,
    IGenericRepository<Topic> _topicRepo) : IRequestHandler<CreateProblemCommand, Result>
    {
        public async Task<Result> Handle(CreateProblemCommand request, CancellationToken ct)
        {
            var dto = request.Problem;

            if (dto.TopicId is int topicId &&
                await _topicRepo.CountAsync(new TopicsByIdsSpec([topicId])) == 0)
                return Result.Failure(TopicErrors.TopicNotFound);
            if (await _problemRepo.GetEntityWithspec(new GetProblemByTitleSpec(dto.Title.Trim().ToLower()))is not  null)
            {
                return Result.Failure(ProblemErrors.ProblemAlreadyExists);
            }
           

            var problem = new Problem
            {
                Title = dto.Title.Trim(),
                Statement = dto.Statement,
                InputFormat = dto.InputFormat,
                OutputFormat = dto.OutputFormat,
                Constraints = dto.Constraints,
                Difficulty = dto.Difficulty,
                TimeLimitMs = dto.TimeLimitMs,
                MemoryLimitMb = dto.MemoryLimitMb,
                TopicId = dto.TopicId,
                TestCases = dto.TestCases.Select((t, i) => new ProblemTestCase
                {
                    Input = t.Input,
                    ExpectedOutput = t.ExpectedOutput,
                    IsSample = t.IsSample,
                    SortOrder = i + 1
                }).ToList()
            };

            _problemRepo.AddItem(problem);
            return await _problemRepo.SaveChangesAsync()
                ? Result.Success()
                : Result.Failure(ProblemErrors.CantCreateProblem);
        }
    }
}
