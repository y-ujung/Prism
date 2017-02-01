using Calculator.Infra.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calculator.General.ViewModels
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

        public GeneralViewModel(ICalcEngine calc)
        {
            InputNumberCommand = new DelegateCommand<object>(s => calc.InputNumber(s));
            InputOperationCommand = new DelegateCommand<object>(s => calc.InputOperation(s));
            InputPointCommand = new DelegateCommand<object>(s => calc.InputPoint());
            InputEqualCommand = new DelegateCommand<object>(s => calc.InputEqual());
            InputClearCommand = new DelegateCommand<object>(s => calc.InputClear());
            InputDeleteCommand = new DelegateCommand<object>(s => calc.InputDelete());
            InputBracketCommand = new DelegateCommand<object>(s => calc.InputBracket(s));
        }

    }
}
