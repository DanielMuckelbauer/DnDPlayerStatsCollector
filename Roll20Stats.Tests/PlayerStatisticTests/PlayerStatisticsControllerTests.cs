using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
using Roll20Stats.InfrastructureLayer.DAL.Context;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Xunit;

namespace Roll20Stats.Tests.PlayerStatisticTests
{
    public class PlayerStatisticsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PlayerStatisticsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = SetupInMemoryDatabase(factory);
        }

        private WebApplicationFactory<Startup> SetupInMemoryDatabase(WebApplicationFactory<Startup> factory)
        {
            return factory.WithWebHostBuilder(builder =>
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
                    using var serviceScope = _factory.Services.CreateScope();
                    var scopedServices = serviceScope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationContext>();

                    db.Database.EnsureCreated();

                    db.PlayerStatistics.RemoveRange(db.PlayerStatistics);
                    db.SaveChanges();
                });
            });
        }

        [Fact]
        public async Task Create_Returns_Success()
        {
            var client = _factory.CreateDefaultClient();
            var request = new AddPlayerStatisticCommand
            {
                CharacterId = "Id",
                CharacterName = "Name",
                DamageDealt = 1,
                DamageTaken = 2
            };
            var requestBody = new StringContent(JsonConvert.SerializeObject(request));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync("/api/playerstatistics", requestBody);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Creates_Statistic()
        {
            var stat = new PlayerStatistic
            {
                CharacterId = "Id",
                CharacterName = "Testosteron",
                DamageTaken = 5
            };

            using var serviceScope = _factory.Services.CreateScope();
            var scopedServices = serviceScope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationContext>();
            db.Database.EnsureCreated();
            db.PlayerStatistics.Add(stat);
            db.SaveChanges();

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/playerstatistics/Id");

            var responseObject = JsonConvert.DeserializeObject<PlayerStatistic>(await response.Content.ReadAsStringAsync());
            responseObject.CharacterId.Should().Be("Id");
            responseObject.CharacterName = "Testodron";
        }
    }
}
