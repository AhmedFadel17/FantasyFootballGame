using AutoFixture;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.Validators.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Linq.Expressions;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.UnitTests.AutoFixture
{
    public class ValidatorCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {

            var createValidatorMock = new Mock<IValidator<CreatePlayerDto>>();
            createValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<CreatePlayerDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new ValidationResult());

            var updateValidatorMock = new Mock<IValidator<UpdatePlayerDto>>();
            updateValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<UpdatePlayerDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new ValidationResult());

            fixture.Customize<IValidator<CreatePlayerDto>>(x => x.FromFactory(() => createValidatorMock.Object));
            fixture.Customize<IValidator<UpdatePlayerDto>>(x => x.FromFactory(() => updateValidatorMock.Object));
        }
    }
}
