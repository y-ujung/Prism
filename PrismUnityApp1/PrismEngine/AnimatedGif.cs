using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismEngine
{
    public class AnimatedGif
    {
        public List<AnimatedGifFrame> Images { get; set; }

        public AnimatedGif(string path)
        {
            Images = new List<AnimatedGifFrame>();

            Image image = Image.FromFile(path);
            int frames = image.GetFrameCount(FrameDimension.Time);

            if (frames <= 1) throw new ArgumentException("Image not animated");
            byte[] times = image.GetPropertyItem(0x5100).Value;
            int frame = 0;

            for (;;)
            {
                int duration = BitConverter.ToInt32(times, 4 * frame);
                Console.WriteLine("Duration : " + duration);
                Images.Add(new AnimatedGifFrame(new Bitmap(image), duration));
                if (++frame >= frame)
                {
                    break;
                }
                image.SelectActiveFrame(FrameDimension.Time, frame);
            }
            image.Dispose();
            Console.WriteLine();
        }
    }

    public class AnimatedGifFrame
    {
        public int Duration { get; set; }
        public Image Image { get; set; }


        public AnimatedGifFrame(Image image, int duration)
        {
            Image = image;
            Duration = duration;
        }



    }
}
