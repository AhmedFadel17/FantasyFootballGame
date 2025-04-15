using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums.User;
using FantasyFootballGame.IntegrationTests.AutoFixture;
using FantasyFootballGame.IntegrationTests.Extensions;
using FantasyFootballGame.IntegrationTests.Services;

using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.IntegrationTests.Helpers;



namespace FantasyFootballGame.IntegrationTests.Features.Players
{
    public class PlayersSuccessTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;
        private readonly string _playersUrl;
        private readonly string _teamsUrl;

        public PlayersSuccessTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            using var scope = factory.Services.CreateScope();
            _authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            _playersUrl = "/api/players";
            _teamsUrl = "/api/teams";
        }

        [Theory]
        [AutoMoqData]
        public async Task GetAll_Should_Return_Players_List(CreateTeamDto teamDto,CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            var createTeamResponse = await IntegrationTestHelper.CreateItem(_client, _teamsUrl, teamDto, token);
            var createdTeam = await createTeamResponse.Content.ReadAsJsonAsync<TeamResponseDto>();
            dto.TeamId = createdTeam.Id;
            var createResponse = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var createdPlayer = await createResponse.Content.ReadAsJsonAsync<PlayerResponseDto>();
            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, _playersUrl);
            IntegrationTestHelper.AddDefaultHeaders(request, token);

            var response = await _client.SendAsync(request);
            var players = await response.Content.ReadAsJsonAsync<PaginationDto<PlayerResponseDto>>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            players.Should().NotBeNull();
            players.Items.Should().NotBeNull();
            players.Items.Should().NotBeEmpty();
            players.Items.Count.Should().Be(1);
            players.Items.First().Id.Should().Be(createdPlayer.Id);

            await IntegrationTestHelper.DeleteItem(_client, _playersUrl, createdPlayer.Id, token);
            await IntegrationTestHelper.DeleteItem(_client, _teamsUrl, createdTeam.Id, token);
        }


        [Theory]
        [AutoMoqData]
        public async Task GetById_Should_Return_Player(CreateTeamDto teamDto, CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            var createTeamResponse = await IntegrationTestHelper.CreateItem(_client, _teamsUrl, teamDto, token);
            var createdTeam = await createTeamResponse.Content.ReadAsJsonAsync<TeamResponseDto>();
            dto.TeamId = createdTeam.Id;
            var createResponse = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var createdPlayer = await createResponse.Content.ReadAsJsonAsync<PlayerResponseDto>();
            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_playersUrl}/{createdPlayer.Id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);

            var response = await _client.SendAsync(request);
            var players = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            players.Should().NotBeNull();
            players.Id.Should().Be(createdPlayer.Id);

            await IntegrationTestHelper.DeleteItem(_client, _playersUrl, createdPlayer.Id, token);
            await IntegrationTestHelper.DeleteItem(_client, _teamsUrl, createdTeam.Id, token);
        }


        [Theory]
        [AutoMoqData]
        public async Task Create_Should_Return_Player(CreateTeamDto teamDto, CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            var createTeamResponse = await IntegrationTestHelper.CreateItem(_client, _teamsUrl, teamDto, token);
            var createdTeam = await createTeamResponse.Content.ReadAsJsonAsync<TeamResponseDto>();
            dto.TeamId = createdTeam.Id;
            // Act
            var response = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var player = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            player.Should().NotBeNull();
            player.TeamId.Should().Be(dto.TeamId);
            player.Name.Should().Be(dto.Name);


            await IntegrationTestHelper.DeleteItem(_client, _playersUrl, player.Id, token);
            await IntegrationTestHelper.DeleteItem(_client, _teamsUrl, createdTeam.Id, token);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Should_Return_Player(CreateTeamDto teamDto,UpdatePlayerDto updatePlayerDto, CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            var createTeamResponse = await IntegrationTestHelper.CreateItem(_client, _teamsUrl, teamDto, token);
            var createdTeam = await createTeamResponse.Content.ReadAsJsonAsync<TeamResponseDto>();
            dto.TeamId = createdTeam.Id;
            updatePlayerDto.TeamId = dto.TeamId;
            var createResponse = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var createdPlayer = await createResponse.Content.ReadAsJsonAsync<PlayerResponseDto>();
            // Act
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_playersUrl}/{createdPlayer.Id}")
            {
                Content = updatePlayerDto.ReadAsJsonContent()
            };
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);
            var player = await response.Content.ReadAsJsonAsync<PlayerResponseDto>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            player.Should().NotBeNull();
            player.Id.Should().Be(createdPlayer.Id);
            player.Name.Should().Be(updatePlayerDto.Name);
            player.Price.Should().Be(updatePlayerDto.Price);
            await IntegrationTestHelper.DeleteItem(_client, _playersUrl, createdPlayer.Id, token);
            await IntegrationTestHelper.DeleteItem(_client, _teamsUrl, createdTeam.Id, token);
        }


        [Theory]
        [AutoMoqData]
        public async Task Delete_Should_Return_Success(CreateTeamDto teamDto, CreatePlayerDto dto)
        {
            // Arrange
            string token = await _authService.GetTokenAsync(UserRole.Moderator);
            var createTeamResponse = await IntegrationTestHelper.CreateItem(_client, _teamsUrl, teamDto, token);
            var createdTeam = await createTeamResponse.Content.ReadAsJsonAsync<TeamResponseDto>();
            dto.TeamId = createdTeam.Id;
            var createResponse = await IntegrationTestHelper.CreateItem(_client, _playersUrl, dto, token);
            var createdPlayer = await createResponse.Content.ReadAsJsonAsync<PlayerResponseDto>();
            // Act
            var request = new HttpRequestMessage(HttpMethod.Delete,$"{_playersUrl}/{createdPlayer.Id}");
            IntegrationTestHelper.AddDefaultHeaders(request, token);
            var response = await _client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            await IntegrationTestHelper.DeleteItem(_client, _playersUrl, createdPlayer.Id, token);
            await IntegrationTestHelper.DeleteItem(_client, _teamsUrl, createdTeam.Id, token);
        }
    }
}
