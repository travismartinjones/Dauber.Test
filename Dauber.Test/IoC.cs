using System;
using System.Collections.Generic;
using System.Threading;
using StructureMap;

namespace Dauber.Core
{
    /// <summary>
    /// Replace this implementation with references to your own container.
    /// </summary>
    public static class IoC
    {
        private static readonly Lazy<StructureMap.Container> _containerBuilder =
            new Lazy<StructureMap.Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static StructureMap.Container defaultContainer()
        {
            return new StructureMap.Container(x =>
            {
                // default config
            });
        }

        public static T TryGetInstance<T>()
        {
            return Container.TryGetInstance<T>();
        }

        public static object TryGetInstance(Type type)
        {
            if (type == null) return null;
            return Container.TryGetInstance(type);
        }

        public static T GetInstance<T>()
        {
            return Container.TryGetInstance<T>();
        }

        public static object GetInstance(Type type)
        {
            if (type == null) return null;
            return Container.GetInstance(type);
        }

        public static IEnumerable<T> GetAllInstances<T>()
        {
            return Container.GetAllInstances<T>();
        }

        public static T GetInstance<T>(string name)
        {
            return Container.GetInstance<T>(name);
        }

        public static void Inject<T>(T instance) where T : class
        {
            Container.Inject(typeof(T), instance);
        }

        public static void Setup(params string[] assemblies)
        {
            Container.Configure(initalizer =>
            {
                initalizer.Scan(scanner =>
                {
                    scanner.WithDefaultConventions();
                    scanner.LookForRegistries();
                    scanner.RegisterConcreteTypesAgainstTheFirstInterface();

                    foreach (var assemblyName in assemblies)
                    {
                        scanner.Assembly(assemblyName);
                    }
                });
            });
        }
    }
}