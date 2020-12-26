using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.PlayerStatistics.Commands.AddPlayerStatistic
{
    public class AddPlayerStatisticCommand : IRequest<ResponseWithMetaData<PlayerStatisticDto>>
    {
        public string CharacterName { get; set; }
        public string CharacterId { get; set; }
        public string GameName { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
    }
}
