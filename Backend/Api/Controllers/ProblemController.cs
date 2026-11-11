using Api.Abstractions;
using Application.Problems.Commands.CreateProblem;
using Application.Problems.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-problem")]
        public async Task<IActionResult> AddProblem([FromBody] CreateProblemDto dto, CancellationToken ct)
        {
            var res = await _mediator.Send(new CreateProblemCommand(dto), ct);
            return res.IsSuccess ? Created() : res.ToProblem();
        }
    }
}
