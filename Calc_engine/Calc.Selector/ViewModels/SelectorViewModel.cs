using Calc.Infra.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Calc.Selector.ViewModels
{
    public class SelectorViewModel : BindableBase
    {
        public bool? calc1checked;
        public bool? Calc1Checked
        {
            get { return calc1checked; }
            set
            {
                if (calc1checked == value) return;

                SetProperty(ref calc1checked, value);
                ViewNavigate();
            }
        }

        public bool? calc2checked;
        public bool? Calc2Checked
        {
            get { return calc2checked; }
            set
            {
                if (calc2checked == value) return;

                SetProperty(ref calc2checked, value);
                ViewNavigate();
            }
        }

        public IRegionManager RM { get; set; }

        public SelectorViewModel(IRegionManager rm)
        {
            RM = rm;
        }

        private void ViewNavigate()
        {
            RM.Regions["General"].RemoveAll();
            RM.Regions["Scientific"].RemoveAll();

            if (Calc1Checked == true)
            {
                RM.RequestNavigate("General", "General");
            }
            if(Calc2Checked == true)
            {
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("result", "0");

                RM.RequestNavigate("General", "General");
                RM.RequestNavigate("Scientific", "Scientific", parameters);
            }
        }
    }
}
