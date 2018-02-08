using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint_ver2
{

    class draw
    {
        public Point[] points_right_arrow;
        public Point[] points_left_arrow;
        public Point[] points_top_arrow;
        public Point[] points_bottom_arrow;
        public Point[] points_pentagon;
        public Point[] points_hexan;
        public Point[] points_fourpoint_star;
        public Point[] points_fivepoint_star;

        public void right_arrow_drawing(Point point1, Point point2)
        {
            points_right_arrow = new Point[8];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {

                points_right_arrow[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_right_arrow[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_right_arrow[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(point1.Y));
                points_right_arrow[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_right_arrow[4] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(point2.Y));
                points_right_arrow[5] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_right_arrow[6] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_right_arrow[7] = points_right_arrow[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_right_arrow[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_right_arrow[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_right_arrow[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32(point2.Y));
                points_right_arrow[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_right_arrow[4] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32(point1.Y));
                points_right_arrow[5] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_right_arrow[6] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_right_arrow[7] = points_right_arrow[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {

                points_right_arrow[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_right_arrow[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_right_arrow[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(point2.Y));
                points_right_arrow[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_right_arrow[4] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(point1.Y));
                points_right_arrow[5] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_right_arrow[6] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_right_arrow[7] = points_right_arrow[0];

            }
            if (point2.X < point1.X && point2.Y > point1.Y)
            {

                points_right_arrow[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_right_arrow[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_right_arrow[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32(point1.Y));
                points_right_arrow[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_right_arrow[4] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32(point2.Y));
                points_right_arrow[5] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X)), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_right_arrow[6] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_right_arrow[7] = points_right_arrow[0];

            }
        }

        public void pentagon_drawing(Point point1, Point point2)
        {
            points_pentagon = new Point[6];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {
                points_pentagon[0] = new Point(Convert.ToInt32((point2.X - point1.X) / 2) + point1.X, Convert.ToInt32(point1.Y));
                points_pentagon[1] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_pentagon[2] = new Point(Convert.ToInt32(point2.X - 15), Convert.ToInt32(point2.Y));
                points_pentagon[3] = new Point(Convert.ToInt32(point1.X + 15), Convert.ToInt32(point2.Y));
                points_pentagon[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_pentagon[5] = points_pentagon[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_pentagon[0] = new Point(Convert.ToInt32(point1.X - 15), Convert.ToInt32(point1.Y));
                points_pentagon[1] = new Point(Convert.ToInt32(point2.X + 15), Convert.ToInt32(point1.Y));
                points_pentagon[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_pentagon[3] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point2.Y));
                points_pentagon[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_pentagon[5] = points_pentagon[0];

            }
            else if (point2.X < point1.X && point2.Y > point1.Y)
            {
                points_pentagon[0] = new Point(Convert.ToInt32(point1.X - 15), Convert.ToInt32(point2.Y));
                points_pentagon[1] = new Point(Convert.ToInt32(point2.X + 15), Convert.ToInt32(point2.Y));
                points_pentagon[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_pentagon[3] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point1.Y));
                points_pentagon[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_pentagon[5] = points_pentagon[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {
                points_pentagon[0] = new Point(Convert.ToInt32(point2.X - 15), Convert.ToInt32(point1.Y));
                points_pentagon[1] = new Point(Convert.ToInt32(point1.X + 15), Convert.ToInt32(point1.Y));
                points_pentagon[2] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_pentagon[3] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                points_pentagon[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_pentagon[5] = points_pentagon[0];

            }
        }

        public void hexan_drawing(Point point1, Point point2)
        {
            points_hexan = new Point[7];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {

                points_hexan[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_hexan[1] = new Point(Convert.ToInt32((point2.X - point1.X) / 2 + point1.X), Convert.ToInt32(point1.Y));
                points_hexan[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_hexan[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_hexan[4] = new Point(Convert.ToInt32((point2.X - point1.X) / 2 + point1.X), Convert.ToInt32(point2.Y));
                points_hexan[5] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_hexan[6] = points_hexan[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_hexan[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_hexan[1] = new Point(Convert.ToInt32((point1.X - point2.X) / 2 + point2.X), Convert.ToInt32(point2.Y));
                points_hexan[2] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_hexan[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_hexan[4] = new Point(Convert.ToInt32((point1.X - point2.X) / 2 + point2.X), Convert.ToInt32(point1.Y));
                points_hexan[5] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_hexan[6] = points_hexan[0];

            }
            else if (point2.X < point1.X && point2.Y > point1.Y)
            {
                points_hexan[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_hexan[1] = new Point(Convert.ToInt32((point1.X - point2.X) / 2 + point2.X), Convert.ToInt32(point1.Y));
                points_hexan[2] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_hexan[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_hexan[4] = new Point(Convert.ToInt32((point1.X - point2.X) / 2 + point2.X), Convert.ToInt32(point2.Y));
                points_hexan[5] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_hexan[6] = points_hexan[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {
                points_hexan[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_hexan[1] = new Point(Convert.ToInt32((point2.X - point1.X) / 2 + point1.X), Convert.ToInt32(point2.Y));
                points_hexan[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_hexan[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_hexan[4] = new Point(Convert.ToInt32((point2.X - point1.X) / 2 + point1.X), Convert.ToInt32(point1.Y));
                points_hexan[5] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_hexan[6] = points_hexan[0];

            }
        }

        public void left_arrow_drawing(Point point1, Point point2)
        {
            points_left_arrow = new Point[8];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {

                points_left_arrow[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_left_arrow[1] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point1.Y));
                points_left_arrow[2] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_left_arrow[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_left_arrow[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_left_arrow[5] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_left_arrow[6] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                points_left_arrow[7] = points_left_arrow[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_left_arrow[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_left_arrow[1] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point2.Y));
                points_left_arrow[2] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_left_arrow[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_left_arrow[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_left_arrow[5] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_left_arrow[6] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point1.Y));
                points_left_arrow[7] = points_left_arrow[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {

                points_left_arrow[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_left_arrow[1] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                points_left_arrow[2] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_left_arrow[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 4) + point2.Y));
                points_left_arrow[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_left_arrow[5] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 4) * 3) + point2.Y));
                points_left_arrow[6] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point1.Y));
                points_left_arrow[7] = points_left_arrow[0];

            }
            if (point2.X < point1.X && point2.Y > point1.Y)
            {

                points_left_arrow[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_left_arrow[1] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point1.Y));
                points_left_arrow[2] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_left_arrow[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 4) + point1.Y));
                points_left_arrow[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_left_arrow[5] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 4) * 3) + point1.Y));
                points_left_arrow[6] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point2.Y));
                points_left_arrow[7] = points_left_arrow[0];

            }
        }

        public void top_arrow_drawing(Point point1, Point point2)
        {
            points_top_arrow = new Point[8];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {

                points_top_arrow[0] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point1.Y));
                points_top_arrow[1] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[3] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(point2.Y));
                points_top_arrow[4] = new Point(Convert.ToInt32(((point2.X - point1.X) / 4) + point1.X), Convert.ToInt32(point2.Y));
                points_top_arrow[5] = new Point(Convert.ToInt32(((point2.X - point1.X) / 4) + point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[6] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[7] = points_top_arrow[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_top_arrow[0] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point2.Y));
                points_top_arrow[1] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[3] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(point1.Y));
                points_top_arrow[4] = new Point(Convert.ToInt32(((point1.X - point2.X) / 4) + point2.X), Convert.ToInt32(point1.Y));
                points_top_arrow[5] = new Point(Convert.ToInt32(((point1.X - point2.X) / 4) + point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[6] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[7] = points_top_arrow[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {

                points_top_arrow[0] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                points_top_arrow[1] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[3] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(point1.Y));
                points_top_arrow[4] = new Point(Convert.ToInt32(((point2.X - point1.X) / 4) + point1.X), Convert.ToInt32(point1.Y));
                points_top_arrow[5] = new Point(Convert.ToInt32(((point2.X - point1.X) / 4) + point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[6] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_top_arrow[7] = points_top_arrow[0];

            }
            if (point2.X < point1.X && point2.Y > point1.Y)
            {

                points_top_arrow[0] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point1.Y));
                points_top_arrow[1] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[3] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(point2.Y));
                points_top_arrow[4] = new Point(Convert.ToInt32(((point1.X - point2.X) / 4) + point2.X), Convert.ToInt32(point2.Y));
                points_top_arrow[5] = new Point(Convert.ToInt32(((point1.X - point2.X) / 4) + point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[6] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_top_arrow[7] = points_top_arrow[0];

            }
        }

        public void bottom_arrow_drawing(Point point1, Point point2)
        {
            points_bottom_arrow = new Point[8];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {

                points_bottom_arrow[0] = new Point(Convert.ToInt32(((point2.X - point1.X) / 4) + point1.X), Convert.ToInt32(point1.Y));
                points_bottom_arrow[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(point1.Y));
                points_bottom_arrow[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[4] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                points_bottom_arrow[5] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[6] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4)) + point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[7] = points_bottom_arrow[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_bottom_arrow[0] = new Point(Convert.ToInt32(((point1.X - point2.X) / 4) + point2.X), Convert.ToInt32(point2.Y));
                points_bottom_arrow[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(point2.Y));
                points_bottom_arrow[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[4] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point1.Y));
                points_bottom_arrow[5] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[6] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4)) + point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[7] = points_bottom_arrow[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {

                points_bottom_arrow[0] = new Point(Convert.ToInt32(((point2.X - point1.X) / 4) + point1.X), Convert.ToInt32(point2.Y));
                points_bottom_arrow[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(point2.Y));
                points_bottom_arrow[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4) * 3) + point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[3] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[4] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point1.Y));
                points_bottom_arrow[5] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[6] = new Point(Convert.ToInt32((((point2.X - point1.X) / 4)) + point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_bottom_arrow[7] = points_bottom_arrow[0];
            }
            if (point2.X < point1.X && point2.Y > point1.Y)
            {

                points_bottom_arrow[0] = new Point(Convert.ToInt32(((point1.X - point2.X) / 4) + point2.X), Convert.ToInt32(point1.Y));
                points_bottom_arrow[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(point1.Y));
                points_bottom_arrow[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4) * 3) + point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[4] = new Point(Convert.ToInt32(((point1.X - point2.X) / 2) + point2.X), Convert.ToInt32(point2.Y));
                points_bottom_arrow[5] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[6] = new Point(Convert.ToInt32((((point1.X - point2.X) / 4)) + point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_bottom_arrow[7] = points_bottom_arrow[0];

            }
        }

        public void foupoint_star_drawing(Point point1, Point point2)
        {
            points_fourpoint_star = new Point[9];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {
                points_fourpoint_star[0] = new Point(Convert.ToInt32(point1.X - 3), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_fourpoint_star[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) - 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) - 10);
                points_fourpoint_star[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32(point1.Y));
                points_fourpoint_star[3] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) + 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) - 10);
                points_fourpoint_star[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_fourpoint_star[5] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) + 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) + 10);
                points_fourpoint_star[6] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32(point2.Y));
                points_fourpoint_star[7] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) - 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) + 10);
                points_fourpoint_star[8] = points_fourpoint_star[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_fourpoint_star[0] = new Point(Convert.ToInt32(point2.X - 3), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_fourpoint_star[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) - 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) - 10);
                points_fourpoint_star[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32(point2.Y));
                points_fourpoint_star[3] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) + 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) - 10);
                points_fourpoint_star[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_fourpoint_star[5] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) + 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) + 10);
                points_fourpoint_star[6] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32(point1.Y));
                points_fourpoint_star[7] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) - 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) + 10);
                points_fourpoint_star[8] = points_fourpoint_star[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {

                points_fourpoint_star[0] = new Point(Convert.ToInt32(point1.X - 3), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_fourpoint_star[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) - 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) - 10);
                points_fourpoint_star[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32(point2.Y));
                points_fourpoint_star[3] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) + 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) - 10);
                points_fourpoint_star[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y));
                points_fourpoint_star[5] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) + 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) + 10);
                points_fourpoint_star[6] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32(point1.Y));
                points_fourpoint_star[7] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X) - 10), Convert.ToInt32(((point1.Y - point2.Y) / 2) + point2.Y) + 10);
                points_fourpoint_star[8] = points_fourpoint_star[0];
            }
            if (point2.X < point1.X && point2.Y > point1.Y)
            {

                points_fourpoint_star[0] = new Point(Convert.ToInt32(point2.X - 3), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_fourpoint_star[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) - 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) - 10);
                points_fourpoint_star[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32(point1.Y));
                points_fourpoint_star[3] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) + 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) - 10);
                points_fourpoint_star[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                points_fourpoint_star[5] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) + 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) + 10);
                points_fourpoint_star[6] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32(point2.Y));
                points_fourpoint_star[7] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + point2.X) - 10), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y) + 10);
                points_fourpoint_star[8] = points_fourpoint_star[0];
            }
        }

        public void fivepoint_star_drawing(Point point1, Point point2)
        {
            points_fivepoint_star = new Point[11];

            if (point2.X > point1.X && point2.Y > point1.Y)
            {
                points_fivepoint_star[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) - 10) + point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32(point1.Y));
                points_fivepoint_star[3] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + 10) + point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[5] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + 25) + point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) + 10) + point1.Y));
                points_fivepoint_star[6] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + 40) + point1.X), Convert.ToInt32(point2.Y));
                points_fivepoint_star[7] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) + 25) + point1.Y));
                points_fivepoint_star[8] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) - 40) + point1.X), Convert.ToInt32(point2.Y));
                points_fivepoint_star[9] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) - 25) + point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) + 10) + point1.Y));
                points_fivepoint_star[10] = points_fivepoint_star[0];

            }
            else if (point2.X < point1.X && point2.Y < point1.Y)
            {
                points_fivepoint_star[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) - 10) + point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32(point2.Y));
                points_fivepoint_star[3] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + 10) + point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[5] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + 25) + point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) + 10) + point2.Y));
                points_fivepoint_star[6] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + 40) + point2.X), Convert.ToInt32(point1.Y));
                points_fivepoint_star[7] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) + 25) + point2.Y));
                points_fivepoint_star[8] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) - 40) + point2.X), Convert.ToInt32(point1.Y));
                points_fivepoint_star[9] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) - 25) + point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) + 10) + point2.Y));
                points_fivepoint_star[10] = points_fivepoint_star[0];

            }
            else if (point2.X > point1.X && point2.Y < point1.Y)
            {

                points_fivepoint_star[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) - 10) + point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[2] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32(point2.Y));
                points_fivepoint_star[3] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + 10) + point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[4] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) - 20) + point2.Y));
                points_fivepoint_star[5] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + 25) + point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) + 10) + point2.Y));
                points_fivepoint_star[6] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + 40) + point1.X), Convert.ToInt32(point1.Y));
                points_fivepoint_star[7] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2)) + point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) + 25) + point2.Y));
                points_fivepoint_star[8] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) - 40) + point1.X), Convert.ToInt32(point1.Y));
                points_fivepoint_star[9] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) - 25) + point1.X), Convert.ToInt32((((point1.Y - point2.Y) / 2) + 10) + point2.Y));
                points_fivepoint_star[10] = points_fivepoint_star[0];
            }
            if (point2.X < point1.X && point2.Y > point1.Y)
            {

                points_fivepoint_star[0] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[1] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) - 10) + point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[2] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32(point1.Y));
                points_fivepoint_star[3] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + 10) + point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) - 20) + point1.Y));
                points_fivepoint_star[5] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + 25) + point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) + 10) + point1.Y));
                points_fivepoint_star[6] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) + 40) + point2.X), Convert.ToInt32(point2.Y));
                points_fivepoint_star[7] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2)) + point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) + 25) + point1.Y));
                points_fivepoint_star[8] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) - 40) + point2.X), Convert.ToInt32(point2.Y));
                points_fivepoint_star[9] = new Point(Convert.ToInt32((((point1.X - point2.X) / 2) - 25) + point2.X), Convert.ToInt32((((point2.Y - point1.Y) / 2) + 10) + point1.Y));
                points_fivepoint_star[10] = points_fivepoint_star[0];
            }
        }



    }

}
