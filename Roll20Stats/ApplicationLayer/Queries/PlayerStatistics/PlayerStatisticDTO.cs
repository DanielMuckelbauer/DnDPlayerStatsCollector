namespace Roll20Stats.ApplicationLayer.Queries.PlayerStatistics
{
    public class PlayerStatisticDTO
    {
        public string CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
    }
}