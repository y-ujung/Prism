using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismInfra
{
    public interface IEngine
    {

        void LoadImage();
        void SaveImage();
        void ResizeImage();
        void CropImage();
    }
}
