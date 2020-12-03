using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            _factory = factory;
            SetupInMemoryDatabase(_factory);
        }

        private void SetupInMemoryDatabase(WebApplicationFactory<Startup> factory)
        {
            
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
                CharacterName = "Testosteron"
            };

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices((context, services) =>
                {
                    var serviceDescriptor = services
                        .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));

                    if (serviceDescriptor != null)
                    {
                        services.Remove(serviceDescriptor);
                    }


                    services.AddDbContext<ApplicationContext>((provider, optionsBuilder) =>
                    {
                        optionsBuilder
                            .UseInMemoryDatabase("test-database");
                    });
                    var servicesProvider = services.BuildServiceProvider();

                    using var serviceScope = servicesProvider.CreateScope();
                    var scopedServices = serviceScope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationContext>();

                    db.Database.EnsureCreated();

                    db.PlayerStatistics.Add(stat);
                    db.SaveChanges();
                });
            }).CreateClient();
            var response = await client.GetAsync("/api/playerstatistics/Id");

            var responseObject = JsonConvert.DeserializeObject<PlayerStatistic>(await response.Content.ReadAsStringAsync());
            responseObject.CharacterId.Should().Be("Id");
            responseObject.CharacterName = "Testodron";
        }
    }
}
