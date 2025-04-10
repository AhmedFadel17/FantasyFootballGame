using AutoFixture;
using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Application.Mapping;
using FluentValidation;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballGame.IntegrationTests.AutoFixture
{
    public class PaginationCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<PlayerResponseDto>(It.IsAny<Player>()))
                     .Returns((Player p) => new PlayerResponseDto
                     {
                         Id = p.Id,
                         Name = p.Name,
                         FullName = p.FullName,
                         ShirtNumber = p.ShirtNumber,
                         Position = p.Position.ToString(),
                         TeamId = p.TeamId,
                         Price = p.Price
                     });

            fixture.Customize<PaginationConverter<Player, PlayerResponseDto>>(composer =>
            {
                return composer.FromFactory(() => new PaginationConverter<Player, PlayerResponseDto>(mapperMock.Object));
            });

            fixture.Customize<PaginationDto<PlayerResponseDto>>(composer =>
            {
                var items = fixture.CreateMany<PlayerResponseDto>(5).ToList();
                return composer
                    .With(x => x.Items, items)
                    .With(x => x.TotalCount, items.Count)
                    .With(x => x.PageNumber, 1)
                    .With(x => x.PageSize, 10)
                    .With(x => x.TotalPages, 1);
            });
        }
    }
}
