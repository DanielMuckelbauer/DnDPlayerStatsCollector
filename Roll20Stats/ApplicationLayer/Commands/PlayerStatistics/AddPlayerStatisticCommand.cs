using MediatR;

namespace Roll20Stats.ApplicationLayer.Commands.PlayerStatistics
{
    public class AddPlayerStatisticCommand : IRequest<Unit>
    {
        public string CharacterName { get; set; }
        public string CharacterId { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
    }
}
