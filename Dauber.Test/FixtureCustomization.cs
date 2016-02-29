using Ploeh.AutoFixture;

namespace Dauber.Test
{
    public class FixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var contextFixture = new ContextFixture();

            fixture.Register(() => contextFixture);

            fixture.Customizations.Add(new FakeBuilder(contextFixture.Container));
            fixture.Customizations.Add(new ContainerBuilder(contextFixture.Container));
        }
    }
}