using Prism.Modularity;
using Prism.Regions;
using System;

namespace ButtonControl
{
    public class ButtonControlModule : IModule
    {
        IRegionManager _regionManager;

        public ButtonControlModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ButtonRegion", typeof(ButtonControl.Views.Button));
        }
    }
}