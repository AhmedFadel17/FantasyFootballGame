using AutoFixture;
using AutoMapper;
using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.Mapping;

namespace FantasyFootballGame.IntegrationTests.AutoFixture
{
    public class MapperCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            IMapper mapper = null!;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();

                // Setup generic mapping
                cfg.CreateMap(typeof(PaginationSource<>), typeof(PaginationDto<>));
            });

            // Create the mapper after we define the service constructor logic
            mapper = new Mapper(config, serviceType =>
            {
                if (serviceType.IsGenericType &&
                    serviceType.GetGenericTypeDefinition() == typeof(PaginationConverter<,>))
                {
                    var genericArgs = serviceType.GetGenericArguments();
                    var concreteType = typeof(PaginationConverter<,>).MakeGenericType(genericArgs);
                    return Activator.CreateInstance(concreteType, mapper)!;
                }

                return null!;
            });

            fixture.Customize<IMapper>(x => x.FromFactory(() => mapper));
        }
    }
}
