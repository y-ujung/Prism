using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calculator.Scientific
{
    public class ScientificModule : IModule
    {
        IRegionManager _regionManager;

        public ScientificModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Scientific", typeof(Calculator.Scientific.Views.Scientific));
        }
    }
}