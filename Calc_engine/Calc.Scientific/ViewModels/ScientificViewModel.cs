using Calc.Infra.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calc.Scientific.ViewModels
{
    public class ScientificViewModel : BindableBase, INavigationAware
    {
        public ICommand InputOperationCommand { get; set; }
        public ICommand InputFunctionParam1Command { get; set; }
        public ICommand InputFunctionParam2Command { get; set; }
        public ICalcEngine CalcEngine { get; private set; }


        public ScientificViewModel(ICalcEngine calc)
        {
            CalcEngine = calc;
            InputOperationCommand = new DelegateCommand<object>(s => calc.InputOperation(s));
            InputFunctionParam1Command = new DelegateCommand<object>(s => calc.InputFunctionParam1(s));
            InputFunctionParam2Command = new DelegateCommand<object>(s => calc.InputFunctionParam2(s));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string v = (string)navigationContext.Parameters["result"];
            CalcEngine.Result = v;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
