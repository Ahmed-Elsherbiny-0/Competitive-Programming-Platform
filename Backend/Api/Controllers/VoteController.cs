using Api.Abstractions;
using Api.Extensions;
using Application.Tests.Commands.CreateTest;
using Application.Tests.Dtos;
using Application.Votes.Commands;
using Application.Votes.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class VoteController (IMediator _mediator): ControllerBase
    {
        [Authorize]
        [HttpPost("add-vote")]
        public async Task<IActionResult> AddVote(VoteDto voteDto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var res = await _mediator.Send(new CreateVoteCommand(userId,voteDto), cancellationToken);
            if (res.IsSuccess)
                return Created();
            return res.ToProblem();
        }
    }
}
