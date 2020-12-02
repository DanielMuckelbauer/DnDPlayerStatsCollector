using MediatR;
using Roll20Stats.ApplicationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQuery : IRequest<PlayerStatisticDTO>
    {
        public string CharacterId { get; set; }
    }
}
