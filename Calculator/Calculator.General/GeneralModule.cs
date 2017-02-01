using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calculator.General
{
    public class GeneralModule : IModule
    {
        IRegionManager _regionManager;

        public GeneralModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("General", typeof(Calculator.General.Views.General));
        }
    }
}