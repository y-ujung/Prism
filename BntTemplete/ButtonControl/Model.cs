using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ButtonControl
{
    public class Model
    {
        public enum SHAPE_TYPE { CIRCLE, RECT }

        public Rect Bound { get; set; }
        public Brush Color { get; set; } 
        
        public SHAPE_TYPE Type { get; set; } 

        public Model(Rect bound , Brush b, SHAPE_TYPE t)
        {
            Bound = bound;
            Color = b;
            Type = t;
        }
    }
}
