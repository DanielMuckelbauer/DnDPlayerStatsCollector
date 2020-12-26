using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.PlayerStatistics.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQuery : IRequest<ResponseWithMetaData<PlayerStatisticDto>>
    {
        public string CharacterId { get; set; }
        public string GameName { get; set; }
    }
}
