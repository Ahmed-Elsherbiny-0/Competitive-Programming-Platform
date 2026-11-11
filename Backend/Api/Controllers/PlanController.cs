using Api.Abstractions;
using Application.Plans.Commands.CreatePlan;
using Application.Plans.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController (IMediator _mediator): ControllerBase
    {
        [HttpPost("add-plan")]
        public async Task<IActionResult> AddPlan([FromBody] CreatePlanDto dto, CancellationToken ct)
        {
            var res = await _mediator.Send(new CreatePlanCommand(dto), ct);
            return res.IsSuccess ? Created() : res.ToProblem();
        }
    }
}
