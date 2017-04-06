using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.General
{
    public class GeneralModule : IModule
    {
        private IUnityContainer _container;

        public GeneralModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.General>("General");

        }
    }
}