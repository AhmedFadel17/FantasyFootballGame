using AutoFixture;
using AutoMapper;
using FantasyFootballGame.Application.Mapping;

namespace FantasyFootballGame.UnitTests.AutoFixture
{
    public class MapperCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();

            }).CreateMapper();

            fixture.Customize<IMapper>(x => x.FromFactory(() => mapper));

        }
    }
}
