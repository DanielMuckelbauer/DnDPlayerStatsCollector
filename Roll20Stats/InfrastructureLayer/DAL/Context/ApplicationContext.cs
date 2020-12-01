using Microsoft.EntityFrameworkCore;
using Roll20Stats.InfrastructureLayer.DAL.Models;

namespace Roll20Stats.InfrastructureLayer.DAL.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }

        public new DbSet<T> Set<T>() where T : class, IEntity
        {
            return base.Set<T>();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}