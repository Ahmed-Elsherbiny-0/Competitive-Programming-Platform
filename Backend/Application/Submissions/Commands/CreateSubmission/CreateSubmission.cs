using Application.Comman.Errors;
using Domain.Comman;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Submissions.Commands.CreateSubmission
{

    public record CreateSubmissionCommand(string UserId, CreateSubmissionDto Submission)
        : IRequest<Result<int>>;

    public class CreateSubmissionCommandHandler(
    IGenericRepository<Submission> _submissionRepo,
    IGenericRepository<Problem> _problemRepo)
    : IRequestHandler<CreateSubmissionCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateSubmissionCommand request, CancellationToken ct)
        {
            var dto = request.Submission;

            // the problem must exist
            var problem = await _problemRepo.GetItemById(dto.ProblemId);
            if (problem is null)
                return Result.Failure<int>(SubmissionErrors.ProblemNotFound);

            var submission = new Submission
            {
                ApplicationUserId = request.UserId,
                ProblemId = dto.ProblemId,
                Language = dto.Language,
                SourceCode = dto.SourceCode,
                Verdict = Verdict.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _submissionRepo.AddItem(submission);

            return await _submissionRepo.SaveChangesAsync()
                ? Result.Success(submission.Id)
                : Result.Failure<int>(SubmissionErrors.CantCreateSubmission);
        }
    }
}
