using AutoFixture;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Domain.Models;
using Moq;

namespace FantasyFootballGame.IntegrationTests.AutoFixture.ModelsCustomizations
{
    public class TeamCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {


            fixture.Customize<CreateTeamDto>(composer =>
            {
                return composer
                    .With(t => t.Name, "Real MAdrid")
                    .With(t => t.MainColor, "White")
                    .With(t => t.SecondaryColor, "White")
                    .With(t => t.Abbreviation, "Rma")
                    .With(t => t.ShirtImgSrc, "https//");


            });

            fixture.Customize<UpdateTeamDto>(composer =>
            {
                return composer
                .With(t => t.Name, "Real MAdrid")
                    .With(t => t.MainColor, "White")
                    .With(t => t.Abbreviation, "Rma")
                    .With(t => t.SecondaryColor, "White")
                    .With(t => t.ShirtImgSrc, "https//");

            });

            fixture.Customize<Team>(composer =>
            {
                return composer
                    .With(t => t.Players, []);
            });
        }
    }
}
