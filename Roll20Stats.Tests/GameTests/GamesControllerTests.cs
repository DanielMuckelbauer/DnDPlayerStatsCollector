using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Roll20Stats.InfrastructureLayer.DAL.Entities;
using Roll20Stats.PresentationLayer.DataTransferObjects;
using Roll20Stats.Tests.Shared;
using Xunit;

namespace Roll20Stats.Tests.GameTests
{
    public class GamesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GamesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = TestDatabaseManager.SetupInMemoryDatabase(factory, "game-database");
        }

        [Fact]
        public async Task Creates_Game_And_Gets_Single_Game()
        {
            var expected = new GameDto(0, "Testosteron");
            var client = _factory.CreateClient();
            var requestBody = new StringContent(JsonConvert.SerializeObject(expected));
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("/api/games/Testosteron", new StringContent(string.Empty));

            var getResponse = await client.GetAsync("/api/games/Testosteron");
            response.EnsureSuccessStatusCode();
            var getResponseObject = JsonConvert.DeserializeObject<GameDto>(await getResponse.Content.ReadAsStringAsync());
            var createResponseObject = JsonConvert.DeserializeObject<GameDto>(await response.Content.ReadAsStringAsync());
            createResponseObject.Should().BeEquivalentTo(expected, option
                => option.Excluding(dto => dto.Id));
            getResponseObject.Should().BeEquivalentTo(expected, option
                => option.Excluding(dto => dto.Id));
        }

        [Fact]
        public async Task Gets_All_Games()
        {
            TestDatabaseManager.SeedDatabase(_factory, new[]
            {
                new Game{Name = "Game1"},
                new Game{Name = "Game2"}
            });
            var expected = new[]
            {
                new GameDto(0, "Game1"),
                new GameDto(1, "Game2")
            };
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/games");

            response.EnsureSuccessStatusCode();
            var responseObject = JsonConvert.DeserializeObject<GameDto[]>(await response.Content.ReadAsStringAsync());
            responseObject.Should().BeEquivalentTo(expected, option
                => option.Excluding(dto => dto.Id));
        }

        [Fact]
        public async Task Trying_To_Get_Non_Existent_Game_Returns_Not_Found()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/games/Id");

            var responseObject = JsonConvert.DeserializeObject<ResponseWithMetaData<GameDto>>(await response.Content.ReadAsStringAsync());
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseObject.ErrorMessage.Should().Be(@"Game with name ""Id"" does not exist.");

        }

        [Fact]
        public async Task Trying_To_Create_Game_With_Same_Name_Returns_Conflict()
        {
            TestDatabaseManager.SeedDatabase(_factory, new Game { Name = "Name" });
            var client = _factory.CreateClient();

            var response = await client.PostAsync("/api/games/Name", new StringContent(string.Empty));

            var responseObject = JsonConvert.DeserializeObject<ResponseWithMetaData<GameDto>>(await response.Content.ReadAsStringAsync());
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            responseObject.ErrorMessage.Should().Be(@"Game with name ""Name"" already exists.");
        }
    }
}
