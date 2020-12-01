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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await Mediator.Send(new GetPlayerStatisticQuery {CharacterId = id});
            if (result is { })
                return Ok(result);
            return NotFound();
        }
    }
}