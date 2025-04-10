using FantasyFootballGame.UnitTests.AutoFixture;
using Moq;
using AutoFixture.Xunit2;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums;
using FluentAssertions;
using FantasyFootballGame.Application.Services.Players;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.DataAccess.Repositories.Players;


namespace FantasyFootballGame.UnitTests.Features.Players.Services.SuccessCases
{
    public class PlayersServiceSuccessTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetAll_WithFilters_ReturnsPaginationDto(
                int page,
                int pageSize,
                int? teamId,
                string? name,
                int? shirtNumber,
                PlayerStatus? status,
                PlayerPosition? position,
                double? minPrice,
                double? maxPrice,
                List<Player> players,
                [Frozen] Mock<IPlayersRepository> repositoryMock,
                [Greedy] PlayersService sut
            )
        {
            // Arrange
            int totalCount = players.Count;

            repositoryMock
                .Setup(x => x.GetAllWithPaginationAndFilters(page, pageSize, teamId, shirtNumber, name, status, position, minPrice, maxPrice))
                .ReturnsAsync((players, totalCount));

            // Act
            var result = await sut.AllWithPaginationAndFilters(
                page, pageSize, teamId, shirtNumber, name, status, position, minPrice, maxPrice);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(players.Count);
            result.TotalCount.Should().Be(players.Count);
            result.PageNumber.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
        }


        [Theory]
        [AutoMoqData]
        public async Task GetById_WhenPlayerExists_ReturnsPlayer(
            Player player,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            [Greedy] PlayersService sut
            )
        {
            
            int id =player.Id;
            //Arrange             
            repositoryMock.Setup(x => x.GetById(id))
                .ReturnsAsync(player);

            // Act            
            var result = await sut.GetById(id);

            //Assert 
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_WithValidData_ReturnsCreatedPlayer(
            CreatePlayerDto createPlayerDto,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            [Greedy] PlayersService sut
            )
        {
            //Arrange             
            repositoryMock.Setup(x => x.Create(It.IsAny<Player>()))
                       .Returns(Task.CompletedTask);

            repositoryMock.Setup(x => x.Save()).Returns(Task.CompletedTask);

            // Act            
            var result = await sut.Create(createPlayerDto);

            //Assert 
            result.Should().NotBeNull();
            repositoryMock.Verify(x => x.Create(It.IsAny<Player>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_WithValidData_ReturnsUpdatedPlayer(
            int id,
            UpdatePlayerDto updatePlayerDto,
            Player existingPlayer,
            Player updatedPlayer,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            [Greedy] PlayersService sut
            )
        {
            //Arrange             
            repositoryMock.Setup(x => x.GetById(id))
                .ReturnsAsync(existingPlayer);
            repositoryMock.Setup(x => x.Update(It.IsAny<Player>()))
                .Verifiable();

            // Act            
            var result = await sut.Update(id, updatePlayerDto);

            //Assert 
            result.Should().NotBeNull();
            repositoryMock.Verify(x => x.Update(It.IsAny<Player>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Delete_WhenPlayerExists_DeletesSuccessfully(
            int id,
            Player player,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            [Greedy] PlayersService sut
            )
        {
            //Arrange             
            repositoryMock.Setup(x => x.GetById(id))
                .ReturnsAsync(player);
            repositoryMock.Setup(x => x.Delete(It.IsAny<Player>()))
                .Verifiable();

            // Act            
            await sut.Delete(id);

            //Assert 
            repositoryMock.Verify(x => x.Delete(It.IsAny<Player>()), Times.Once);
        }
    }
}