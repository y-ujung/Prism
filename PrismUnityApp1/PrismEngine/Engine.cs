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

        public Engine()
        {
        }

        public void LoadImage()
        {
            Input = OpenImage();
        }

        public void SaveImage()
        {
            using (var imageFactory = new ImageFactory(preserveExifData: true))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif Image|*.png";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    string savePath = saveFileDialog1.FileName;
                    imageFactory.Load(Output)
                    .Save(savePath);
                }
            }

        }

        public void ResizeImage()
        {
            var size = new System.Drawing.Size(1500,1000);
            ResizeLayer resizeLayer = new ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Stretch, AnchorPosition.Center, true, null, null, null, null);

            using (Process = new MemoryStream())
            {
                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(Input)
                        .Resize(resizeLayer)
                        .Save(Process);
                }
                Output = Process.ToArray();
            }
        }

        public void CropImage()
        {
            Bitmap img = new Bitmap(BytearrToImage(Input));

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

        public void MergeImage()
        {
            Bitmap img = new Bitmap(1000, 1000, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(img);

            Image source1 = BytearrToImage(Input);
            Image source2 = BytearrToImage(OpenImage());
            g.DrawImage(source1, 0, 0);
            g.DrawImage(source2, 250, 250);

            var crop = new ImageProcessor.Imaging.CropLayer(MinX(img), MinY(img), MaxX(img) - MinX(img), MaxY(img) - MinY(img), CropMode.Pixels);

            var size = new System.Drawing.Size(MaxX(img) - MinX(img), MaxY(img) - MinY(img));
            ResizeLayer resizeLayer = new ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Stretch, AnchorPosition.Center, true, null, null, null, null);

            using (Process = new MemoryStream())
            {
                img.Save(Process, source1.RawFormat);
                img = new Bitmap(Process);

                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(img)

                        .Crop(crop)
                      .Resize(resizeLayer)
                        .Save(Process);
                }
                Output = Process.ToArray();
            }

            g.Dispose();
        }

        // byte[] -> image
        private Image BytearrToImage(byte[] bytearr)
        {
            MemoryStream ms = new MemoryStream(bytearr);
            Image img = Image.FromStream(ms);
            return img;
        }

        // image -> byte[]
        private byte[] ImageToBytearr(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
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
            for (int x = img.Width; x >= 0; x--)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    System.Drawing.Color col = img.GetPixel(x - 1, y);
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
            for (int y = img.Height; y >= 0; y--)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    System.Drawing.Color col = img.GetPixel(x, y - 1);
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
