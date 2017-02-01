using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calc.Selector.ViewModels
{
    public class SelectorViewModel : BindableBase
    {

        private bool? _calc1checked;
        public bool? Calc1Checked
        {
            get { return _calc1checked; }
            set
            {
                if (_calc1checked == value) return;

                SetProperty(ref _calc1checked, value);
                ViewNavigate();
            }
        }

        private bool? _calc2checked;
        public bool? Calc2Checked
        {
            get { return _calc2checked; }
            set {
                if (_calc2checked == value) return;

                SetProperty(ref _calc2checked, value);
                ViewNavigate();
            }
        }


        public IRegionManager RM { get; private set; }

        public SelectorViewModel(IRegionManager rm)
        {
            RM = rm;
        }

        private void ViewNavigate()
        {
            ClearViews();

            if (Calc1Checked == true)
            {
                RM.RequestNavigate("General", "Pad");
            }

            if(Calc2Checked == true)
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("result", "000");

                RM.RequestNavigate("General", "Pad");
                RM.RequestNavigate("Scientific", "Scientific");
            }

        }

        private void ClearViews()
        {
            RM.Regions["General"].RemoveAll();
            RM.Regions["Scientific"].RemoveAll();
        }
    }
}
