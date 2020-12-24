using MediatR;

namespace Roll20Stats.ApplicationLayer.PlayerStatistics.Commands.DeletePlayerStatistic
{
    public class DeletePlayerStatisticCommand : IRequest
    {
        public string CharacterId { get; set; }
        public string GameName { get; set; }
    }
}
