using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
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
    }
}
