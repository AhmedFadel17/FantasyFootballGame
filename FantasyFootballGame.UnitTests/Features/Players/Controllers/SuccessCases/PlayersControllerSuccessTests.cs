using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.UnitTests.AutoFixture;
using Moq;
using FantasyFootballGame.API.Controllers;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using FantasyFootballGame.Application.DTOs.Players;

namespace FantasyFootballGame.UnitTests.Features.Players.Controllers.SuccessCases
{
    public class PlayersControllerSuccessTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetAll(
            List<PlayerResponseDto> playerResponseDtos,
            [Frozen] Mock<IPlayersService> playersServiceMock,
            [Greedy] PlayersController sut
            )
        {
            //Arrange             
            playersServiceMock.Setup(s => s.All()).ReturnsAsync(playerResponseDtos);

            // Act            
            var res = await sut.GetAll();

            //Assert 
            res.Should().NotBeNull();
            res.Should().BeOfType<OkObjectResult>();
        }
    }
}
