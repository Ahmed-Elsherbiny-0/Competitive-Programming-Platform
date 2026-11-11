using Application.Comman.Errors;
using Application.Plans.Dtos;
using Application.Plans.Specifications;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plans.Commands.CreatePlan
{
    public record CreatePlanCommand(CreatePlanDto Plan) : IRequest<Result>;


    public class CreatePlanCommandHandler(
    IGenericRepository<Plan> _planRepo,
    IGenericRepository<Topic> _topicRepo,
    IGenericRepository<Problem> _problemRepo) : IRequestHandler<CreatePlanCommand, Result>
    {
        public async Task<Result> Handle(CreatePlanCommand request, CancellationToken ct)
        {
            var dto = request.Plan;

            if (await _planRepo.CountAsync(new PlanByLevelSpec(dto.Level)) > 0)
                return Result.Failure(PlanErrors.LevelAlreadyExists);

            var topicIds = dto.TopicIds.Distinct().ToList();
            if (await _topicRepo.CountAsync(new TopicsByIdsSpec(topicIds)) != topicIds.Count)
                return Result.Failure(PlanErrors.TopicNotFound);

            var problemIds = dto.ProblemIds.Distinct().ToList();
            if (problemIds.Count > 0 &&
                await _problemRepo.CountAsync(new ProblemsByIdsSpec(problemIds)) != problemIds.Count)
                return Result.Failure(PlanErrors.ProblemNotFound);

            var plan = new Plan
            {
                Title = dto.Title,
                Description = dto.Description,
                Level = dto.Level,
                PlanTopics = topicIds
                    .Select((id, i) => new PlanTopic { TopicId = id, Order = i + 1 }).ToList(),
                PlanProblems = problemIds
                    .Select((id, i) => new PlanProblem { ProblemId = id, Order = i + 1 }).ToList()
            };

            await _planRepo.AddItem(plan);
            return await _planRepo.SaveChangesAsync()
                ? Result.Success()
                : Result.Failure(PlanErrors.CantCreatePlan);
        }
    }
}

