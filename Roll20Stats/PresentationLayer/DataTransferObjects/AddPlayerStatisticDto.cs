namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public class AddPlayerStatisticDto
    {
        public int Id { get; set; }
        public string CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
        public string GameName { get; set; }
    }
}
