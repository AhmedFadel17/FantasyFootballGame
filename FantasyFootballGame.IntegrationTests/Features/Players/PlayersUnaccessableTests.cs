using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Domain.Enums.User;
using FantasyFootballGame.IntegrationTests.AutoFixture;
using FantasyFootballGame.IntegrationTests.Extensions;
using FantasyFootballGame.IntegrationTests.Helpers;
using FantasyFootballGame.IntegrationTests.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyFootballGame.IntegrationTests.Features.Players
{
    public class PlayersUnaccessableTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;
        private readonly string _playersUrl;

        public PlayersUnaccessableTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            using var scope = factory.Services.CreateScope();
            _authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            _playersUrl = "/api/players";
        }

       


        [Theory]
        [AutoMoqData]
        public async Task Create_Should_Return_Unaccessable(CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Player);
            // Act
            var response = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Should_Return_Unaccessable(int id, UpdatePlayerDto updatePlayerDto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Player);
            // Act
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_playersUrl}/{id}")
            {
                Content = updatePlayerDto.ReadAsJsonContent()
            };
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }


        [Theory]
        [AutoMoqData]
        public async Task Delete_Should_Return_Unaccessable(int id)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Player);
            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_playersUrl}/{id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }
    }
}
