using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class TinySquare
    {
        public const float Size = 6;
        public PointF Location;
        public PointF BottomRight;

        public TinySquare(float x, float y)
        {
            Location = new PointF(x, y);
            BottomRight = new PointF(Location.X + Size, Location.Y + Size);
        }

        public bool IsInside(PointF mousePoint, float margin)
        {
            if (mousePoint.X > this.Location.X - margin && mousePoint.X < this.BottomRight.X + margin &&
                mousePoint.Y > this.Location.Y - margin && mousePoint.Y < this.BottomRight.Y + margin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
