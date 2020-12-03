using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Entities;

namespace Roll20Stats.Tests.Shared
{
    public static class TestDatabaseManager
    {
        public static void SeedDatabase<TEntity>(WebApplicationFactory<Startup> factory, params TEntity[] entities) where TEntity : IEntity
        {
            using var serviceScope = factory.Services.CreateScope();
            var scopedServices = serviceScope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationContext>();
            db.Database.EnsureCreated();
            foreach (var entity in entities)
            {
                db.Add(entity);
            }
            db.SaveChanges();
        }

        public static WebApplicationFactory<Startup> SetupInMemoryDatabase(WebApplicationFactory<Startup> factory)
        {
            var returnFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                    foreach (var d in descriptors.ToList())
                    {
                        services.Remove(d);
                    }

                    services.AddDbContext<ApplicationContext>((provider, optionsBuilder) =>
                    {
                        optionsBuilder
                            .UseInMemoryDatabase("test-database");
                    });
                });
            });
            ResetInMemoryDatabase(returnFactory);
            return returnFactory;
        }

        private static void ResetInMemoryDatabase(WebApplicationFactory<Startup> factory)
        {
            using var serviceScope = factory.Services.CreateScope();
            var scopedServices = serviceScope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationContext>();
            db.Database.EnsureCreated();
            db.PlayerStatistics.RemoveRange(db.PlayerStatistics);
            db.SaveChanges();
        }
    }
}