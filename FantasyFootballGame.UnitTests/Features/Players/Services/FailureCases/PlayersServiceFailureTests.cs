using FantasyFootballGame.UnitTests.AutoFixture;
using Moq;
using AutoFixture.Xunit2;
using FantasyFootballGame.Application.DTOs.Players;
using FluentAssertions;
using FantasyFootballGame.Application.Services.Players;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.UnitTests.Features.Players.Services.FailureCases
{
    public class PlayersServiceFailureTests
    {

        [Theory]
        [AutoMoqData]
        public async Task GetById_WhenPlayerNotFound_ThrowsKeyNotFoundException(
            int id,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            [Greedy] PlayersService sut
            )
        {
            //Arrange             
            repositoryMock.Setup(x => x.GetById(id))
                .ReturnsAsync((Player?)null);

            // Act            
            var act = () => sut.GetById(id);

            //Assert 
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_WhenValidationFails_ThrowsArgumentException(
            CreatePlayerDto createPlayerDto,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            PlayersService sut
            )
        {
            //Arrange             
            createPlayerDto.Name = string.Empty; // Invalid data
            repositoryMock.Setup(x => x.Create(It.IsAny<Player>()))
                .Throws<ArgumentException>();

            // Act            
            var act = () => sut.Create(createPlayerDto);

            //Assert 
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_WhenPlayerNotFound_ThrowsKeyNotFoundException(
            int id,
            UpdatePlayerDto updatePlayerDto,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            PlayersService sut
            )
        {
            //Arrange             
            repositoryMock.Setup(x => x.GetById(id))
                .ReturnsAsync((Player?)null);

            // Act            
            var act = () => sut.Update(id, updatePlayerDto);

            //Assert 
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_WhenValidationFails_ThrowsArgumentException(
            Player player,
            UpdatePlayerDto updatePlayerDto,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            PlayersService sut
            )
        {
            //Arrange
            updatePlayerDto.Name = string.Empty; // Invalid data
            repositoryMock.Setup(x => x.GetById(player.Id))
                .ReturnsAsync(player);
            repositoryMock.Setup(x => x.Update(It.IsAny<Player>()))
                .Throws<ArgumentException>();

            // Act            
            var act = () => sut.Update(player.Id, updatePlayerDto);

            //Assert 
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Delete_WhenPlayerNotFound_ThrowsKeyNotFoundException(
            int id,
            [Frozen] Mock<IPlayersRepository> repositoryMock,
            PlayersService sut
            )
        {
            //Arrange             
            repositoryMock.Setup(x => x.GetById(id))
                .ReturnsAsync((Player?)null);

            // Act            
            var act = () => sut.Delete(id);

            //Assert 
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        
    }
}