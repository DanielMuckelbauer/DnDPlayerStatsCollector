using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roll20Stats.ApplicationLayer.Commands.CreateGame;
using Roll20Stats.ApplicationLayer.Queries.SingleGame;

namespace Roll20Stats.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Roll20ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> Create(string name)
        {
            var result = await _mediator.Send(new CreateGameCommand { Name = name });
            return CreateResponse(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetSingle(string name)
        {
            var result = await _mediator.Send(new GetSingleGameQuery { Name = name });
            return CreateResponse(result);
        }
    }
}
