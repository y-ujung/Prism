using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismEngine
{
    public class ImageInfo
    {
        private const int PROPERTY_TAG_FRAME_DELAY = 0x5100;
        public Image image;
        public int frame;
        public int frameCount;
        public bool frameDirty;
        public bool animated;
        public int[] frameDelay;    // ms 단위



        public ImageInfo(Image image)
        {
            this.image = image;
            animated = ImageAnimator.CanAnimate(image);

            if (animated)
            {
                frameCount = image.GetFrameCount(FrameDimension.Time);

                PropertyItem frameDelayItem = image.GetPropertyItem(PROPERTY_TAG_FRAME_DELAY);

                // If the image does not have a frame delay, we just return 0.                                     
                //
                if (frameDelayItem != null)
                {
                    // Convert the frame delay from byte[] to int
                    //
                    byte[] values = frameDelayItem.Value;

                    frameDelay = new int[frameCount];
                    for (int i = 0; i < frameCount; ++i)
                    {
                        frameDelay[i] = values[i * 4] + 256 * values[i * 4 + 1] + 256 * 256 * values[i * 4 + 2] + 256 * 256 * 256 * values[i * 4 + 3];

                    }
                }
            }
            else
            {
                frameCount = 1;
            }
            if (frameDelay == null)
            {
                frameDelay = new int[frameCount];
            }
            this.image.Dispose();
            image.Dispose();
        }



    }
}

