using Api.Abstractions;
using Api.Extensions;
using Application.Submissions.Commands.CreateSubmission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpPost("add-submission")]
        public async Task<IActionResult> AddSubmission(
    [FromBody] CreateSubmissionDto dto, CancellationToken ct)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var res = await _mediator.Send(new CreateSubmissionCommand(userId, dto), ct);
            return res.IsSuccess
                ? Accepted(new { submissionId = res.Value })
                : res.ToProblem();
        }
    }
}
