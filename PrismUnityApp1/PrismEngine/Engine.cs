using PrismInfra;
using ImageProcessor;
using ImageProcessor.Imaging;
using Prism.Mvvm;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.Windows;
using System.Drawing.PSD;

namespace PrismEngine
{
    class Engine : BindableBase, IEngine
    {
        private byte[] input;
        public byte[] Input
        {
            get { return input; }
            set { SetProperty(ref input, value); }
        }

        private MemoryStream process;
        public MemoryStream Process
        {
            get { return process; }
            set { SetProperty(ref process, value); }
        }

        private Rectangle rec;
        public Rectangle Rec
        {
            get { return rec; }
            set { SetProperty(ref rec, value); }
        }

        private int x;
        public int X
        {
            get { return x; }
            set { SetProperty(ref x, value); }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { SetProperty(ref y, value); }
        }

        private int w;
        public int W
        {
            get { return w; }
            set { SetProperty(ref w, value); }
        }

        private int h;
        public int H
        {
            get { return h; }
            set { SetProperty(ref h, value); }
        }

        public Engine()
        {
        }

        public void LoadImage()
        {
            Input = OpenImage();
            Bitmap img = new Bitmap(BytearrToImage(Input));
            Rec = new Rectangle(0, 0, img.Width, img.Height);
            X = Rec.X;
            Y = Rec.Y;
            W = Rec.Width;
            H = Rec.Height;

        }

        public void SaveImage()
        {
            using (var imageFactory = new ImageFactory(preserveExifData: true))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    string savePath = saveFileDialog1.FileName;
                    imageFactory.Load(Input)
                    .Save(savePath);
                }
            }

        }

        public void ResizeImage(object obj)
        {
            string str = (obj is string) ? obj as string : string.Empty;

            ImageProcessor.Imaging.ResizeMode mode;

            if (str == "max")
                mode = ImageProcessor.Imaging.ResizeMode.Max;
            else
                mode = ImageProcessor.Imaging.ResizeMode.Stretch;

            using (Process = new MemoryStream())
            {
                var size = new System.Drawing.Size(W, H);
                Rec = new Rectangle(Rec.X, Rec.Y, size.Width, size.Height);
                ResizeLayer resizeLayer = new ResizeLayer(size, mode, AnchorPosition.Center, true, null, null, null, null);

                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(Input)
                        .Resize(resizeLayer)
                        .Save(Process);
                }

                SaveProcess();

            }
        }

        public void CropImage()
        {
            using (Process = new MemoryStream())
            {
                Rec = new Rectangle(X, Y, W, H);
                var crop = new ImageProcessor.Imaging.CropLayer(Rec.X, Rec.Y, Rec.Width, Rec.Height, CropMode.Pixels);

                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(Input)
                        .Crop(crop)
                        .Save(Process);
                }

                SaveProcess();

            }

        }

        public void CropTransparentImage()
        {
            using (Process = new MemoryStream())
            {
                Rec = SaveRecInfo(new Bitmap(BytearrToImage(Input)));
                var crop = new ImageProcessor.Imaging.CropLayer(Rec.X, Rec.Y, Rec.Width, Rec.Height, CropMode.Pixels);

                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(Input)
                        .Crop(crop)
                        .Save(Process);
                }

                SaveProcess();

            }

        }

        public void MergeImage()
        {
            Bitmap img = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(img);

            Image source1 = BytearrToImage(Input);
            Image source2 = BytearrToImage(OpenImage());

            Rectangle Rec2 = new Rectangle(200, 300, source2.Width, source2.Height);

            g.DrawImage(source1, Rec);
            g.DrawImage(source2, Rec2);


            using (Process = new MemoryStream())
            {
                img.Save(Process, System.Drawing.Imaging.ImageFormat.Png);
                img = new Bitmap(Process);

                Rec = SaveRecInfo(img);
                var crop = new ImageProcessor.Imaging.CropLayer(Rec.X, Rec.Y, Rec.Width, Rec.Height, CropMode.Pixels);

                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(img)
                        .Crop(crop)
                        .Save(Process);
                }

                SaveProcess();
            }
        }

        // byte[] -> image
        private Image BytearrToImage(byte[] bytearr)
        {
            MemoryStream ms = new MemoryStream(bytearr);
            Image img = Image.FromStream(ms);
            return img;
        }

        //이미지 가져오기( + .psd)
        private byte[] OpenImage()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "jpg";
            openFile.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png; *.psd";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                string[] opneFileInfo = openFile.SafeFileName.Split('.');

                foreach (string filename in openFile.FileNames)
                {
                    if (opneFileInfo[1] == "psd")
                    {
                        var tmp = new PsdFile();
                        tmp.Load(filename);

                        using (Process = new MemoryStream())
                        {
                            using (var imageFactory = new ImageFactory(preserveExifData: true))
                            {
                                Bitmap img = ImageDecoder.DecodeImage(tmp);
                                imageFactory.Load(img)
                                    .Save(Process);
                            }
                            return Process.ToArray();
                        }
                    }
                    else
                    {
                        return File.ReadAllBytes(filename);
                    }
                }
            }
            return null;
        }

        private void SaveProcess()
        {
            Input = Process.ToArray();

            X = Rec.X;
            Y = Rec.Y;
            W = Rec.Width;
            H = Rec.Height;
        }

        private Rectangle SaveRecInfo(Bitmap img)
        {
            return new Rectangle(MinX(img), MinY(img), MaxX(img) - MinX(img), MaxY(img) - MinY(img));
        }

        private int MinX(Bitmap img)
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

        private int MinY(Bitmap img)
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

        private int MaxX(Bitmap img)
        {
            for (int x = img.Width - 1; x >= 0; x--)
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
            return img.Width;
        }

        private int MaxY(Bitmap img)
        {
            for (int y = img.Height - 1; y >= 0; y--)
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
            return img.Height;
        }
    }
}
