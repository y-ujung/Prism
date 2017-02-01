using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calculator.Selector
{
    [ModuleDependency("EngineModule")]
    public class SelectorModule : IModule
    {
        IRegionManager _regionManager;

        public SelectorModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Selector", typeof(Calculator.Selector.Views.Selector));
        }
    }
}