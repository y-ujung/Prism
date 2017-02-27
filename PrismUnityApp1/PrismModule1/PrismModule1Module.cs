using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace PrismModule1
{
    [ModuleDependency("PrismEngineModule")]
    public class PrismModule1Module : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public PrismModule1Module(IUnityContainer container, RegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.Content>("Content");
            _regionManager.RequestNavigate("Content", "Content");

        }
    }
}