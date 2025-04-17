using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.UnitTests.AutoFixture;
using Moq;
using FantasyFootballGame.API.Controllers;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums;
using FluentAssertions;
using FantasyFootballGame.Application.DTOs.Common;

namespace FantasyFootballGame.UnitTests.Features.Players.Controllers.SuccessCases
{
    public class PlayersControllerSuccessTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetAll(
            int page,
            int pageSize,
            int? teamId,
            string? name,
            int? shirtNumber,
            PlayerStatus? status,
            PlayerPosition? position,
            double? minPrice,
            double? maxPrice,
            PaginationDto<PlayerResponseDto> paginationDto,
            [Frozen] Mock<IPlayersService> playersServiceMock,
            [Greedy] PlayersController sut
            )
        {
            //Arrange             
            playersServiceMock.Setup(s => s.AllWithPaginationAndFilters(page, pageSize, teamId, shirtNumber, name, status, position, minPrice, maxPrice))
                .ReturnsAsync(paginationDto);

            // Act            
            var res = await sut.GetAll(teamId, shirtNumber, name, status, position, minPrice, maxPrice, page, pageSize);

            //Assert 
            res.Should().NotBeNull();
            res.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [AutoMoqData]
        public async Task GetById(
            int id,
            PlayerResponseDto playerResponseDto,
            [Frozen] Mock<IPlayersService> playersServiceMock,
            [Greedy] PlayersController sut
            )
        {
            //Arrange             
            playersServiceMock.Setup(s => s.GetById(id)).ReturnsAsync(playerResponseDto);

            // Act            
            var res = await sut.GetById(id);

            //Assert 
            res.Should().NotBeNull();
            res.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Create(
            CreatePlayerDto createPlayerDto,
            PlayerResponseDto playerResponseDto,
            [Frozen] Mock<IPlayersService> playersServiceMock,
            [Greedy] PlayersController sut
            )
        {
            //Arrange             
            playersServiceMock.Setup(s => s.Create(createPlayerDto)).ReturnsAsync(playerResponseDto);

            // Act            
            var res = await sut.Create(createPlayerDto);

            //Assert 
            res.Should().NotBeNull();
            res.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Update(
            int id,
            UpdatePlayerDto updatePlayerDto,
            PlayerResponseDto playerResponseDto,
            [Frozen] Mock<IPlayersService> playersServiceMock,
            [Greedy] PlayersController sut
            )
        {
            //Arrange             
            playersServiceMock.Setup(s => s.Update(id, updatePlayerDto)).ReturnsAsync(playerResponseDto);

            // Act            
            var res = await sut.Update(id, updatePlayerDto);

            //Assert 
            res.Should().NotBeNull();
            res.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Delete(
            int id,
            [Frozen] Mock<IPlayersService> playersServiceMock,
            [Greedy] PlayersController sut
            )
        {
            //Arrange             
            playersServiceMock.Setup(s => s.Delete(id)).Returns(Task.CompletedTask);

            // Act            
            var res = await sut.Delete(id);

            //Assert 
            res.Should().NotBeNull();
            res.Should().BeOfType<OkObjectResult>();
        }
    }
}
