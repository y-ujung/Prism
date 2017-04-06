using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Calc.Scientific
{
    public class ScientificModule : IModule
    {
        private IUnityContainer _container;

        public ScientificModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.Scientific>("Scientific");
        }
    }
}