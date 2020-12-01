using MediatR;

namespace Roll20Stats.ApplicationLayer.Queries.PlayerStatistics
{
    public class GetPlayerStatisticQuery : IRequest<PlayerStatisticDTO>, IRequest<Unit>
    {
        public string CharacterId { get; set; }
    }
}
