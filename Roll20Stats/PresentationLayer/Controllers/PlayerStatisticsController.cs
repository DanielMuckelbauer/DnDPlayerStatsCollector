using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roll20Stats.ApplicationLayer.PlayerStatistics.Commands.AddPlayerStatistic;
using Roll20Stats.ApplicationLayer.PlayerStatistics.Commands.DeletePlayerStatistic;
using Roll20Stats.ApplicationLayer.PlayerStatistics.Queries.AllPlayerStatistics;
using Roll20Stats.ApplicationLayer.PlayerStatistics.Queries.SinglePlayerStatistic;

namespace Roll20Stats.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatisticsController : Roll20ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> Create(AddPlayerStatisticCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResponse(result);
        }

        [HttpGet("{id}/{gameName}")]
        public async Task<IActionResult> Get(string id, string gameName)
        {
            var result = await _mediator.Send(new GetPlayerStatisticQuery { CharacterId = id, GameName = gameName });
            return CreateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllPlayerStatisticsQuery());
            if (result is { })
                return Ok(result);
            return NotFound();
        }

        [HttpDelete("{id}/{gameName}")]
        public async Task<IActionResult> Delete(string id, string gameName)
        {
            return Ok(await _mediator.Send(new DeletePlayerStatisticCommand { CharacterId = id, GameName = gameName }));
        }
    }
}