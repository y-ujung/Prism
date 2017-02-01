using Calc.General.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.General
{
    public class GeneralModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;

        public GeneralModule(IUnityContainer container, RegionManager regionManager)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion("General", typeof(Calc.General.Views.General));
            //_regionManager.RegisterViewWithRegion("General", typeof(Calc.General.Views.Pad));

            //IRegion region = this._regionManager.Regions["General"];
            //Pad view = new Pad();
            //region.Add(view);
            //region.Activate(view);

            _container.RegisterType<object, Views.General>("General");
        }
    }
}