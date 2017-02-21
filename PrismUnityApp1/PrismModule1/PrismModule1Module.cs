using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace PrismModule1
{
    public class PrismModule1Module : IModule
    {
        IRegionManager _regionManager;

        public PrismModule1Module(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Content", typeof(PrismModule1.Views.Content));
        }
    }
}