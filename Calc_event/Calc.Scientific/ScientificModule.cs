using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.Scientific
{
    public class ScientificModule : IModule
    {
        private IUnityContainer _container;
        IRegionManager _regionManager;

        public ScientificModule(IUnityContainer container, RegionManager regionManager)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion("Scientific", typeof(Calc.Scientific.Views.Scientific));
            _container.RegisterType<object, Views.Scientific>("Scientific");
        }
    }
}