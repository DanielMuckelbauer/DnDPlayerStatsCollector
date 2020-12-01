using System.Collections.Generic;

namespace Roll20Stats.InfrastructureLayer.DAL.Models
{
    public class Game
    {
        public int ID { get; set; }
        public List<PlayerStatistic> PlayerStats { get; set; }
    }
}
