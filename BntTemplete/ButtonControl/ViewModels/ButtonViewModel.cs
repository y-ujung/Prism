using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ButtonControl.ViewModels
{
    public class ButtonViewModel : BindableBase
    {
        public Model MyModel { get; set; }
        public ICommand MouseRightButtonDownCommand { get; set; }

        public ButtonViewModel()
        {
            var bound = new Rect(0, 0, 40, 40);
            var color = new SolidColorBrush(Colors.Red);
            MyModel = new Model(bound, color, Model.SHAPE_TYPE.CIRCLE);

            MouseRightButtonDownCommand = new DelegateCommand<object>(MouseRightButtonDown);
        }

        private void MouseRightButtonDown(object obj)
        {
            MessageBox.Show("MouseRightButtonDownCommand");
        }
    }
}
