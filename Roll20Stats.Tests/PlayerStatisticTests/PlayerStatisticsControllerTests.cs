using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Roll20Stats.ApplicationLayer.Commands.AddPlayerStatistic;
using Roll20Stats.ApplicationLayer.Queries.SinglePlayerStatistic;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.PresentationLayer.DataTransferObjects;
using Roll20Stats.Tests.Shared;
using Xunit;

namespace Roll20Stats.Tests.PlayerStatisticTests
{
    public class PlayerStatisticsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        
        public PlayerStatisticsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = TestDatabaseManager.SetupInMemoryDatabase(factory, "playerstatistics-database");
        }

        [Fact]
        public async Task Gets_Single_PlayerStatistic()
        {
            var game = new Game { Name = "Game", Id = 1 };
            var stat = new PlayerStatistic
            {
                CharacterId = "Id2",
                CharacterName = "Testosteron",
                Game = game,
                DamageDealt = 1,
                DamageTaken = 2
            };
            TestDatabaseManager.SeedDatabase(_factory, stat);
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/playerstatistics/Id2/Game");

            response.EnsureSuccessStatusCode();
            var responseObject = JsonConvert.DeserializeObject<GetPlayerStatisticDto>(await response.Content.ReadAsStringAsync());
            responseObject.Should().BeEquivalentTo(new GetPlayerStatisticDto
            {
                GameName = "Game",
                CharacterId = "Id2",
                CharacterName = "Testosteron",
                DamageDealt = 1,
                DamageTaken = 2
            });
        }

        [Fact]
        public async Task Returns_Not_Found_When_Statistic_Does_Not_Exist()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/playerstatistics/Id/falseName");

            var responseObject = JsonConvert.DeserializeObject<ResponseWithMetaData<PlayerStatistic>>(await response.Content.ReadAsStringAsync());
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseObject.ErrorMessage.Should().Be(@"Player with id ""Id"" and game name ""falseName"" was not found.");
        }

        [Fact]
        public async Task Gets_All_PlayerStatistic()
        {
            var stats = new[]
            {
                new PlayerStatistic
                {
                    CharacterId = "Id",
                    CharacterName = "Testosteron"
                },
                new PlayerStatistic
                {
                    CharacterId = "Id1",
                    CharacterName = "Testosteron1"
                }
            };
            TestDatabaseManager.SeedDatabase(_factory, stats);
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/playerstatistics");

            response.EnsureSuccessStatusCode();
            var responseObject = JsonConvert.DeserializeObject<PlayerStatistic[]>(await response.Content.ReadAsStringAsync());
            responseObject.Should().BeEquivalentTo(stats, option
                => option.Excluding(statistic => statistic.Id));
        }

        [Fact]
        public async Task Creates_PlayerStatistic_And_Corresponding_Game()
        {
            var client = _factory.CreateClient();
            var request = new AddPlayerStatisticCommand
            {
                CharacterId = "Id",
                CharacterName = "Name",
                GameName = "GameName",
                DamageDealt = 1,
                DamageTaken = 2
            };
            var requestBody = new StringContent(JsonConvert.SerializeObject(request));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync("/api/playerstatistics", requestBody);
            var getResponse = await client.GetAsync("/api/playerstatistics/Id/GameName");
            var gameGetResponse = await client.GetAsync("/api/games/GameName");

            response.EnsureSuccessStatusCode();
            var getResponseObject = JsonConvert.DeserializeObject<GetPlayerStatisticDto>(await getResponse.Content.ReadAsStringAsync());
            var createResponseObject = JsonConvert.DeserializeObject<AddPlayerStatisticDto>(await response.Content.ReadAsStringAsync());
            var gameGetResponseObject = JsonConvert.DeserializeObject<GameDto>(await gameGetResponse.Content.ReadAsStringAsync());
            getResponseObject.Should().BeEquivalentTo(request);
            createResponseObject.Should().BeEquivalentTo(request);
            gameGetResponseObject.Name.Should().Be("GameName");
        }

        [Fact]
        public async Task Adds_to_Existing_Player_Statistic()
        {
            var client = _factory.CreateClient();
            var stat = new PlayerStatistic
            {
                CharacterId = "Id",
                CharacterName = "Testosteron",
                DamageDealt = 1,
                DamageTaken = 2,
                Game = new Game
                {
                    Name = "GameName"
                }
            };
            TestDatabaseManager.SeedDatabase(_factory, stat);
            var request = new AddPlayerStatisticCommand
            {
                CharacterId = "Id",
                CharacterName = "Name",
                DamageDealt = 1,
                DamageTaken = 2,
                GameName = "GameName"
            };
            var requestBody = new StringContent(JsonConvert.SerializeObject(request));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync("/api/playerstatistics", requestBody);
            var getResponse = await client.GetAsync("/api/playerstatistics/Id/GameName");

            response.EnsureSuccessStatusCode();
            var getResponseObject = JsonConvert.DeserializeObject<GetPlayerStatisticDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseObject.Should().BeEquivalentTo(new GetPlayerStatisticDto
            {
                CharacterId = "Id",
                CharacterName = "Testosteron",
                DamageDealt = 2,
                DamageTaken = 4,
                GameName = "GameName"
            });

        }

        [Fact]
        public async Task Deletes_PlayerStatistic()
        {
            var stats = new[]
            {
                new PlayerStatistic
                {
                    CharacterId = "Id",
                    CharacterName = "Testosteron",
                    Game = new Game { Name = "GameName" }
                },
                new PlayerStatistic
                {
                    CharacterId = "Id1",
                    CharacterName = "Testosteron1"
                }
            };
            TestDatabaseManager.SeedDatabase(_factory, stats);
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/playerstatistics/Id/GameName");
            var getResponse = await client.GetAsync("/api/playerstatistics");

            response.EnsureSuccessStatusCode();
            var responseObject = JsonConvert.DeserializeObject<PlayerStatistic[]>(await getResponse.Content.ReadAsStringAsync());
            responseObject.Should().HaveCount(1);
            responseObject.First().Should().BeEquivalentTo(stats[1], option
                => option.Excluding(statistic => statistic.Id));
        }
    }
}
