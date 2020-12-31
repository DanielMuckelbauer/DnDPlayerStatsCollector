using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;
using System.Collections.Generic;

namespace Roll20Stats.ApplicationLayer.Games.Queries.SingleGame
{
    public class GetAllGamesQuery : IRequest<IEnumerable<GameDto>>
    {
    }
}
