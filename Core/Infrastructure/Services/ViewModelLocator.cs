using Autofac;
using Autofac.Core;
using System;

namespace TMDb.Core
{
    public class ViewModelLocator
    {
        IContainer _container;
        ContainerBuilder _containerBuilder;

        static readonly ViewModelLocator _instance = new ViewModelLocator();
        public static ViewModelLocator Instance => _instance;

        public ViewModelLocator()
            => _containerBuilder = new ContainerBuilder();

        public T Resolve<T>()
            => _container.Resolve<T>();

        public object Resolve(Type type)
            => _container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
            => _containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
           => _containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();

        public void RegisterModules(IModule[] platformSpecificModules)
        {
            foreach (var platformSpecificModule in platformSpecificModules)
                _containerBuilder.RegisterModule(platformSpecificModule);
        }

        public void Register<T>() where T : class
            => _containerBuilder.RegisterType<T>();

        public void Build()
        {
            if (_container == null)
                _container = _containerBuilder.Build();
        }
    }
}
