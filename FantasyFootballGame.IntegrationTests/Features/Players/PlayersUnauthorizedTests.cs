using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.IntegrationTests.AutoFixture;
using FantasyFootballGame.IntegrationTests.Extensions;
using FantasyFootballGame.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;


namespace FantasyFootballGame.IntegrationTests.Features.Players
{
    public class PlayersUnauthorizedTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly string _playersUrl;

        public PlayersUnauthorizedTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            using var scope = factory.Services.CreateScope();
            _playersUrl = "/api/players";
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAll_Should_Return_Unauthorized()
        {
            // Arrange
            string token = null;
            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, _playersUrl);
            IntegrationTestHelper.AddDefaultHeaders(request, token);

            var response = await _client.SendAsync(request);
            var players = await response.Content.ReadAsJsonAsync<PaginationDto<PlayerResponseDto>>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }


        [Theory]
        [AutoMoqData]
        public async Task GetById_Should_Return_Unauthorized(int id)
        {
            // Arrange
            string token = null;

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_playersUrl}/{id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);

            var response = await _client.SendAsync(request);
            var players = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }


        [Theory]
        [AutoMoqData]
        public async Task Create_Should_Return_Unauthorized(CreatePlayerDto dto)
        {
            // Arrange
            string token = null;
            // Act
            var response = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var player = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Should_Return_Unauthorized(int id, UpdatePlayerDto updatePlayerDto)
        {
            // Arrange
            string token = null;

            // Act
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_playersUrl}/{id}")
            {
                Content = updatePlayerDto.ReadAsJsonContent()
            };
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);
            var player = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

        }


        [Theory]
        [AutoMoqData]
        public async Task Delete_Should_Return_Unauthorized(int id)
        {
            // Arrange
            string token = null;
            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_playersUrl}/{id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
