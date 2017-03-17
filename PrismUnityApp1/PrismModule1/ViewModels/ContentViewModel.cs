using Prism.Commands;
using Prism.Mvvm;
using PrismInfra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PrismModule1.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        public IEngine Engine {get; private set; }

        public ICommand LoadImageCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        public ICommand ResizeImageCommand { get; set; }
        public ICommand CropTransparentImageCommand { get; set;}
        public ICommand CropImageCommand { get; set; }
        public ICommand MergeImageCommand { get; set; }

        public ContentViewModel(IEngine engine)
        {
            Engine = engine;

            LoadImageCommand = new DelegateCommand<object>(s => Engine.LoadImage());
            SaveImageCommand = new DelegateCommand<object>(s => Engine.SaveImage());
            ResizeImageCommand = new DelegateCommand<object>(s => Engine.ResizeImage(s));
            CropImageCommand = new DelegateCommand<object>(s => Engine.CropImage());
            CropTransparentImageCommand = new DelegateCommand<object>(s => Engine.CropTransparentImage());
            MergeImageCommand = new DelegateCommand<object>(s => Engine.MergeImage());
        }

    }
}

