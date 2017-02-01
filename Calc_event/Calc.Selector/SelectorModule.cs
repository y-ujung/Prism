using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.Selector
{
    public class SelectorModule : IModule
    {
        IRegionManager _regionManager;

        public SelectorModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Selector", typeof(Calc.Selector.Views.Selector));

        }
    }
}