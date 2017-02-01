using Prism.Modularity;
using Prism.Regions;
using System;

namespace ContentControl
{
    public class ContentControlModule : IModule
    {
        IRegionManager _regionManager;

        public ContentControlModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(ContentControl.Views.Content));
        }
    }
}