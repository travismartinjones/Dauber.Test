using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dauber.Core;
using Fixie;
using Ploeh.AutoFixture.Kernel;

namespace Dauber.Test
{
    public abstract class TestConvention : Convention
    {
        protected abstract string[] Assemblies { get; }

        protected TestConvention()
        {
            Classes.NameStartsWith("when_", "and_");

            Methods.Where(mi => mi.IsPublic && mi.IsVoid());

            ClassExecution
                .CreateInstancePerClass()
                .UsingFactory(CreateFromFixture);

            IoC.Setup(Assemblies);                      
        }

        private object CreateFromFixture(Type type)
        {
            var fixture = new Ploeh.AutoFixture.Fixture().Customize(new FixtureCustomization());

            return new SpecimenContext(fixture).Resolve(type);
        }
    }
}
