using System;
using System.Reflection;
using FakeItEasy;
using Ploeh.AutoFixture.Kernel;
using StructureMap;

namespace Dauber.Test
{
    public class FakeBuilder : ISpecimenBuilder
    {
        private readonly IContainer _container;

        public FakeBuilder(IContainer container)
        {
            _container = container;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var paramInfo = request as ParameterInfo;

            if (paramInfo == null)
                return new NoSpecimen(request);
            
            if (!paramInfo.Name.StartsWith("fake") && !paramInfo.Name.EndsWith("fake"))
                return new NoSpecimen(request);

            var method = typeof(A)
                .GetMethod("Fake", Type.EmptyTypes)
                .MakeGenericMethod(paramInfo.ParameterType);

            var fake = method.Invoke(null, null);

            _container.Configure(cfg => cfg.For(paramInfo.ParameterType).Use(fake));

            return fake;
        }
    }
}