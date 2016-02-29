using Dauber.Core;
using StructureMap;

namespace Dauber.Test
{
    public class ContextFixture
    {
        public ContextFixture()
        {
            Container = IoC.Container.CreateChildContainer();
        }

        public IContainer Container { get; }
    }
}