namespace Roll20Stats.InfrastructureLayer.DAL.Models
{
    public class PlayerStatistic
    {
        public int Id { get; set; }
        public string CharacterId { get; set; }
        public string Name { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
    }
}
