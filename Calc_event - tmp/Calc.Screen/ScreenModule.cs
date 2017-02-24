using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.Screen
{
    public class ScreenModule : IModule
    {
        IRegionManager _regionManager;

        public ScreenModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Screen", typeof(Calc.Screen.Views.Screen));
        }
    }
}