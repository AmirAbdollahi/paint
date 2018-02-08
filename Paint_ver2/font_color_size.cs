using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint_ver2
{
    class font_color_size
    {
        public int fontsize = 4;
        public Color p = Color.Black;
        public Brush pp = Brushes.Black;
        public bool resultline = false;
        public bool resultpencil = false;




        public int font_size(int x)
        {
            fontsize = x;
            return fontsize;
        }

       
      

        
    }
}
