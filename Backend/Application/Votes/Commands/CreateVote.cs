using Application.Comman.Errors;
using Application.Votes.Dtos;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Votes.Commands
{
    public  record  CreateVoteCommand(string userId,VoteDto Votedto):IRequest<Result>;
   public class CreateVoteCommandHandler(IGenericRepository<Vote>_voteRepo) : IRequestHandler<CreateVoteCommand, Result>
    {
        public async Task<Result> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Votedto;
            var vote = new Vote
            {
                ApplicationUserId = request.userId,
                TestId = dto.testId,
                VoteAnswers = dto.VoteAnswers.Select(x => new VoteAnswer
                {
                    QuestionId = x.QuestionId,
                    AnswerId = x.AnswerId,

                }).ToList()

            };
            _voteRepo.AddItem(vote);
            if (await _voteRepo.SaveChangesAsync())
            {
                return Result.Success();
            }
            return Result.Failure(VoteErrors.CantCreateVote);

        }
    }
}
