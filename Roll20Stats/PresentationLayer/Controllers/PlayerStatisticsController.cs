using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
using Roll20Stats.ApplicationLayer.Commands.DeletePlayerStatistic;
using Roll20Stats.ApplicationLayer.Queries.AllPlayerStatistics;
using Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic;

namespace Roll20Stats.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> Create(AddPlayerStatisticCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetPlayerStatisticQuery { CharacterId = id });
            if (result is { })
                return Ok(result);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllPlayerStatisticsQuery());
            if (result is { })
                return Ok(result);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeletePlayerStatisticCommand { CharacterId = id }));
        }
    }
}