using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calculator.Screen
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
            _regionManager.RegisterViewWithRegion("Screen", typeof(Calculator.Screen.Views.Screen));
        }
    }
}