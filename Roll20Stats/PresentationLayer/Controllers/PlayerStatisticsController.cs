using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Roll20Stats.ApplicationLayer.Commands.PlayerStatistics;
using Roll20Stats.ApplicationLayer.Queries.PlayerStatistics;

namespace Roll20Stats.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatisticsController : ControllerBase
    {
        private IMediator _mediator;
        private IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPut]
        public async Task<IActionResult> Create(AddPlayerStatisticCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetPlayerStatisticQuery query)
        {
            var result = await Mediator.Send(query);
            if (result is { })
                return Ok(result);
            return NotFound();
        }
    }
}