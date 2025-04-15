using FantasyFootballGame.Application.DTOs.Common;
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
    public class PlayersFailureTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;
        private readonly string _playersUrl;
        private readonly string _teamsUrl;

        public PlayersFailureTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            using var scope = factory.Services.CreateScope();
            _authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            _playersUrl = "/api/players";
            _teamsUrl = "/api/teams";
        }

        [Theory]
        [AutoMoqData]
        public async Task GetById_Should_Return_NotFound(int id)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_playersUrl}/{id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);

            var response = await _client.SendAsync(request);
            var players = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }


        [Theory]
        [AutoMoqData]
        public async Task Create_Should_Return_ValidationErrors(CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            dto.Name = "";
            dto.ShirtNumber = 1000;
            dto.Price = 1000;
            dto.Position = "ssss";
            // Act
            var response = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.UnprocessableContent);
        }

       

        [Theory]
        [AutoMoqData]
        public async Task Update_Should_Return_ValidationErrors(CreateTeamDto teamDto, UpdatePlayerDto updatePlayerDto, CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            var createTeamResponse = await IntegrationTestHelper.CreateItem(_client, _teamsUrl, teamDto, token);
            var createdTeam = await createTeamResponse.Content.ReadAsJsonAsync<TeamResponseDto>();
            dto.TeamId = createdTeam.Id;
            var createResponse = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var createdPlayer = await createResponse.Content.ReadAsJsonAsync<PlayerResponseDto>();

            updatePlayerDto.Name = "s";
            updatePlayerDto.ShirtNumber = 1000;
            updatePlayerDto.Price = 1000;
            updatePlayerDto.Position = "s";
            // Act
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_playersUrl}/{createdPlayer.Id}")
            {
                Content = updatePlayerDto.ReadAsJsonContent()
            };
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.UnprocessableContent);

            await IntegrationTestHelper.DeleteItem(_client, _playersUrl, createdPlayer.Id, token);
            await IntegrationTestHelper.DeleteItem(_client, _teamsUrl, createdTeam.Id, token);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Should_Return_NotFound(int id, UpdatePlayerDto updatePlayerDto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            updatePlayerDto.TeamId = null;
            updatePlayerDto.Name = null;
            updatePlayerDto.ShirtNumber = null;
            updatePlayerDto.Price = null;
            updatePlayerDto.Position = null;
            // Act
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_playersUrl}/{id}")
            {
                Content = updatePlayerDto.ReadAsJsonContent()
            };
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }


        [Theory]
        [AutoMoqData]
        public async Task Delete_Should_Return_NotFound(int id)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_playersUrl}/{id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

    }
}
