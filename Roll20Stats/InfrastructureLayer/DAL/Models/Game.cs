using System.Collections.Generic;

namespace Roll20Stats.InfrastructureLayer.DAL.Models
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        public List<PlayerStatistic> PlayerStats { get; set; }
    }
}
