using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using PrismInfra;
using System;

namespace PrismEngine
{
    [Module(ModuleName = "PrismEngineModule")]
    public class PrismEngineModule : IModule
    {
        IUnityContainer _container;

        public PrismEngineModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IEngine, Engine>(new ContainerControlledLifetimeManager());
        }
    }
}