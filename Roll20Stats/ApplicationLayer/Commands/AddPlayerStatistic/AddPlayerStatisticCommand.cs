using MediatR;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic
{
    public class AddPlayerStatisticCommand : IRequest<ResponseWithMetaData<AddPlayerStatisticRequest>>
    {
        public string CharacterName { get; set; }
        public string CharacterId { get; set; }
        public string GameName { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
    }
}
