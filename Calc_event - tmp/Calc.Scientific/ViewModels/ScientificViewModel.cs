using Calc.Infra;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calc.Scientific.ViewModels
{
    public class ScientificViewModel : BindableBase
    {
        public ICommand InputOperationCommand { get; set; }
        public ICommand InputFunctionParam1Command { get; set; }
        public ICommand InputFunctionParam2Command { get; set; }

        public ScientificViewModel(IEventAggregator ea, IRegionManager rm)
        {
            InputOperationCommand = new DelegateCommand<object>(s => ea.GetEvent<InputOperationiEvent>().Publish(s));
            InputFunctionParam1Command = new DelegateCommand<object>(s => ea.GetEvent<InputFunctionParam1Event>().Publish(s));
            InputFunctionParam2Command = new DelegateCommand<object>(s => ea.GetEvent<InputFunctionParam2Event>().Publish(s));
        }
    }
}
