using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_ver2
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmpMain;
        Bitmap bmpBeforeDrawShape;
        Bitmap bmpbeforeselect;
        Bitmap bmpclear;
        Bitmap bmpimage;
        Bitmap bmpselect;
        Bitmap bmpselect1;


        font_color_size clas = new font_color_size();
        draw draw = new draw();

        bool pencil = false;
        bool pencildraw = false;
        bool line = false;
        bool linedraw = false;
        bool eraser = false;
        bool eraserdraw = false;
        bool fill=false;
        bool circle = false;
        bool circledraw = false;
        bool rectangle=false;
        bool rectangledraw = false;
        bool poligan = false;
        bool poligandraw = false;
        bool triangle = false;
        bool triangledraw = false;
        bool triangleright = false;
        bool trianglerightdraw = false;
        bool diamond = false;
        bool diamonddraw = false;
        bool pentagon=false;
        bool pentagondraw = false;
        bool hexan = false;
        bool hexandraw = false;
        bool right_arrow = false;
        bool right_arrow_draw = false;
        bool left_arrow = false;
        bool left_arrow_draw = false;
        bool top_arrow = false;
        bool top_arrow_draw = false;
        bool bottom_arrow = false;
        bool bottom_arrow_draw = false;
        bool four_point_star = false;
        bool four_point_star_draw = false;
        bool five_point_star = false;
        bool five_point_star_draw = false;
        bool lightening = false;
        bool lightening_draw = false;
        bool rec_call = false;
        bool rec_call_draw = false;
        bool oval_call = false;
        bool oval_call_draw = false;
        bool cloud_call = false;
        bool cloud_call_draw = false;
        bool heart = false;
        bool heart_draw = false;
        bool text = false;
        bool text_draw = false;

        bool fill_shapes = false;
        bool color_picker = false;
        bool select = false;
        bool select_draw = false;
        bool cut = false;
        bool copy = false;
        bool paste = false;
        


        bool right_resize_form=false;
        bool right_bottom_resize_form=false;
        bool bottom_resize_form=false;
        bool right_top_resize_shape = false;
        bool right_bottom_resize_shape = false;
        bool left_top_resize_shape = false;
        bool left_bottom_resize_shape = false;
        bool right_resize_shape = false;
        bool bottom_resize_shape = false;
        bool left_resize_shape = false;
        bool top_resize_shape = false;

        Point[] trangpoints;

        Point point1;
        Point point2;
       
        int width;
        int height;

        int poligancont = 0;

        Pen pen;
        SolidBrush brush;

        int width_pic;
        int height_pic;

        Color crl;

        Point[] myPointList = new Point[1000000];
        int counter = 0;
        Rectangle theRectangle;
        Rectangle select_rectangle;

        int counter_colorpanel = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void select_free_btn_Click(object sender, EventArgs e)
        {

        }

        private void right_arrow_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            right_arrow = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fill_outline_combox.SelectedItem==fill_outline_combox.Items[1])
            {
                fill_shapes = true;
            }
            else
            {
                fill_shapes = false;
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)

        {
            e.Graphics.DrawImage(bmpMain, 0, 0);
         
           
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            bmpMain = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmpMain);
            bmpBeforeDrawShape = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmpclear= new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmpselect = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmpbeforeselect = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmpimage = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            pictureBox1.Refresh();

            //for (int y = 0; y < pictureBox1.Height; y++)
            //{
            //    for (int x = 0; x < pictureBox1.Width; x++)
            //    {
            //        bmpMain.SetPixel(x, y, Color.White);
            //    }
            //}

            //pictureBox1.Invalidate();

           
        }

        private void pencil_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            pencil = true;

                       
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (line)
            {
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                linedraw = true;
                point1 = new Point(e.X, e.Y);
            }
            else if (pencil)
            {
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                pencildraw = true;
                
            }
            else if(eraser)
            {
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                eraserdraw = true;
            }
            else if (fill)
            {
                 bmpBeforeDrawShape = new Bitmap(bmpMain);
                 point1 = e.Location;
                 crl = bmpBeforeDrawShape.GetPixel(point1.X, point1.Y);

                 for (int j = point1.Y; bmpMain.GetPixel(point1.X, j) == crl && j < pictureBox1.Height - 1; j++)
                 {
                     for (int i = point1.X; bmpMain.GetPixel(i, j) == crl && i < pictureBox1.Width - 1; i++)
                     {

                         bmpBeforeDrawShape.SetPixel(i, j, color1_panel.BackColor);

                     }
                 }

                 for (int j = point1.Y; bmpMain.GetPixel(point1.X, j) == crl && j >= 1; j--)
                 {
                     for (int i = point1.X; bmpMain.GetPixel(i, j) == crl && i >= 1; i--)
                     {

                         bmpBeforeDrawShape.SetPixel(i, j, color1_panel.BackColor);

                     }
                 }
                
                 for (int j = point1.Y; bmpMain.GetPixel(point1.X, j) == crl && j >= 1; j--)
                 {
                     for (int i = point1.X; bmpMain.GetPixel(i, j) == crl && i < pictureBox1.Width - 1; i++)
                     {

                         bmpBeforeDrawShape.SetPixel(i, j, color1_panel.BackColor);

                     }
                 }
                 for (int j = point1.Y; bmpMain.GetPixel(point1.X, j) == crl && j < pictureBox1.Height - 1; j++)
                 {
                     for (int i = point1.X; bmpMain.GetPixel(i, j) == crl && i >= 1; i--)
                     {

                         bmpBeforeDrawShape.SetPixel(i, j, color1_panel.BackColor);

                     }
                 }
                
                 bmpMain = new Bitmap(bmpBeforeDrawShape);
                 pictureBox1.Refresh();
            }

            else if (circle)
            {
                circledraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
             
            }
            else if(rectangle)
            {
                rectangledraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (poligan)
            {
                
                if (poligancont==0)
                {
                    poligandraw = true;
                    bmpBeforeDrawShape = new Bitmap(bmpMain);
                    point1 = new Point(e.X, e.Y);
                }
                else if (poligancont>=1)
                {
                    bmpBeforeDrawShape = new Bitmap(bmpMain);
                    point1 = point2;
                    point2 = e.Location;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);

                    g.DrawLine(pen, point1, point2);
                    poligancont++;
                    pictureBox1.Refresh();
                }
            }
            else if (triangle)
            {
                triangledraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (triangleright)
            {
                trianglerightdraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (diamond)
            {
                diamonddraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (pentagon)
            {
                pentagondraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (hexan)
            {
                hexandraw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (right_arrow)
            {
                right_arrow_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (left_arrow)
            {
                left_arrow_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (top_arrow)
            {
                top_arrow_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (bottom_arrow)
            {
                bottom_arrow_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (four_point_star)
            {
                four_point_star_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (five_point_star)
            {
                five_point_star_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (lightening)
            {
                lightening_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);

            }
            else if (rec_call)
            {
                rec_call_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
            }
            else if (oval_call)
            {
                oval_call_draw= true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
            }
            else if (cloud_call)
            {
                cloud_call_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
            }
            else if (heart)
            {
                heart_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
            }
            else if (text)
            {
                text_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
            }
            else if (color_picker)
            {
                point1 = new Point(e.X, e.Y);
            }
            else if (select)
            {
                select_draw = true;
                bmpBeforeDrawShape = new Bitmap(bmpMain);
                point1 = new Point(e.X, e.Y);
            }
            //pictureBox5.Visible = false;
            //pictureBox6.Visible = false;
            //pictureBox7.Visible = false;
            //pictureBox8.Visible = false;
            //pictureBox9.Visible = false;
            //pictureBox10.Visible = false;
            //pictureBox11.Visible = false;
            //pictureBox12.Visible = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            

            linedraw = false;
            pencildraw = false;
            eraserdraw = false;

            if (circle)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                circledraw = false;
                
            }
            else if (rectangle)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                rectangledraw = false;
            }
           
            else if (triangle)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                triangledraw = false;
            }
            else if (triangleright)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                trianglerightdraw = false;
            }
            else if (diamond)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                diamonddraw = false;
            }
            else if (pentagon)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                pentagondraw = false;
            }
            else if (hexan)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                hexandraw = false;
            }
            else if (right_arrow)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                right_arrow_draw = false;
            }
            else if (left_arrow)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                left_arrow_draw = false;
            }
            else if (top_arrow)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                top_arrow_draw = false;
            }
            else if (bottom_arrow)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                bottom_arrow_draw = false;
            }
            else if (four_point_star)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                four_point_star_draw = false;
            }
            else if (five_point_star)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                five_point_star_draw = false;
            }
            else if (lightening)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                lightening_draw = false;
            }
            else if (rec_call)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                rec_call_draw = false;
            }
            else if (oval_call)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                oval_call_draw = false;
            }
            else if (cloud_call)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                cloud_call_draw = false;
            }
            else if (heart)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                heart_draw = false;
            }
            else if (text)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;
                pictureBox12.Visible = true;
                text_draw = false;
            }

            poligandraw = false;
            poligancont++;
            counter = 0;

           

           if (color_picker)
            {
               Color colorpicker= bmpMain.GetPixel(point1.X,point1.Y);
               color1_panel.BackColor = colorpicker;
               if (colorpicker==Color.FromArgb(0,0,0,0))
               {
                   color1_panel.BackColor = Color.White;
               }
            }

            if (select)
            {
                select_draw = false;
                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, point2.X - point1.X, point2.Y - point1.Y);
                ControlPaint.DrawReversibleFrame(theRectangle, Color.Black, FrameStyle.Dashed);
                select_rectangle = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);
                System.Drawing.Imaging.PixelFormat format = bmpMain.PixelFormat;
                bmpselect = bmpMain.Clone(select_rectangle,format);
               
                pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                crop_btn.Enabled = true;
                pictureBox1.Refresh();
              // bmpselect.Save("d://kk.jpg");
               
            }

          }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (line && linedraw)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                point2 = e.Location;
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);

                g.DrawLine(pen, point1, point2);

                pictureBox1.Refresh();
                
               
            }

            else if(pencildraw && pencil)
            {
                brush = new SolidBrush(color1_panel.BackColor);
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                point1 = e.Location;

                g = Graphics.FromImage(bmpMain);
                g.FillEllipse(brush, point1.X, point1.Y,clas.fontsize,clas.fontsize);
                myPointList[counter] = e.Location;

                if (counter >= 1)
                {
                   g.DrawLine(pen, myPointList[counter - 1].X + (clas.fontsize / 2), myPointList[counter - 1].Y + (clas.fontsize / 2), myPointList[counter].X + (clas.fontsize / 2), myPointList[counter].Y + (clas.fontsize / 2));
                }


                counter++;
                pictureBox1.Refresh();
            }

            else if (eraserdraw && eraser)

            {
                point1 = e.Location;
                g = Graphics.FromImage(bmpMain);
                Pen p = new Pen(Brushes.White, clas.fontsize);
                
                g.FillEllipse(Brushes.White, point1.X, point1.Y, clas.fontsize, clas.fontsize);

                myPointList[counter] = e.Location;

                if (counter >= 1)
                {
                    g.DrawLine(p, myPointList[counter - 1].X + (clas.fontsize / 2), myPointList[counter - 1].Y + (clas.fontsize / 2), myPointList[counter].X + (clas.fontsize / 2), myPointList[counter].Y + (clas.fontsize / 2));
                }

                counter++;
                pictureBox1.Refresh();
            }

            else if (circledraw && circle)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.FillEllipse(brush, point1.X, point1.Y, width, height);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
                   
                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X,theRectangle.Location.Y-26);
                    pictureBox6.Location = new Point(theRectangle .Location.X-4,theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height+theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X-2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width+theRectangle.Location.X,(theRectangle.Height/2)+theRectangle.Location.Y-26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X,theRectangle.Height+theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);

                   
                }

                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.DrawEllipse(pen, point1.X, point1.Y, width, height);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X,theRectangle.Location.Y-26);
                    pictureBox6.Location = new Point(theRectangle .Location.X-4,theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height+theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X-2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width+theRectangle.Location.X,(theRectangle.Height/2)+theRectangle.Location.Y-26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X,theRectangle.Height+theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
          
            }

            else if (rectangledraw && rectangle)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.FillRectangle(brush, point1.X, point1.Y, width, height);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.DrawRectangle(pen, point1.X, point1.Y, width, height);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
               

            }
            else if (poligan && poligandraw)
            {
                if (poligancont==0)
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    g.DrawLine(pen, point1, point2);
                    pictureBox1.Refresh();

                }
                else
                {

                }
                
            }

            else if (triangledraw && triangle)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    trangpoints = new Point[4];
                    trangpoints[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point2.Y));
                    trangpoints[1] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point1.Y));
                    trangpoints[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(point2.Y));
                    trangpoints[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point2.Y));
                    g.FillPolygon(brush,trangpoints);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);

                    trangpoints = new Point[4];
                    trangpoints[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point2.Y));
                    trangpoints[1] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point1.Y));
                    trangpoints[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(point2.Y));
                    trangpoints[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point2.Y));
                    g.DrawLines(pen, trangpoints);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                

            }
            else if (trianglerightdraw && triangleright)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    Point[] trangpoints = new Point[4];
                    trangpoints[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point1.Y));
                    trangpoints[1] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(point2.Y));
                    trangpoints[2] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point2.Y));
                    trangpoints[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point1.Y));
                    g.FillPolygon(brush, trangpoints);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);

                    Point[] trangpoints = new Point[4];
                    trangpoints[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point1.Y));
                    trangpoints[1] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(point2.Y));
                    trangpoints[2] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point2.Y));
                    trangpoints[3] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(point1.Y));
                    g.DrawLines(pen, trangpoints);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
               

            }
            else if (diamonddraw && diamond)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    Point[] points = new Point[5];
                    points[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                    points[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(point1.Y));
                    points[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                    points[3] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                    points[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                    g.FillPolygon(brush, points);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);

                    Point[] points = new Point[5];
                    points[0] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                    points[1] = new Point(Convert.ToInt32((((point2.X - point1.X) / 2) + point1.X)), Convert.ToInt32(point1.Y));
                    points[2] = new Point(Convert.ToInt32(point2.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                    points[3] = new Point(Convert.ToInt32(((point2.X - point1.X) / 2) + point1.X), Convert.ToInt32(point2.Y));
                    points[4] = new Point(Convert.ToInt32(point1.X), Convert.ToInt32(((point2.Y - point1.Y) / 2) + point1.Y));
                    g.DrawLines(pen, points);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                

            }
            else if (pentagondraw && pentagon)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.pentagon_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_pentagon);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.pentagon_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_pentagon);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                

            }
            else if (hexandraw && hexan)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.hexan_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_hexan);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.hexan_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_hexan);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                

            }
            else if (right_arrow_draw && right_arrow)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.right_arrow_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_right_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;
                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.right_arrow_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_right_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);

                }
                
            }
            else if (left_arrow_draw && left_arrow)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.left_arrow_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_left_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.left_arrow_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_left_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
            }

            else if (top_arrow_draw && top_arrow)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.top_arrow_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_top_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.top_arrow_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_top_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);

                }
                
            }
            else if (bottom_arrow_draw && bottom_arrow)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.bottom_arrow_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_bottom_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.bottom_arrow_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_bottom_arrow);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 12, point1.Y + 150, width, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                
            }
            else if (four_point_star_draw && four_point_star)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.foupoint_star_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_fourpoint_star);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.foupoint_star_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_fourpoint_star);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                
            }
            else if (five_point_star_draw && five_point_star)
            {
                if (fill_shapes)
                {
                    brush = new SolidBrush(color1_panel.BackColor);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.fivepoint_star_drawing(point1, point2);
                    g.FillPolygon(brush, draw.points_fivepoint_star);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                else
                {
                    pen = new Pen(color1_panel.BackColor, clas.fontsize);
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    draw.fivepoint_star_drawing(point1, point2);
                    g.DrawLines(pen, draw.points_fivepoint_star);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                
            }
            else if (lightening_draw && lightening)
            {
                if (fill_shapes)
                {
                    //point2 = e.Location;
                    //g.Clear(pictureBox1.BackColor);
                    //bmpMain = new Bitmap(bmpBeforeDrawShape);
                    //g = Graphics.FromImage(bmpMain);
                    //pictureBox1.Refresh();
                }
                else
                {
                    point2 = e.Location;
                    width = point2.X - point1.X;
                    height = point2.Y - point1.Y;

                    g.Clear(pictureBox1.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDrawShape);
                    g = Graphics.FromImage(bmpMain);
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X, point1.Y, width, height);
                    pictureBox1.Refresh();

                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                    pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                    pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                    pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                    pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                    pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                    pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
                }
                
            }
            else if (rec_call_draw && rec_call)
            {
                point2 = e.Location;
                width = point2.X - point1.X;
                height = point2.Y - point1.Y;

                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X, point1.Y, width,height);
                pictureBox1.Refresh();

                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            }
            else if (oval_call_draw && oval_call)
            {
                point2 = e.Location;
                width = point2.X - point1.X;
                height = point2.Y - point1.Y;

                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X, point1.Y, width, height);
                pictureBox1.Refresh();

                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            }
            else if (cloud_call_draw && cloud_call)
            {
                point2 = e.Location;
                width = point2.X - point1.X;
                height = point2.Y - point1.Y;

                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X, point1.Y,width,height);
                pictureBox1.Refresh();

                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            }
            else if (heart_draw && heart)
            {
                point2 = e.Location;
                width = point2.X - point1.X;
                height = point2.Y - point1.Y;

                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X, point1.Y,width,height);
                pictureBox1.Refresh();

                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            }
            else if (text_draw && text)
            {
                point2 = e.Location;
                width = point2.X - point1.X;
                height = point2.Y - point1.Y;

                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                richTextBox1.Location = new Point(point1.X+pictureBox1.Location.X,point1.Y+pictureBox1.Location.Y);
                richTextBox1.Size = new Size(point2.X - point1.X, point2.Y - point1.Y);
                richTextBox1.Visible = true;
                pictureBox1.Refresh();

                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

                pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
                pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
                pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
                pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
                pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
                pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            }
            else if (select_draw && select)
            {
               
                point2 = e.Location;
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                select_rectangle = new Rectangle(point1.X + 10, point1.Y + 150, point2.X - point1.X, point2.Y - point1.Y);
                ControlPaint.DrawReversibleFrame(select_rectangle, Color.Black, FrameStyle.Dashed);
                pictureBox1.Refresh();
            }

           
    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void line_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            line = true;
                       
            line_btn.BackColor = Color.LightYellow;
            
        }

        private void eraser_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            eraser = true;
       
        }

        private void fill_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            fill = true;
           
        }

        private void oval_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            circle = true;
        }

        private void deactrive_buttons ()
        {
            pencil = false;
            line = false;
            eraser = false;
            fill = false;
            circle = false;
            rectangle = false;
            poligan = false;
            triangle = false;
            triangleright = false;
            diamond = false;
            pentagon = false;
            hexan = false;
            right_arrow = false;
            left_arrow = false;
            top_arrow = false;
            bottom_arrow = false;
            four_point_star = false;
            five_point_star = false;
            lightening = false;
            rec_call = false;
            oval_call = false;
            cloud_call = false;
            heart = false;
            text = false;
            color_picker = false;
            select = false;
            cut = false;
            copy = false;
            paste = false;
            poligancont = 0;
            
        }

        private void rec_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            rectangle = true;
        }

        private void polygan_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            poligan = true;
        }

        private void tirangle_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            triangle = true;
        }

        private void right_triangle_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            triangleright = true;
        }

        private void pnlGray50_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pnlGray50_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Gray;
            richTextBox1.SelectionColor = Color.Gray;
         }

        private void pnlBlack_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Black;
            richTextBox1.SelectionColor = Color.Black;
            
        }

        private void pnlDarkRed_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Maroon;
            richTextBox1.SelectionColor = Color.Maroon;
            
        }

        private void pnlRed_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Red;
            richTextBox1.SelectionColor = Color.Red;

        }

        private void pnlOrange_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Orange;
            richTextBox1.SelectionColor = Color.Orange;

        }

        private void pnlYellow_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Yellow;
            richTextBox1.SelectionColor = Color.Yellow;
        }

        private void pnlGreen_Click(object sender, EventArgs e)
        {

            color1_panel.BackColor = Color.Green;
            richTextBox1.SelectionColor = Color.Green;

        }

        private void pnlTurquoise_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Blue;
            richTextBox1.SelectionColor = Color.Blue;

        }

        private void pnlPurple_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Purple;
            richTextBox1.SelectionColor = Color.Purple;

        }

        private void pnlWhite_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.White;
            richTextBox1.SelectionColor = Color.White;

        }

        private void pnlGray25_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Silver;
            richTextBox1.SelectionColor = Color.Silver;

        }

        private void pnlBrown_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Brown;
            richTextBox1.SelectionColor = Color.Brown;

        }

        private void pnlLightPink_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Pink;
            richTextBox1.SelectionColor = Color.Pink;

        }

        private void pnlGold_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Bisque;
            richTextBox1.SelectionColor = Color.Bisque;

        }

        private void pnlLightYellow_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.LemonChiffon;
            richTextBox1.SelectionColor = Color.LemonChiffon;

        }

        private void pnlLime_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.PaleGreen;
            richTextBox1.SelectionColor = Color.PaleGreen;

        }

        private void pnlPaleTurquoise_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.Cyan;
            richTextBox1.SelectionColor = Color.Cyan;

        }

        private void pnlSteelBlue_Click(object sender, EventArgs e)
        {
            color1_panel.BackColor = Color.DarkOrchid;
            richTextBox1.SelectionColor = Color.DarkOrchid;

        }

        private void diamond_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            diamond = true;
        }

        private void pentagon_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            pentagon = true;
        }

        private void hexagon_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            hexan = true;
        }

        private void edit_color_btn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                color1_panel.BackColor = colorDialog1.Color;
                //pnlCustomColor0.BackColor = colorDialog1.CustomColors[colorDialog1.];
               if (counter_colorpanel==0)
               {
                   CustomColor0_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 1;
               }
               else if (counter_colorpanel == 1)
               {
                   CustomColor1_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 2;
               }
               else if (counter_colorpanel == 2)
               {
                   CustomColor2_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 3;
               }
               else if (counter_colorpanel == 3)
               {
                   CustomColor3_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 4;
               }
               else if (counter_colorpanel ==4)
               {
                   CustomColor4_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 5;
               }
               else if (counter_colorpanel == 5)
               {
                   CustomColor5_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 6;
               }
               else if (counter_colorpanel == 6)
               {
                   CustomColor6_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 7;
               }
               else if (counter_colorpanel == 7)
               {
                   CustomColor7_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 8;
               }
               else if (counter_colorpanel == 8)
               {
                   CustomColor8_pnl.BackColor = colorDialog1.Color;
                   counter_colorpanel = 0;
               }
            }
            

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            right_resize_form = true;
            width_pic = pictureBox1.Width;
            height_pic = pictureBox1.Height;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            right_resize_form = false;
            pictureBox2.Location = new Point((pictureBox1.Size.Width + pictureBox1.Location.X), (pictureBox1.Size.Height/2)+pictureBox1.Location.Y);
            pictureBox3.Location = new Point((pictureBox1.Size.Width + pictureBox1.Location.X), (pictureBox1.Location.Y+pictureBox1.Height));
            pictureBox4.Location = new Point((pictureBox1.Width/2)+pictureBox1.Location.X, (pictureBox1.Location.Y + pictureBox1.Height));
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;
            if (right_resize_form )
            {
                pictureBox1.Width = width_pic + e.X;

            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            right_bottom_resize_form = true;
            width_pic = pictureBox1.Width;
            height_pic = pictureBox1.Height;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            right_bottom_resize_form = false;
            pictureBox2.Location = new Point((pictureBox1.Size.Width + pictureBox1.Location.X), (pictureBox1.Size.Height / 2) + pictureBox1.Location.Y);
            pictureBox3.Location = new Point((pictureBox1.Size.Width + pictureBox1.Location.X), (pictureBox1.Location.Y + pictureBox1.Height));
            pictureBox4.Location = new Point((pictureBox1.Width / 2) + pictureBox1.Location.X, (pictureBox1.Location.Y + pictureBox1.Height));
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNWSE;
            if (right_bottom_resize_form)
            {
                pictureBox1.Height = height_pic + e.Y;
                pictureBox1.Width = width_pic + e.X;

            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            bottom_resize_form = true;
            width_pic = pictureBox1.Width;
            height_pic = pictureBox1.Height;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            bottom_resize_form = false;
            pictureBox2.Location = new Point((pictureBox1.Size.Width + pictureBox1.Location.X), (pictureBox1.Size.Height / 2) + pictureBox1.Location.Y);
            pictureBox3.Location = new Point((pictureBox1.Size.Width + pictureBox1.Location.X), (pictureBox1.Location.Y + pictureBox1.Height));
            pictureBox4.Location = new Point((pictureBox1.Width / 2) + pictureBox1.Location.X, (pictureBox1.Location.Y + pictureBox1.Height));
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNS;
            if (bottom_resize_form)
            {
               pictureBox1.Height = height_pic + e.Y;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //theRectangle = new Rectangle(200,200,100,100);
            //ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            right_top_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;

            
            
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            right_top_resize_shape = false;
           
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            point1.Y = point1.Y + e.Y;

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
            
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNESW;
            if (right_top_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);

                width = width_pic + e.X;
                height = height_pic - e.Y;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X, point1.Y + e.Y, width, height);
                        
                       
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X, point1.Y + e.Y, width, height);
                       
                       
                    }
                    
                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X, point1.Y + e.Y, width, height);
                        
                        
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X, point1.Y + e.Y, width, height);
                        
                       
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X, point1.Y+e.Y, width,height);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X, point1.Y+e.Y, width, height);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X, point1.Y+e.Y, width, height);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X, point1.Y+e.Y, width, height);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X, point1.Y+e.Y, width, height);
                }

                pictureBox1.Refresh();
                theRectangle = new Rectangle(point1.X + 10, point1.Y + e.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
            }
        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            right_bottom_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
        }

        private void pictureBox7_MouseUp(object sender, MouseEventArgs e)
        {
            right_bottom_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNWSE;
            if (right_bottom_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                width = width_pic + e.X;
                height= height_pic + e.Y;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X, point1.Y, width, height);
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X, point1.Y, width, height);
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X, point1.Y, width, height);
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X, point1.Y, width, height);
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X, point1.Y, width, height);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X, point1.Y, width, height);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X, point1.Y, width, height);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X, point1.Y, width, height);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X, point1.Y, width, height);
                }
                pictureBox1.Refresh();
                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height+ 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

            }
        }

        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            right_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
        }

        private void pictureBox9_MouseUp(object sender, MouseEventArgs e)
        {
            right_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
           
        }

        private void pictureBox9_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;
            if (right_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                width = width_pic + e.X;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X, point1.Y, width, height_pic);
                        
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X, point1.Y, width, height_pic);
                        
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X, point1.Y, width, height_pic);
                       
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X, point1.Y, width, height_pic);
                        
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X, point1.Y, width, height_pic);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X, point1.Y, width, height_pic);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X, point1.Y, width, height_pic);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X, point1.Y, width, height_pic);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X, point1.Y, width, height_pic);
                }
                pictureBox1.Refresh();
                theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height_pic + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
            }
        }

        private void pictureBox10_MouseUp(object sender, MouseEventArgs e)
        {
            bottom_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            bottom_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
        }

        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNS;
            if (bottom_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                height = height_pic + e.Y;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X, point1.Y, width_pic, height);
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X, point1.Y, width_pic, height);
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X, point1.Y, width_pic, height);
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X, point1.Y, width_pic, height);
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X, point1.Y, width_pic, height);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X, point1.Y, width_pic, height);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X, point1.Y, width_pic, height);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X, point1.Y, width_pic, height);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X, point1.Y, width_pic, height);
                }
              
                    pictureBox1.Refresh();
                    theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width_pic + 5, height + 5);
                    ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

               
            }
        }

        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            left_resize_shape= true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
          
        }

        private void pictureBox11_MouseUp(object sender, MouseEventArgs e)
        {
            left_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            point1.X = point1.X + e.X;

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox11_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;
            if (left_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                width = width_pic - e.X;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X + e.X, point1.Y, width, height_pic);
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X + e.X, point1.Y, width, height_pic);
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X + e.X, point1.Y, width, height_pic);
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X + e.X, point1.Y, width, height_pic);
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X+e.X, point1.Y, width, height_pic);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X+e.X, point1.Y, width, height_pic);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X+e.X, point1.Y, width, height_pic);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X+e.X, point1.Y, width, height_pic);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X+e.X, point1.Y, width, height_pic);
                }
                pictureBox1.Refresh();
                theRectangle = new Rectangle(point1.X + 10 + e.X, point1.Y + 150, width + 5, height_pic + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

            }
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            left_bottom_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
        }

        private void pictureBox8_MouseUp(object sender, MouseEventArgs e)
        {
            left_bottom_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            point1.X = point1.X + e.X;

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNESW;
            if (left_bottom_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                width = width_pic - e.X;
                height = height_pic + e.Y;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X + e.X, point1.Y, width, height);
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X + e.X, point1.Y, width, height);
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X + e.X, point1.Y, width, height);
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X + e.X, point1.Y, width, height);
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X+e.X, point1.Y, width, height);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X+e.X, point1.Y, width, height);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X+e.X, point1.Y, width, height);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X+e.X, point1.Y, width, height);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X+e.X, point1.Y, width, height);
                }
                pictureBox1.Refresh();
                theRectangle = new Rectangle(point1.X + 10 + e.X, point1.Y + 150, width + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

            }
        }

        private void pictureBox12_MouseDown(object sender, MouseEventArgs e)
        {
            top_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
        }

        private void pictureBox12_MouseUp(object sender, MouseEventArgs e)
        {
            top_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            point1.Y = point1.Y + e.Y;

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox12_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNS;
            if (top_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                height = height_pic - e.Y;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X, point1.Y + e.Y, width_pic, height);
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X, point1.Y + e.Y, width_pic, height);
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X, point1.Y + e.Y, width_pic, height);
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X, point1.Y + e.Y, width_pic, height);
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X, point1.Y + e.Y, width_pic, height);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X, point1.Y + e.Y, width_pic, height);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X, point1.Y + e.Y, width_pic, height);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X, point1.Y + e.Y, width_pic, height);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X, point1.Y + e.Y, width_pic, height);
                }
                pictureBox1.Refresh();
                theRectangle = new Rectangle(point1.X + 10, point1.Y +e.Y+ 150, width_pic + 5, height + 5);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);

            }
        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            right_top_resize_shape = true;
            width_pic = theRectangle.Width;
            height_pic = theRectangle.Height;
        }

        private void pictureBox6_MouseUp(object sender, MouseEventArgs e)
        {
            right_top_resize_shape = false;
            pictureBox5.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Location.Y - 26);
            pictureBox6.Location = new Point(theRectangle.Location.X - 4, theRectangle.Location.Y - 26);
            pictureBox7.Location = new Point(theRectangle.Width + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 26);
            pictureBox8.Location = new Point(theRectangle.Location.X - 2, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox9.Location = new Point(theRectangle.Width + theRectangle.Location.X, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox10.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Height + theRectangle.Location.Y - 24);
            pictureBox11.Location = new Point(theRectangle.Location.X - 4, (theRectangle.Height / 2) + theRectangle.Location.Y - 26);
            pictureBox12.Location = new Point((theRectangle.Width / 2) + theRectangle.Location.X, theRectangle.Location.Y - 26);
            point1.X = point1.X + e.X;
            point1.Y = point1.Y + e.Y;

            theRectangle = new Rectangle(point1.X + 10, point1.Y + 150, width + 5, height + 5);
            ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNWSE;
            if (right_top_resize_shape)
            {
                pen = new Pen(color1_panel.BackColor, clas.fontsize);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                width = width_pic - e.X;
                height = height_pic - e.Y;
                if (circle)
                {
                    if (fill_shapes)
                    {
                        g.FillEllipse(clas.pp, point1.X + e.X, point1.Y + e.Y, width, height);
                    }
                    else
                    {
                        g.DrawEllipse(pen, point1.X + e.X, point1.Y + e.Y, width, height);
                    }

                }
                else if (rectangle)
                {
                    if (fill_shapes)
                    {
                        g.FillRectangle(clas.pp, point1.X + e.X, point1.Y + e.Y, width, height);
                    }
                    else
                    {
                        g.DrawRectangle(pen, point1.X + e.X, point1.Y + e.Y, width, height);
                    }
                }
                else if (lightening)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Lightning.png"), point1.X+e.X, point1.Y + e.Y, width, height);
                }
                else if (rec_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Rounded rectangular callout.png"), point1.X+e.X, point1.Y + e.Y, width, height);
                }
                else if (oval_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Oval callout.png"), point1.X+e.X, point1.Y + e.Y, width, height);
                }
                else if (cloud_call)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Cloud callout.png"), point1.X+e.X, point1.Y + e.Y, width, height);
                }
                else if (heart)
                {
                    g.DrawImage(Image.FromFile(@"F:\New folder (2)\Paint\Paint\Resources\Heart.png"), point1.X+e.X, point1.Y + e.Y, width, height);
                }
                pictureBox1.Refresh();

                theRectangle = new Rectangle(point1.X + e.X + 10, point1.Y + e.Y + 150, width, height);
                ControlPaint.DrawReversibleFrame(theRectangle, this.BackColor, FrameStyle.Dashed);


            }
        }

        private void left_arrow_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            left_arrow = true;
        }

        private void up_arrow_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            top_arrow = true;
        }

        private void bottom_right_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            bottom_arrow = true;
        }

        private void four_point_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            four_point_star = true;
        }

        private void five_point_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            five_point_star = true;
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
           DialogResult new_page= MessageBox.Show("Are you sure??", "New Page", MessageBoxButtons.YesNo);
            if (Convert.ToString(new_page)=="Yes")
            {
                for (int y = 0; y < bmpMain.Height; y++)
                {
                    for (int x = 0; x < bmpMain.Width; x++)
                    {
                        bmpMain.SetPixel(x, y, Color.White);
                    }
                }

                pictureBox1.Invalidate();
            }
            deactrive_buttons();
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            poligancont = 0;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                bmpMain.Save(saveFileDialog1.FileName+".jpeg");
            }
        }

        private void label_to_textbox_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void label_to_textbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void text_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            text = true;
            
        }

        private void lightning_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            lightening = true;
        }

        private void rec_call_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            rec_call = true;
        }

        private void oval_call_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            oval_call = true;
        }

        private void cloud_call_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            cloud_call = true;
        }

        private void heart_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            heart = true;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                deactrive_buttons();
                bmpMain = new Bitmap(openFileDialog1.FileName);
                g.Clear(pictureBox1.BackColor);
                g = Graphics.FromImage(bmpMain);
                pictureBox1.Refresh();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void pnlBlack_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlDarkRed_Paint(object sender, PaintEventArgs e)
        {

        }

        private void colorpicker_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            color_picker = true;
        }

        private void select_rec_btn_Click(object sender, EventArgs e)
        {
            deactrive_buttons();
            select = true;

        }

        private void rotate_btn_Click(object sender, EventArgs e)
        {
            if (select)
            {
                bmpselect.RotateFlip(RotateFlipType.Rotate90FlipNone);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(bmpselect, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
                pictureBox1.Refresh();
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Size = new Size(pictureBox1.Height, pictureBox1.Width);
                pictureBox1.Refresh();
            }
            
            
       
       }
        private void button3_Click(object sender, EventArgs e)
        {
            if (select)
            {
            bmpselect.RotateFlip(RotateFlipType.Rotate270FlipNone);
            g.Clear(pictureBox1.BackColor);
            bmpMain = new Bitmap(bmpBeforeDrawShape);
            g = Graphics.FromImage(bmpMain);
            g.DrawImage(bmpselect, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
            pictureBox1.Refresh();
            }
             else
            {
                bmpMain.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Size = new Size(pictureBox1.Height, pictureBox1.Width);
                pictureBox1.Refresh();
            }
        }

        private void rotate_180_btn_Click(object sender, EventArgs e)
        {
            if (select)
            {
                bmpselect.RotateFlip(RotateFlipType.Rotate180FlipNone);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(bmpselect, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
                pictureBox1.Refresh();
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.Rotate180FlipNone);
                pictureBox1.Size = new Size(pictureBox1.Height, pictureBox1.Width);
                pictureBox1.Refresh();
            }
            
        }

        private void filip_horizontal_btn_Click(object sender, EventArgs e)
        {
            if(select)
            {
                bmpselect.RotateFlip(RotateFlipType.RotateNoneFlipX);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(bmpselect, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
                pictureBox1.Refresh();
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Size = new Size(pictureBox1.Height, pictureBox1.Width);
                pictureBox1.Refresh();
            }
           
        }

        private void filip_vertical_btn_Click(object sender, EventArgs e)
        {
            if (select)
            {
                bmpselect.RotateFlip(RotateFlipType.RotateNoneFlipY);
                g.Clear(pictureBox1.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawShape);
                g = Graphics.FromImage(bmpMain);
                g.DrawImage(bmpselect, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
                pictureBox1.Refresh();
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Size = new Size(pictureBox1.Height, pictureBox1.Width);
                pictureBox1.Refresh();
            }
         
        }

        private void cut_btn_Click(object sender, EventArgs e)
        {
           //if (select)
           //{
           //    for (int i=0;i<bmpselect.Width;i++)
           //    {
           //        for (int j=0;j<bmpselect.Height;j++)
           //        {
           //            bmpselect.SetPixel(i, j, Color.White);
           //        }
           //    }
               
           //    g.Clear(pictureBox1.BackColor);
           //    bmpMain = new Bitmap(bmpBeforeDrawShape);
           //    g = Graphics.FromImage(bmpMain);
           //    g.DrawImage(bmpselect, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
           //    pictureBox1.Refresh();
                             
           //}
        }

        private void Copy_btn_Click(object sender, EventArgs e)
        {
          //  bmpselect1 = bmpselect;
          //  g.Clear(pictureBox1.BackColor);
          //  bmpMain = new Bitmap(bmpBeforeDrawShape);
          //  g = Graphics.FromImage(bmpMain);
          ////  g.DrawImage(bmpselect1, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
          //  pictureBox1.Refresh();

        }

        private void paste_btn_Click(object sender, EventArgs e)
        {
            //g.Clear(pictureBox1.BackColor);
            //bmpMain = new Bitmap(bmpBeforeDrawShape);
            //g = Graphics.FromImage(bmpMain);
            //g.DrawImage(bmpselect1, new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y));
            //pictureBox1.Refresh();
        }

        private void crop_btn_Click(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(theRectangle.Width, theRectangle.Height);
            bmpMain = new Bitmap(bmpselect, theRectangle.Width, theRectangle.Height);
            g = Graphics.FromImage(bmpMain);
            pictureBox1.Refresh();
        }

        private void CustomColor0_pnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlWhite_Paint(object sender, PaintEventArgs e)
        {

        }

        private void size_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (size_combobox.SelectedItem == size_combobox.Items[0])
             {
                 clas.font_size(2);
             }
            else if (size_combobox.SelectedItem == size_combobox.Items[1])
             {
                 clas.font_size(4);
             }
             else if (size_combobox.SelectedItem == size_combobox.Items[2])
             {
                 clas.font_size(6);
             }
             else if (size_combobox.SelectedItem == size_combobox.Items[3])
             {
                 clas.font_size(8);
             }
            else if (size_combobox.SelectedItem == size_combobox.Items[4])
            {
                clas.font_size(10);
            }
            
        }
       
        //private void the_rectangle_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if ((e.X > point1.X && e.X < point2.X) && (e.Y > point1.Y && e.Y < point2.Y))
        //    {
        //        Cursor.Current = Cursors.SizeAll;
        //    }
        //}

    }
}
