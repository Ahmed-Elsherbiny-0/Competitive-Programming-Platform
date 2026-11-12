using Application.Comman.Errors;
using Application.Submissions.Specifications;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Submissions.Queries
{
    public record GetSubmissionQuery(int Id, string UserId) : IRequest<Result<SubmissionDto>>;
    public class GetSubmissionQueryHandler(IGenericRepository<Submission> submissionRepo)
    : IRequestHandler<GetSubmissionQuery, Result<SubmissionDto>>
    {
        public async Task<Result<SubmissionDto>> Handle(GetSubmissionQuery request, CancellationToken ct)
        {
            var submission = await submissionRepo.GetEntityWithspec(new GetSubmissionSpec(request.Id));

            //|| submission.ApplicationUserId != request.UserId
            if (submission is null )
                return Result.Failure<SubmissionDto>(SubmissionErrors.NotFound);

            var dto = new SubmissionDto(
                submission.Id,
                submission.ProblemId,
                submission.Language,
                submission.Verdict,
                submission.TimeMs,
                submission.MemoryKb,
                submission.FailedTestCaseOrder,
                submission.CompilerOutput,
                submission.CreatedAt);

            return Result.Success(dto);
        }
    }
}
