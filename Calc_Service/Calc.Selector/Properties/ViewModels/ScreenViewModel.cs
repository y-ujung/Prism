using Calc.Infra.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calc.Screen.ViewModels
{
    public class ScreenViewModel : BindableBase
    {
        public ICalcEngine CalcEngine { get; private set; }

        public ScreenViewModel(ICalcEngine calc)
        {
            CalcEngine = calc;
        }
    }
}
