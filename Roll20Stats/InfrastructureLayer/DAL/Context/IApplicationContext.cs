using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Models;

namespace Roll20Stats.InfrastructureLayer.DAL.Context
{
    public interface IApplicationContext
    {
        DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<T> Set<T>() where T : class, IEntity;
        int SaveChanges();
    }
}