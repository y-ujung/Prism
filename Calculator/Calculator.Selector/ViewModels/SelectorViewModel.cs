using Calculator.Infra.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Selector.ViewModels
{
    public class SelectorViewModel : BindableBase
    {
        public ICalcEngine CalcEngine { get; private set; }

        public SelectorViewModel(ICalcEngine calc)
        {
            CalcEngine = calc;

        }
    }
}
