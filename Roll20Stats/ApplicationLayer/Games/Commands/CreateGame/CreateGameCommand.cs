using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<ResponseWithMetaData<GameDto>>
    {
        public string Name { get; set; }
    }
}
