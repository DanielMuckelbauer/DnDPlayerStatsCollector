namespace Roll20Stats.InfrastructureLayer.DAL.Entities
{
    public class PlayerStatistic : IEntity
    {
        public int Id { get; set; }
        public string CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
    }
}
