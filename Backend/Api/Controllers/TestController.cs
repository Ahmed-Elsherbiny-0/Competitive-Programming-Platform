using Api.Abstractions;
using Application.Tests.Commands.CreateTest;
using Application.Tests.Dtos;
using Application.Tests.Queries.GetTest;
using Application.Topics.Commands.CreateTopic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-test")]
        public async Task<IActionResult> AddTest(TestDto testDto, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new CreateTestCommand(testDto), cancellationToken);
            if (res.IsSuccess)
            return Created();
            return res.ToProblem();
        }

        [HttpGet("get-test/{id}")]
        public async Task<IActionResult> GetTest( [FromRoute]int id  , CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new GetTestQuery(id), cancellationToken);
            return Ok(res.Value);
        }
    }
}
