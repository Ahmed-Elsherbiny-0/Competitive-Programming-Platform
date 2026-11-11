using Api.Abstractions;
using Application.Topics.Commands.CreateTopic;
using Application.Topics.Commands.DeleteTopic;
using Application.Topics.Queries;
using Domain.Specifications;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-topic")]
        public async Task<IActionResult> AddTopic(CreateTopicCommand command ,CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new CreateTopicCommand(command.Content, command. Title ), cancellationToken);
            return Created();
        }
        [HttpGet("get-topics")]
        public async Task<IActionResult> GetTopic([FromQuery]TopicSpecParms parms, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new GetTopicsQuery(parms), cancellationToken); 
            if (res.IsSuccess)
           return Ok(res.Value);
            return res.ToProblem();
        }
        [HttpDelete("delete-topic/{id}")]
        public async Task<IActionResult> DeleteTopic([FromRoute] DeleteTopicCommand command, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new DeleteTopicCommand(command.Id), cancellationToken);
            if (res.IsSuccess)
                return Ok();
            return res.ToProblem();
        }
    }
}
