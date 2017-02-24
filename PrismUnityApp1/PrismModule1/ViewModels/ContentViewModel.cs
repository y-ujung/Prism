using ImageProcessor;
using ImageProcessor.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PrismModule1.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        private string savePath;
        public string SavePath
        {
            get { return savePath; }
            set { SetProperty(ref savePath, value); }
        }

        private string loadPath;
        public string LoadPath
        {
            get { return loadPath; }
            set { SetProperty(ref loadPath, value); }
        }

        private byte[] input;
        public byte[] Input
        {
            get { return input; }
            set { SetProperty(ref input, value); }
        }

        private byte[] output;
        public byte[] Output
        {
            get { return output; }
            set { SetProperty(ref output, value); }
        }

        private MemoryStream process;
        public MemoryStream Process
        {
            get { return process; }
            set { SetProperty(ref process, value); }
        }

        public ICommand LoadImageCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        public ICommand ResizeImageCommand { get; set; }
        public ICommand CropImageCommand { get; set; }

        public ContentViewModel()
        {
            LoadPath = "C:\\Users\\NSTL_DH\\Desktop\\image1.png";

            LoadImageCommand = new DelegateCommand<object>(LoadImage);
            SaveImageCommand = new DelegateCommand<object>(SaveImage);
            ResizeImageCommand = new DelegateCommand<object>(ResizeImage);
            CropImageCommand = new DelegateCommand<object>(CropImage);
        }

        private void LoadImage(object obj)
        {
            Input = File.ReadAllBytes(LoadPath);
            SavePath = "C:\\Users\\NSTL_DH\\Desktop\\123.png";
        }

        private void SaveImage(object obj)
        {
            using (var imageFactory = new ImageFactory(preserveExifData: true))
            {
                imageFactory.Load(Output)
                    .Save(SavePath);
            }

        }

        private void ResizeImage(object obj)
        {
            var size = new System.Drawing.Size(2000, 2000);

            using (Process = new MemoryStream())
            {
                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(Input)
                        .Resize(size)
                        .Save(Process);
                }
                Output = Process.ToArray();
            }
        }

        private void CropImage(object obj)
        {
            Bitmap img = new Bitmap(LoadPath);
            var crop = new ImageProcessor.Imaging.CropLayer(MinX(img), MinY(img), MaxX(img) - MinX(img), MaxY(img) - MinY(img), CropMode.Pixels);

            using (Process = new MemoryStream())
            {
                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(Input)
                        .Crop(crop)
                        .Save(Process);
                }
                Output = Process.ToArray();
            }

        }

        public int MinX(Bitmap img)
        {
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    System.Drawing.Color col = img.GetPixel(x, y);
                    if (col.A != 0)
                    {
                        return x;
                    }
                }
            }
            return 0;
        } 

        public int MinY(Bitmap img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    System.Drawing.Color col = img.GetPixel(x, y);
                    if (col.A != 0)
                    {
                        return y;
                    }
                }
            }
            return 0;
        }

        public int MaxX(Bitmap img)
        {
            for (int x = img.Width; x >= 0; x--)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    System.Drawing.Color col = img.GetPixel(x-1, y);
                    if (col.A != 0)
                    {
                        return x;
                    }
                }
            }
            return img.Width;
        }

        public int MaxY(Bitmap img)
        {
            for (int y = img.Height; y >= 0; y--)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    System.Drawing.Color col = img.GetPixel(x, y-1);
                    if (col.A != 0)
                    {
                        return y;
                    }
                }
            }
            return img.Height;
        }
    }
}

