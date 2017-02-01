using Calculator.Infra.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calculator.Scientific.ViewModels
{
    public class ScientificViewModel : BindableBase
    {
        public ICommand InputOperationCommand { get; set; }
        public ICommand InputFunctionParam1Command { get; set; }
        public ICommand InputFunctionParam2Command { get; set; }

        public ScientificViewModel(ICalcEngine calc)
        {
            InputOperationCommand = new DelegateCommand<object>(s => calc.InputOperation(s));
            InputFunctionParam1Command = new DelegateCommand<object>(s => calc.InputFunctionParam1(s));
            InputFunctionParam2Command = new DelegateCommand<object>(s => calc.InputFunctionParam2(s));
        }
    }
}
