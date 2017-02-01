using Calc.Infra;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calc.General.ViewModels
{
    public class GeneralViewModel : BindableBase
    {

        public ICommand InputNumberCommand { get; set; }
        public ICommand InputOperationCommand { get; set; }
        public ICommand InputPointCommand { get; set; }
        public ICommand InputEqualCommand { get; set; }
        public ICommand InputClearCommand { get; set; }
        public ICommand InputDeleteCommand { get; set; }
        public ICommand InputBracketCommand { get; set; }

        public GeneralViewModel(IEventAggregator ea)
        {
            InputNumberCommand = new DelegateCommand<object>(s => ea.GetEvent<InputNumberEvent>().Publish(s));
            InputOperationCommand = new DelegateCommand<object>(s => ea.GetEvent<InputOperationiEvent>().Publish(s));
            InputPointCommand = new DelegateCommand<object>(s => ea.GetEvent<InputPointEvent>().Publish(s));
            InputEqualCommand = new DelegateCommand<object>(s => ea.GetEvent<InputEqualEvent>().Publish(s));
            InputClearCommand = new DelegateCommand<object>(s => ea.GetEvent<InputClearEvent>().Publish(s));
            InputDeleteCommand = new DelegateCommand<object>(s => ea.GetEvent<InputDeleteEvent>().Publish(s));
            InputBracketCommand = new DelegateCommand<object>(s => ea.GetEvent<InputBracketEvent>().Publish(s));
        }
    }
}
