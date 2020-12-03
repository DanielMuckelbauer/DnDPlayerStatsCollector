using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic
{
    public class GetPlayerStatisticQuery : ResponseWrapper<GetPlayerStatisticDto>
    {
        public string CharacterId { get; set; }
    }
}
