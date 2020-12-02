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
                CharacterId = "Id2",
                CharacterName = "Testosteron"
            };
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    //services.Remove(services.FirstOrDefault(descriptor =>
                    //    descriptor.ServiceType == typeof(ApplicationContext)));
                    //services.AddDbContext<ApplicationContext>(contextOptionsBuilder =>
                    //    contextOptionsBuilder.UseInMemoryDatabase("StatsData"));
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetService<ApplicationContext>();
                    db.PlayerStatistics.Add(stat);
                    db.SaveChanges();
                });

            }).CreateClient();
           
            var response = await client.GetAsync("/api/playerstatistics/Id2");

            var responseObject = JsonConvert.DeserializeObject<PlayerStatistic>(await response.Content.ReadAsStringAsync());
            responseObject.Should().BeEquivalentTo(stat);

        }
    }
}
