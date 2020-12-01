using Microsoft.EntityFrameworkCore;
using Roll20Stats.DAL.Models;

namespace Roll20Stats.DAL.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
    }
}