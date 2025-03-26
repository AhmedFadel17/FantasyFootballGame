using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace FantasyFootballGame.UnitTests.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(params string[] values) : base(() => CreateFixture(values))
        {
            
        }

        public static IFixture CreateFixture(params string[] values)
        {
            Fixture fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(
                new MapperCustomization()
            ));

            return fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

    }
}
