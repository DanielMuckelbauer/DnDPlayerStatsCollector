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
            var result = await _mediator.Send(new GetPlayerStatisticQuery {CharacterId = id});
            if (result is { })
                return Ok(result);
            return NotFound();
        }
    }
}