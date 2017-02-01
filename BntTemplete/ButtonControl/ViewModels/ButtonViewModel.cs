using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ButtonControl.ViewModels
{
    public class ButtonViewModel : BindableBase
    {
        public Model MyModel { get; set; }

        public ButtonViewModel()
        {
            var bound = new Rect(0, 0, 40, 40);
            var color = new SolidColorBrush(Colors.Red);
            MyModel = new Model(bound, color, Model.SHAPE_TYPE.CIRCLE);
            FrameworkElement
        }
    }
}
