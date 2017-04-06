using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace JsonXml.Content
{
    public class ContentModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ContentModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.Content>("Content");
            _regionManager.RequestNavigate("ContentRegion", "Content");
        }
    }
}