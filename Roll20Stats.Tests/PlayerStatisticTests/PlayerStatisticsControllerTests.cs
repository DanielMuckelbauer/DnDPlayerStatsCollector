using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.Tests.Shared;
using Xunit;

namespace Roll20Stats.Tests.PlayerStatisticTests
{
    public class PlayerStatisticsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PlayerStatisticsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = TestDatabaseManager.SetupInMemoryDatabase(factory);
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
        public async Task Creates_PlayerStatistic()
        {
            var stat = new PlayerStatistic
            {
                CharacterId = "Id",
                CharacterName = "Testosteron",
                DamageTaken = 5
            };
            TestDatabaseManager.SeedDatabase(_factory, stat);
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/playerstatistics/Id");

            var responseObject = JsonConvert.DeserializeObject<PlayerStatistic>(await response.Content.ReadAsStringAsync());
            responseObject.CharacterId.Should().Be("Id");
            responseObject.CharacterName = "Testodron";
        }
    }
}
