﻿using AutoFixture;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;
using Moq;

namespace FantasyFootballGame.IntegrationTests.AutoFixture.ModelsCustomizations
{
    public class PlayerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var random = new Random();

            fixture.Customize<PlayerResponseDto>(composer =>
            {
                return composer;
            });

            fixture.Customize<CreatePlayerDto>(composer =>
            {
                return composer
                    .With(t => t.Price, random.Next(1,99))
                    .With(t => t.ShirtNumber, random.Next(1, 99))
                    .With(t => t.Position, nameof(PlayerPosition.Forward));
            });

            fixture.Customize<UpdatePlayerDto>(composer =>
            {
                return composer
                    .With(t => t.Price, random.Next(1, 99))
                    .With(t => t.ShirtNumber, random.Next(1, 99))
                    .With(t => t.Position, nameof(PlayerPosition.Forward))
                    .With(t => t.Status, nameof(PlayerStatus.Available));
            });

            fixture.Customize<Player>(composer =>
            {
                return composer
                    .With(t => t.Team, It.IsAny<Team>());
            });
        }
    }
}