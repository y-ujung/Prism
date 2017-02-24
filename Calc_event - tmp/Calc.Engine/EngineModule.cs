using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.Engine
{
   // [Module(ModuleName = "EngineModule")]
    public class EngineModule : IModule
    {
        IUnityContainer _container;

        public EngineModule(IUnityContainer container, RegionManager regionManager)
        {
            _container = container;
        }

        public void Initialize()
        {
           // _container.RegisterType<CalcEngine>(new ContainerControlledLifetimeManager());
        }
    }
}