using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calculator.Selector
{
    public class SeletorModule : IModule
    {
        IRegionManager _regionManager;

        public SeletorModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Selector", typeof(Calculator.Selector.Views.Selector));
        }
    }
}