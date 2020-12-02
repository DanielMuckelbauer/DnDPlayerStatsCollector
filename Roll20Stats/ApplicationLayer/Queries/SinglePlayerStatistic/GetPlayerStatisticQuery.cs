using MediatR;
using Roll20Stats.ApplicationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQuery : IRequest<PlayerStatisticDTO>, IRequest<Unit>
    {
        public string CharacterId { get; set; }
    }
}
