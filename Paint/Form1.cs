using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        Draw draw;
        public Graphics g;
        public Graphics backgroundGraphics;
        Bitmap bmpBackgroundPanel;
        Bitmap bmpMain;
        Bitmap bmpBeforeDrawWithMouseMove; // the image before draw with MouseMove
        Bitmap bmpBeforeDashRect;
        Bitmap bmpBeforeShape;
        Bitmap bmpBeforeResizePicMain;

        public Bitmap bmpCloudCallout;
        public Color[,] clrArrayCloudCallout;
        public Bitmap bmpRoundedRectCallout;
        public Color[,] clrArrayRoundedRectCallout;
        public Bitmap bmpOvalCallout;
        public Color[,] clrArrayOvalCallout;
        public Bitmap bmpHeart;
        public Color[,] clrArrayHeart;
        public Bitmap bmpLightning;
        public Color[,] clrArrayLightning;

        Bitmap[] bitmapsArray = new Bitmap[1];
        int currentIndexOfbitmapsArray;
        int maxIndexOfbitmapsArray;
        const int minIndexOfbitmapsArray = 1;

        Pen pen;
        Color foregroundColor;
        public Color backgroundColor;
        SolidBrush solidBrush;
        SolidBrush textBrush; // used in draw text method
        float penWidth;
        Color activeButtonColor = Color.LightSkyBlue;

        Font textFont = new Font("Arial", 10);

        SolidBrush eraserSolidBrush;
        Rectangle eraserRect;

        public PointF point1;
        public PointF point2;

        // used while drawing polygon
        public PointF firstPoint;
        float minX;
        float maxX;
        float minY;
        float maxY;
        private bool isFirstDown = false;
        PointF[] polygonPointsArray;
        PointF[] polygonPointsRatio;

        const int minSize = 10;
        const int margin = 5;
        public PointF dashRectUpperLeftPoint;
        public PointF dashRectLowerRightPoint;
        enum SurroundedShapes { NONE, OVAL, RECTANGLE, RIGHTTRIANGLE, TRIANGLE, DIAMOND, PENTAGON, HEXAGON, RIGHTARROW, LEFTARROW, UPARROW, DOWNARROW, FOURPOINTSTAR, FIVEPOINTSTAR, SIXPOINTSTAR, TEXT, LINE, POLYGON, BITMAP, CLOUDCALLOUT, ROUNDEDRECTCALLOUT, OVALCALLOUT, HEART, LIGHTNING };
        SurroundedShapes surroundedShape;
        public enum Shape { CLOUDCALLOUT, ROUNDEDRECTCALLOUT, OVALCALLOUT, HEART, LIGHTNING, BITMAP };
        Shape shape;
        enum ResizeSide { LOWERRIGHT, LOW, LOWERLEFT, LEFT, UPPERLEFT, UP, UPPERRIGHT, RIGHT };

        int indexOfLastFilledCustomColorPanel = 0;

        public List<PointF> polygonPoints;

        private bool shouldPickColor = false;
        private bool shouldDrawPencil = false;
        private bool shouldDrawAirbrush = false;
        private bool shouldErase = false;
        private bool shouldDrawLine = false;
        private bool shouldDrawOval = false;
        private bool shouldDrawRect = false;
        private bool shouldDrawRightTriangle = false;
        private bool shouldDrawTriangle = false;
        private bool shouldDrawPolygon = false;
        private bool shouldDrawDiamond = false;
        private bool shouldDrawPentagon = false;
        private bool shouldDrawHexagon = false;
        private bool shouldDrawRightArrow = false;
        private bool shouldDrawLeftArrow = false;
        private bool shouldDrawUpArrow = false;
        private bool shouldDrawDownArrow = false;
        private bool shouldDrawFourPointStar = false;
        private bool shouldDrawFivePointStar = false;
        private bool shouldDrawSixPointStar = false;
        private bool shouldDrawCurve = false;
        private bool shouldDrawCloudCallout = false;
        private bool shouldDrawRoundedRectCallout = false;
        private bool shouldDrawOvalCallout = false;
        private bool shouldDrawHeart = false;
        private bool shouldDrawLightning = false;
        private bool shouldDrawImage = false;

        private bool shouldDrawString = false;
        private string text;
        private RichTextBox richTextBox1;
        private bool isRichTextBoxDrawn = false;

        // resize and move drawn line
        TinySquare tinySquare1;
        TinySquare tinySquare2;
        private bool shouldResizeFromPoint1 = false;
        private bool shouldResizeFromPoint2 = false;
        private bool isTinySquaresOnLineDrawn = false;
        PointF linePoint1;
        PointF linePoint2;

        // resize picMain:
        bool shouldResizePicMainFromRight = false;
        bool shouldResizePicMainFromBottom = false;
        bool shouldResizePicMainFromBottomRight = false;
        TinySquare tinRight;
        TinySquare tinBottom;
        TinySquare tinBottomRight;

        private bool isDashRectDrawn = false;
        private bool shouldResizeFromLowerRight = false;
        private bool shouldResizeFromLow = false;
        private bool shouldResizeFromLowerLeft = false;
        private bool shouldResizeFromLeft = false;
        private bool shouldResizeFromUpperLeft = false;
        private bool shouldResizeFromUp = false;
        private bool shouldResizeFromUpperRight = false;
        private bool shouldResizeFromRight = false;

        private bool shouldMove = false;
        private float diffXFromUpperLeft;
        private float diffYFromUpperLeft;
        private float diffXFromLowerRight;
        private float diffYFromLowerRight;

        // selection:
        private bool shouldRectSelect;
        private bool shouldFreeSelect;
        public Bitmap bmpInsideDashRect;
        float newWidth;
        float newHeight;
        Rectangle dashRect;

        // fill with color:
        List<Point> seeds = new List<Point>();

        // airbrush:
        Random random = new Random();
        int airbrushStrength;
        int airbrushSize;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foregroundColor = pnlForegroundColor.BackColor;
            penWidth = (float)Convert.ToDouble(cmbSize.SelectedItem);
            pen = new Pen(foregroundColor, penWidth);

            eraserSolidBrush = new SolidBrush(picMain.BackColor);
            textBrush = new SolidBrush(pnlForegroundColor.BackColor);
            cmbSize.SelectedIndex = 0;

            draw = new Draw();
            draw.GetForm1(this);

            lblMainPanelSize.Text = picMain.Size.Width.ToString() + " x " + picMain.Size.Height.ToString() + "px";

            foreach (FontFamily font in FontFamily.Families)
            {
                cmbFont.Items.Add(font.Name);
            }
            cmbFont.SelectedText = textFont.Name;
            cmbFontSize.SelectedText = textFont.Size.ToString();

            airbrushStrength = (int)numAirbrushStrength.Value;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            picMain.Size = new Size(pnlBackground.Width - 400, pnlBackground.Height - 50);
            bmpMain = new Bitmap(picMain.Width, picMain.Height);
            bmpBeforeDrawWithMouseMove = new Bitmap(picMain.Width, picMain.Height);
            bmpBeforeDashRect = new Bitmap(picMain.Width, picMain.Height);
            bmpBeforeShape = new Bitmap(picMain.Width, picMain.Height);
            bmpBeforeResizePicMain = new Bitmap(picMain.Width, picMain.Height);
            AddCurrentImageToBitmapsList();

            g = Graphics.FromImage(bmpMain);

            btnUndo.Enabled = false;
            btnUndo.BackgroundImage = Properties.Resources.UndoDisabled;

            btnRedo.Enabled = false;
            btnRedo.BackgroundImage = Properties.Resources.RedoDisabled;

            bmpBackgroundPanel = new Bitmap(pnlBackground.Width, pnlBackground.Height);
            backgroundGraphics = Graphics.FromImage(bmpBackgroundPanel);
            // draw tiny squares around picMain on pnlBackground:
            DrawTinySquaresAroundPicMain();

            bmpCloudCallout = Properties.Resources.Cloud_callout;
            bmpCloudCallout.MakeTransparent(Color.White);
            clrArrayCloudCallout = new Color[bmpCloudCallout.Width, bmpCloudCallout.Height];
            for (int y = 0; y < bmpCloudCallout.Height; y++)
            {
                for (int x = 0; x < bmpCloudCallout.Width; x++)
                {
                    clrArrayCloudCallout[x, y] = bmpCloudCallout.GetPixel(x, y);
                }
            }
            bmpRoundedRectCallout = Properties.Resources.Rounded_rectangular_callout;
            bmpRoundedRectCallout.MakeTransparent(Color.White);
            clrArrayRoundedRectCallout = new Color[bmpRoundedRectCallout.Width, bmpRoundedRectCallout.Height];
            for (int y = 0; y < bmpRoundedRectCallout.Height; y++)
            {
                for (int x = 0; x < bmpRoundedRectCallout.Width; x++)
                {
                    clrArrayRoundedRectCallout[x, y] = bmpRoundedRectCallout.GetPixel(x, y);
                }
            }
            bmpOvalCallout = Properties.Resources.Oval_callout;
            bmpOvalCallout.MakeTransparent(Color.White);
            clrArrayOvalCallout = new Color[bmpOvalCallout.Width, bmpOvalCallout.Height];
            for (int y = 0; y < bmpOvalCallout.Height; y++)
            {
                for (int x = 0; x < bmpOvalCallout.Width; x++)
                {
                    clrArrayOvalCallout[x, y] = bmpOvalCallout.GetPixel(x, y);
                }
            }
            bmpHeart = Properties.Resources.Heart;
            bmpHeart.MakeTransparent(Color.White);
            clrArrayHeart = new Color[bmpHeart.Width, bmpHeart.Height];
            for (int y = 0; y < bmpHeart.Height; y++)
            {
                for (int x = 0; x < bmpHeart.Width; x++)
                {
                    clrArrayHeart[x, y] = bmpHeart.GetPixel(x, y);
                }
            }
            bmpLightning = Properties.Resources.Lightning;
            bmpLightning.MakeTransparent(Color.White);
            clrArrayLightning = new Color[bmpLightning.Width, bmpLightning.Height];
            for (int y = 0; y < bmpLightning.Height; y++)
            {
                for (int x = 0; x < bmpLightning.Width; x++)
                {
                    clrArrayLightning[x, y] = bmpLightning.GetPixel(x, y);
                }
            }
        }
        private void AddToBitmapsArray(Bitmap inputBitmap)
        {
            Array.Resize(ref bitmapsArray, bitmapsArray.Length + 1);
            bitmapsArray[bitmapsArray.Length - 1] = inputBitmap;
        }
        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (isTinySquaresOnLineDrawn)
            {
                if (tinySquare1.IsInside(e.Location, margin))
                {
                    shouldResizeFromPoint1 = true;
                }
                else if (tinySquare2.IsInside(e.Location, margin))
                {
                    shouldResizeFromPoint2 = true;
                }
                else // outside of tiny squares
                {
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDashRect);
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();

                    isTinySquaresOnLineDrawn = false;
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;
                }
            }
            else if (isDashRectDrawn)
            {
                if (draw.lowerRightSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromLowerRight = true;
                }
                else if (draw.lowSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromLow = true;
                }
                else if (draw.lowerLeftSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromLowerLeft = true;
                }
                else if (draw.leftSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromLeft = true;
                }
                else if (draw.upperLeftSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromUpperLeft = true;
                }
                else if (draw.upSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromUp = true;
                }
                else if (draw.upperRightSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromUpperRight = true;
                }
                else if (draw.rightSquare.IsInside(e.Location, margin))
                {
                    shouldResizeFromRight = true;
                }
                else if (e.X > dashRectUpperLeftPoint.X - margin && e.X < dashRectLowerRightPoint.X + margin &&
                         e.Y > dashRectUpperLeftPoint.Y - margin && e.Y < dashRectLowerRightPoint.Y + margin) // inside dash rectangle
                {
                    // move shape
                    shouldMove = true;
                    picMain.Cursor = new Cursor(Properties.Resources.Grip.Handle);

                    diffXFromUpperLeft = e.Location.X - dashRectUpperLeftPoint.X;
                    diffYFromUpperLeft = e.Location.Y - dashRectUpperLeftPoint.Y;
                    diffXFromLowerRight = dashRectLowerRightPoint.X - e.Location.X;
                    diffYFromLowerRight = dashRectLowerRightPoint.Y - e.Location.Y;
                }
                else // out of rectangle
                {
                    // clear the rectangle around shape
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDashRect);
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();

                    if (isRichTextBoxDrawn)
                    {
                        g.Clear(picMain.BackColor);
                        bmpMain = new Bitmap(bmpBeforeShape);
                        g = Graphics.FromImage(bmpMain);

                        text = richTextBox1.Text;
                        g.DrawString(text, textFont, textBrush, new RectangleF(dashRectUpperLeftPoint.X, dashRectUpperLeftPoint.Y, dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                        AddCurrentImageToBitmapsList();

                        picMain.Controls.Remove(richTextBox1);
                        richTextBox1 = null;
                        isRichTextBoxDrawn = false;
                    }

                    isDashRectDrawn = false;
                    btnCrop.Enabled = false;
                    //bmpInsideDashRect = null;
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;
                }
            }
            else if (!shouldDrawPolygon)
            {
                picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
            }

            picMain.DrawToBitmap(bmpBeforeDrawWithMouseMove, new Rectangle(0, 0, picMain.Width, picMain.Height));

            if (btnPencil.BackColor == activeButtonColor && e.Button == MouseButtons.Left)
            {
                shouldDrawPencil = true;
                point1 = e.Location;
            }
            else if (btnEraser.BackColor == activeButtonColor || (btnPencil.BackColor == activeButtonColor && e.Button == MouseButtons.Right))
            {
                shouldErase = true;

                eraserRect = new Rectangle(e.X - (int)penWidth / 2, e.Y - (int)penWidth / 2, (int)penWidth, (int)penWidth);
                g.FillRectangle(eraserSolidBrush, eraserRect);
            }
            else if (btnLine.BackColor == activeButtonColor)
            {
                shouldDrawLine = true;
                point1 = e.Location;
            }
            else if (btnOval.BackColor == activeButtonColor)
            {
                shouldDrawOval = true;
                point1 = e.Location;
            }
            else if (btnRectangle.BackColor == activeButtonColor)
            {
                shouldDrawRect = true;
                point1 = e.Location;
            }
            else if (btnRightTriangle.BackColor == activeButtonColor)
            {
                shouldDrawRightTriangle = true;
                point1 = e.Location;
            }
            else if (btnTriangle.BackColor == activeButtonColor)
            {
                shouldDrawTriangle = true;
                point1 = e.Location;
            }
            else if (btnPolygon.BackColor == activeButtonColor)
            {
                shouldDrawPolygon = true;

                if (isFirstDown)
                {
                    polygonPoints = new List<PointF>();
                    firstPoint = e.Location;

                    //set upper left and lower right points to firstPoint:
                    minX = firstPoint.X;
                    maxX = firstPoint.X;
                    minY = firstPoint.Y;
                    maxY = firstPoint.Y;

                    polygonPoints.Add(firstPoint);
                    point1 = e.Location;
                }
                else
                {
                    point2 = e.Location;
                    if (Math.Abs(point2.X - firstPoint.X) <= 15 && Math.Abs(point2.Y - firstPoint.Y) <= 15)
                    {
                        point2 = firstPoint;
                        polygonPoints.Add(point2);

                        if (chkFill.Checked)
                        {
                            solidBrush = new SolidBrush(backgroundColor);
                            polygonPoints.Add(firstPoint);
                            PointF[] polygonArray = polygonPoints.ToArray();
                            g.FillPolygon(solidBrush, polygonArray);
                            polygonPoints.RemoveAt(polygonPoints.Count - 1);
                        }
                        else // dont fill
                        {
                            g.DrawLine(pen, point1, point2);
                        }

                        if (chkOutline.Checked)
                        {
                            g.DrawLine(pen, point1, point2);
                        }

                        // draw dash rect:
                        if (!isDashRectDrawn)
                        {
                            dashRectUpperLeftPoint = new PointF(minX, minY);
                            dashRectLowerRightPoint = new PointF(maxX, maxY);

                            picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                            draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                            surroundedShape = SurroundedShapes.POLYGON;
                            isDashRectDrawn = true;
                        }

                        point1 = Point.Empty;
                        isFirstDown = true; // polygon colsed

                        polygonPointsArray = polygonPoints.ToArray();
                        SetPolygonPointsRatio();

                        polygonPoints.Clear();

                        AddCurrentImageToBitmapsList();
                    }
                    else if (!point1.IsEmpty)
                    {
                        polygonPoints.Add(point2);

                        if (chkFill.Checked)
                        {
                            polygonPoints.Add(firstPoint);
                            PointF[] polygonArray = polygonPoints.ToArray();
                            solidBrush = new SolidBrush(backgroundColor);
                            g.FillPolygon(solidBrush, polygonArray);
                            polygonPoints.RemoveAt(polygonPoints.Count - 1);
                        }
                        else
                        {
                            g.DrawLine(pen, point1, point2);
                        }

                        if (chkOutline.Checked)
                        {
                            g.DrawLine(pen, point1, point2);
                        }

                        // set upper left and lower right points:
                        if (point2.X < minX)
                        {
                            minX = point2.X;
                        }
                        else if (point2.X > maxX)
                        {
                            maxX = point2.X;
                        }

                        if (point2.Y < minY)
                        {
                            minY = point2.Y;
                        }
                        else if (point2.Y > maxY)
                        {
                            maxY = point2.Y;
                        }

                        point1 = e.Location;
                    }
                }

                picMain.Refresh();
            }
            else if (btnDiamond.BackColor == activeButtonColor)
            {
                shouldDrawDiamond = true;
                point1 = e.Location;
            }
            else if (btnPentagon.BackColor == activeButtonColor)
            {
                shouldDrawPentagon = true;
                point1 = e.Location;
            }
            else if (btnHexagon.BackColor == activeButtonColor)
            {
                shouldDrawHexagon = true;
                point1 = e.Location;
            }
            else if (btnRightArrow.BackColor == activeButtonColor)
            {
                shouldDrawRightArrow = true;
                point1 = e.Location;
            }
            else if (btnLeftArrow.BackColor == activeButtonColor)
            {
                shouldDrawLeftArrow = true;
                point1 = e.Location;
            }
            else if (btnUpArrow.BackColor == activeButtonColor)
            {
                shouldDrawUpArrow = true;
                point1 = e.Location;
            }
            else if (btnDownArrow.BackColor == activeButtonColor)
            {
                shouldDrawDownArrow = true;
                point1 = e.Location;
            }
            else if (btnFourPointStar.BackColor == activeButtonColor)
            {
                shouldDrawFourPointStar = true;
                point1 = e.Location;
            }
            else if (btnFivePointStar.BackColor == activeButtonColor)
            {
                shouldDrawFivePointStar = true;
                point1 = e.Location;
            }
            else if (btnSixPointStar.BackColor == activeButtonColor)
            {
                shouldDrawSixPointStar = true;
                point1 = e.Location;
            }
            else if (btnText.BackColor == activeButtonColor)
            {
                shouldDrawString = true;
                point1 = e.Location;
            }
            else if (btnFillWithColor.BackColor == activeButtonColor)
            {
                FillWithColor(bmpMain, e.Location, foregroundColor);
            }
            else if (btnRectSelection.BackColor == activeButtonColor)
            {
                shouldRectSelect = true;
                point1 = e.Location;

                if (!isDashRectDrawn)
                {
                    picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                }
            }
            else if (btnFreeFromSelection.BackColor == activeButtonColor)
            {
                shouldFreeSelect = true;
                point1 = e.Location;
            }
            else if (btnCloudCallout.BackColor == activeButtonColor)
            {
                shouldDrawCloudCallout = true;
                point1 = e.Location;
            }
            else if (btnRoundedRectCallout.BackColor == activeButtonColor)
            {
                shouldDrawRoundedRectCallout = true;
                point1 = e.Location;
            }
            else if (btnOvalCallout.BackColor == activeButtonColor)
            {
                shouldDrawOvalCallout = true;
                point1 = e.Location;
            }
            else if (btnHeart.BackColor == activeButtonColor)
            {
                shouldDrawHeart = true;
                point1 = e.Location;
            }
            else if (btnLightning.BackColor == activeButtonColor)
            {
                shouldDrawLightning = true;
                point1 = e.Location;
            }
            else if (btnDrawImage.BackColor == activeButtonColor)
            {
                shouldDrawImage = true;
                point1 = e.Location;
            }
            else if (btnAirBrush.BackColor == activeButtonColor)
            {
                shouldDrawAirbrush = true;
                for (int counter = 0; counter < airbrushStrength; counter++)
                {
                    SetRandomPixel(bmpMain, e.Location, airbrushSize);
                }
                picMain.Refresh();
            }
        }

        private void picMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (shouldResizeFromPoint1)
            {
                shouldResizeFromPoint1 = false;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeShape);
                g = Graphics.FromImage(bmpMain);

                linePoint1 = e.Location;
                g.DrawLine(pen, linePoint1, linePoint2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                tinySquare1 = new TinySquare(e.Location.X - Draw.tinySquareSize / 2, e.Location.Y - Draw.tinySquareSize / 2);
                tinySquare2 = new TinySquare(linePoint2.X - Draw.tinySquareSize / 2, linePoint2.Y - Draw.tinySquareSize / 2);
                g.DrawRectangle(Pens.Black, tinySquare1.Location.X, tinySquare1.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                g.DrawRectangle(Pens.Black, tinySquare2.Location.X, tinySquare2.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                picMain.Refresh();
            }
            else if (shouldResizeFromPoint2)
            {
                shouldResizeFromPoint2 = false;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeShape);
                g = Graphics.FromImage(bmpMain);

                linePoint2 = e.Location;
                g.DrawLine(pen, linePoint1, linePoint2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                tinySquare1 = new TinySquare(linePoint1.X - Draw.tinySquareSize / 2, linePoint1.Y - Draw.tinySquareSize / 2);
                tinySquare2 = new TinySquare(e.Location.X - Draw.tinySquareSize / 2, e.Location.Y - Draw.tinySquareSize / 2);
                g.DrawRectangle(Pens.Black, tinySquare1.Location.X, tinySquare1.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                g.DrawRectangle(Pens.Black, tinySquare2.Location.X, tinySquare2.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                picMain.Refresh();
            }
            else if (shouldResizeFromLowerRight)
            {
                shouldResizeFromLowerRight = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.LOWERRIGHT, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromLow)
            {
                shouldResizeFromLow = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.LOW, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromLowerLeft)
            {
                shouldResizeFromLowerLeft = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.LOWERLEFT, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromLeft)
            {
                shouldResizeFromLeft = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.LEFT, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromUpperLeft)
            {
                shouldResizeFromUpperLeft = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.UPPERLEFT, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromUp)
            {
                shouldResizeFromUp = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.UP, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromUpperRight)
            {
                shouldResizeFromUpperRight = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.UPPERRIGHT, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldResizeFromRight)
            {
                shouldResizeFromRight = false;
                point2 = e.Location;

                ResizeShape(ResizeSide.RIGHT, point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldMove)
            {
                shouldMove = false;
                picMain.Cursor = Cursors.SizeAll;
                point2 = e.Location;

                MoveShape(point2);
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                picMain.Refresh();
            }
            else if (shouldDrawPencil)
            {
                shouldDrawPencil = false;
                AddCurrentImageToBitmapsList();
            }
            else if (shouldErase)
            {
                shouldErase = false;
                AddCurrentImageToBitmapsList();
            }
            else if (shouldDrawLine)
            {
                shouldDrawLine = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Line(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    linePoint1 = point1;
                    linePoint2 = point2;

                    if (!isTinySquaresOnLineDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));

                        tinySquare1 = new TinySquare(linePoint1.X - Draw.tinySquareSize / 2, linePoint1.Y - Draw.tinySquareSize / 2);
                        tinySquare2 = new TinySquare(linePoint2.X - Draw.tinySquareSize / 2, linePoint2.Y - Draw.tinySquareSize / 2);
                        g.DrawRectangle(Pens.Black, tinySquare1.Location.X, tinySquare1.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                        g.DrawRectangle(Pens.Black, tinySquare2.Location.X, tinySquare2.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);

                        isTinySquaresOnLineDrawn = true;
                    }
                }
            }
            else if (shouldDrawOval) // ellipse
            {
                shouldDrawOval = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Ellipse(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.OVAL;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawRect)
            {
                shouldDrawRect = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Rectangle(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.RECTANGLE;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawRightTriangle)
            {
                shouldDrawRightTriangle = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.RightTriangle(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.RIGHTTRIANGLE;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawTriangle)
            {
                shouldDrawTriangle = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Triangle(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.TRIANGLE;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawPolygon && isFirstDown && !point1.IsEmpty)
            {
                point2 = e.Location;
                if (Math.Abs(point2.X - point1.X) >= 5 && Math.Abs(point2.Y - point1.Y) >= 5)
                {
                    if (chkFill.Checked && !chkOutline.Checked)
                    {
                        // dont draw any line
                    }
                    else
                    {
                        draw.Line(pen, point1, point2);
                        // dont add to bitmaps list
                    }

                    polygonPoints.Add(point2);

                    // set upper left and lower right points:
                    if (point2.X < minX)
                    {
                        minX = point2.X;
                    }
                    else if (point2.X > maxX)
                    {
                        maxX = point2.X;
                    }

                    if (point2.Y < minY)
                    {
                        minY = point2.Y;
                    }
                    else if (point2.Y > maxY)
                    {
                        maxY = point2.Y;
                    }

                    isFirstDown = false;
                    point1 = e.Location;
                }
                else
                {
                    shouldDrawPolygon = false;
                    polygonPoints.Clear();
                }
            }
            else if (shouldDrawDiamond)
            {
                shouldDrawDiamond = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Diamond(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.DIAMOND;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawPentagon)
            {
                shouldDrawPentagon = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Pentagon(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.PENTAGON;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawHexagon)
            {
                shouldDrawHexagon = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Hexagon(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.HEXAGON;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawRightArrow)
            {
                shouldDrawRightArrow = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.RightArrow(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.RIGHTARROW;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawLeftArrow)
            {
                shouldDrawLeftArrow = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.LeftArrow(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.LEFTARROW;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawUpArrow)
            {
                shouldDrawUpArrow = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.UpArrow(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.UPARROW;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawDownArrow)
            {
                shouldDrawDownArrow = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.DownArrow(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.DOWNARROW;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawFourPointStar)
            {
                shouldDrawFourPointStar = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.FourPointStar(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.FOURPOINTSTAR;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawFivePointStar)
            {
                shouldDrawFivePointStar = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.FivePointStar(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.FIVEPOINTSTAR;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawSixPointStar)
            {
                shouldDrawSixPointStar = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.SixPointStar(pen, point1, point2);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.SIXPOINTSTAR;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawString)
            {
                shouldDrawString = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        richTextBox1 = new RichTextBox();
                        //richTextBox1.BackColor = Color.Transparent;
                        richTextBox1.Text = string.Empty;
                        text = string.Empty;
                        richTextBox1.ForeColor = foregroundColor;
                        richTextBox1.BorderStyle = BorderStyle.None;
                        richTextBox1.Location = new Point((int)dashRectUpperLeftPoint.X + 1, (int)dashRectUpperLeftPoint.Y + 1);
                        richTextBox1.Width = (int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X) - 1;
                        richTextBox1.Height = (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y) - 1;
                        picMain.Controls.Add(richTextBox1);
                        isRichTextBoxDrawn = true;

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.TEXT;
                        isDashRectDrawn = true;

                        SetFontStyleButtonsBackColor(FontStyle.Regular);
                        textFont = new Font(textFont, FontStyle.Regular);
                        richTextBox1.Font = textFont;
                    }
                }
            }
            else if (shouldPickColor)
            {
                Color pickedColor = bmpMain.GetPixel(e.X, e.Y);

                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pickedColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pickedColor;
                }
            }
            else if (shouldRectSelect)
            {
                shouldRectSelect = false;
                point2 = e.Location;

                if (point2.X >= picMain.Width || point2.Y >= picMain.Height)
                {
                    float pX = point2.X >= picMain.Width ? picMain.Width : point2.X;
                    float pY = point2.Y >= picMain.Height ? picMain.Height : point2.Y;

                    point2 = new PointF(pX, pY);
                }
                if (point2.X < 0 || point2.Y < 0)
                {
                    float pX = point2.X < 0 ? 0 : point2.X;
                    float pY = point2.Y < 0 ? 0 : point2.Y;

                    point2 = new PointF(pX, pY);
                }

                if (point1 != point2)
                {
                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);
                        dashRect = new Rectangle((int)dashRectUpperLeftPoint.X, (int)dashRectUpperLeftPoint.Y, (int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X), (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));

                        bmpInsideDashRect = bmpBeforeDashRect.Clone(dashRect, bmpBeforeDashRect.PixelFormat);
                        btnCrop.Enabled = true;
                        surroundedShape = SurroundedShapes.BITMAP;
                        draw.Shape(pen, point1, point2, Shape.BITMAP);
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        isDashRectDrawn = true;
                        AddCurrentImageToBitmapsList();

                        for (int y = dashRect.Top; y < dashRect.Bottom; y++)
                        {
                            for (int x = dashRect.Left; x < dashRect.Right; x++)
                            {
                                bmpBeforeShape.SetPixel(x, y, picMain.BackColor);
                            }
                        }
                    }
                }
            }
            else if (shouldFreeSelect)
            {
                shouldFreeSelect = false;
            }
            else if (shouldDrawCloudCallout)
            {
                shouldDrawCloudCallout = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Shape(pen, point1, point2, Shape.CLOUDCALLOUT);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.CLOUDCALLOUT;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawRoundedRectCallout)
            {
                shouldDrawRoundedRectCallout = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Shape(pen, point1, point2, Shape.ROUNDEDRECTCALLOUT);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.ROUNDEDRECTCALLOUT;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawOvalCallout)
            {
                shouldDrawOvalCallout = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Shape(pen, point1, point2, Shape.OVALCALLOUT);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.OVALCALLOUT;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawHeart)
            {
                shouldDrawHeart = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Shape(pen, point1, point2, Shape.HEART);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.HEART;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawLightning)
            {
                shouldDrawLightning = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Shape(pen, point1, point2, Shape.LIGHTNING);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.LIGHTNING;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawImage)
            {
                shouldDrawImage = false;
                point2 = e.Location;

                if (point1 != point2)
                {
                    draw.Shape(pen, point1, point2, Shape.BITMAP);
                    AddCurrentImageToBitmapsList();

                    if (!isDashRectDrawn)
                    {
                        SetDashRectPoints(point1, point2);

                        picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                        draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                        surroundedShape = SurroundedShapes.BITMAP;
                        isDashRectDrawn = true;
                    }
                }
            }
            else if (shouldDrawAirbrush)
            {
                shouldDrawAirbrush = false;
            }

            if (shouldResizePicMainFromRight)
            {
                shouldResizePicMainFromRight = false;
            }
            else if (shouldResizePicMainFromBottom)
            {
                shouldResizePicMainFromBottom = false;
            }
            else if (shouldResizePicMainFromBottomRight)
            {
                shouldResizePicMainFromBottomRight = false;
            }

            picMain.Refresh();
        }


        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            lblCursorLocation.Text = e.X.ToString() + ", " + e.Y.ToString() + "px";

            if (isTinySquaresOnLineDrawn)
            {
                if (tinySquare1.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNS;
                }
                else if (tinySquare2.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNS;
                }
                else
                {
                    picMain.Cursor = Cursors.Cross;
                }

                if (shouldResizeFromPoint1)
                {
                    picMain.Cursor = Cursors.SizeNS;

                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeShape);
                    g = Graphics.FromImage(bmpMain);

                    g.DrawLine(pen, e.Location, linePoint2);
                    tinySquare1 = new TinySquare(e.Location.X - Draw.tinySquareSize / 2, e.Location.Y - Draw.tinySquareSize / 2);
                    tinySquare2 = new TinySquare(linePoint2.X - Draw.tinySquareSize / 2, linePoint2.Y - Draw.tinySquareSize / 2);
                    g.DrawRectangle(Pens.Black, tinySquare1.Location.X, tinySquare1.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                    g.DrawRectangle(Pens.Black, tinySquare2.Location.X, tinySquare2.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                    picMain.Refresh();
                }
                else if (shouldResizeFromPoint2)
                {
                    picMain.Cursor = Cursors.SizeNS;

                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeShape);
                    g = Graphics.FromImage(bmpMain);

                    g.DrawLine(pen, linePoint1, e.Location);
                    tinySquare1 = new TinySquare(linePoint1.X - Draw.tinySquareSize / 2, linePoint1.Y - Draw.tinySquareSize / 2);
                    tinySquare2 = new TinySquare(e.Location.X - Draw.tinySquareSize / 2, e.Location.Y - Draw.tinySquareSize / 2);
                    g.DrawRectangle(Pens.Black, tinySquare1.Location.X, tinySquare1.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                    g.DrawRectangle(Pens.Black, tinySquare2.Location.X, tinySquare2.Location.Y, Draw.tinySquareSize, Draw.tinySquareSize);
                    picMain.Refresh();
                }
            }
            else if (isDashRectDrawn)
            {
                // set cursor:
                if (draw.upperLeftSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNWSE;
                }
                else if (draw.upSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNS;
                }
                else if (draw.upperRightSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNESW;
                }
                else if (draw.rightSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeWE;
                }
                else if (draw.lowerRightSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNWSE;
                }
                else if (draw.lowSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNS;
                }
                else if (draw.lowerLeftSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeNESW;
                }
                else if (draw.leftSquare.IsInside(e.Location, margin))
                {
                    picMain.Cursor = Cursors.SizeWE;
                }
                else if (e.X > dashRectUpperLeftPoint.X - margin && e.X < dashRectLowerRightPoint.X + margin &&
                         e.Y > dashRectUpperLeftPoint.Y - margin && e.Y < dashRectLowerRightPoint.Y + margin) // inside dash rect
                {
                    if (shouldMove)
                    {
                        picMain.Cursor = new Cursor(Properties.Resources.Grip.Handle);
                    }
                    else
                    {
                        picMain.Cursor = Cursors.SizeAll;
                    }
                }
                else // outside dash rect
                {
                    picMain.Cursor = Cursors.Cross;
                }

                // resize:
                if (shouldResizeFromLowerRight)
                {
                    picMain.Cursor = Cursors.SizeNWSE;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.LOWERRIGHT, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromLow)
                {
                    picMain.Cursor = Cursors.SizeNS;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.LOW, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromLowerLeft)
                {
                    picMain.Cursor = Cursors.SizeNESW;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.LOWERLEFT, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromLeft)
                {
                    picMain.Cursor = Cursors.SizeWE;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.LEFT, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromUpperLeft)
                {
                    picMain.Cursor = Cursors.SizeNWSE;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.UPPERLEFT, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromUp)
                {
                    picMain.Cursor = Cursors.SizeNS;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.UP, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromUpperRight)
                {
                    picMain.Cursor = Cursors.SizeNESW;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.UPPERRIGHT, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldResizeFromRight)
                {
                    picMain.Cursor = Cursors.SizeWE;
                    point2 = e.Location;

                    ResizeShape(ResizeSide.RIGHT, point2);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    picMain.Refresh();
                }
                else if (shouldMove) // move
                {
                    point2 = e.Location;

                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeShape);
                    g = Graphics.FromImage(bmpMain);

                    dashRectUpperLeftPoint = new PointF(point2.X - diffXFromUpperLeft, point2.Y - diffYFromUpperLeft);
                    dashRectLowerRightPoint = new PointF(point2.X + diffXFromLowerRight, point2.Y + diffYFromLowerRight);

                    DrawSurroundedShape(surroundedShape);
                    draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);

                    picMain.Refresh();
                }
            }
            else if (shouldDrawPencil) // draw
            {
                point2 = e.Location;
                draw.Line(pen, point1, point2);
                picMain.Refresh();
                point1 = point2;
            }
            else if ((btnPencil.BackColor == activeButtonColor || btnEraser.BackColor == activeButtonColor) && shouldErase) // erase
            {
                eraserRect = new Rectangle(e.X - (int)penWidth / 2, e.Y - (int)penWidth / 2, (int)penWidth, (int)penWidth);
                g.FillRectangle(eraserSolidBrush, eraserRect);
                picMain.Refresh();
            }
            else if (shouldDrawLine)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Line(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawOval)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Ellipse(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawRect)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Rectangle(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawRightTriangle)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.RightTriangle(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawTriangle)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Triangle(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawPolygon && isFirstDown && !point1.IsEmpty)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                if (Math.Abs(point2.X - point1.X) >= 5 && Math.Abs(point2.Y - point1.Y) >= 5)
                {
                    if (chkFill.Checked && !chkOutline.Checked)
                    {
                        // dont draw any line
                    }
                    else
                    {
                        draw.Line(pen, point1, point2);
                        // dont add to bitmaps list
                    }
                }

                picMain.Refresh();
            }
            else if (shouldDrawDiamond)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Diamond(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawPentagon)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Pentagon(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawHexagon)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Hexagon(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawRightArrow)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.RightArrow(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawLeftArrow)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.LeftArrow(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawUpArrow)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.UpArrow(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawDownArrow)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.DownArrow(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawFourPointStar)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.FourPointStar(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawFivePointStar)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.FivePointStar(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawSixPointStar)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.SixPointStar(pen, point1, point2);
                picMain.Refresh();
            }
            else if (shouldDrawString)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.DashRectangle(point1, point2);
                picMain.Refresh();
            }
            else if (shouldRectSelect)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.DashRectangle(point1, point2);
                picMain.Refresh();
            }
            else if (shouldFreeSelect)
            {

            }
            else if (shouldDrawCloudCallout)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Shape(pen, point1, point2, Shape.CLOUDCALLOUT);
                picMain.Refresh();
            }
            else if (shouldDrawRoundedRectCallout)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Shape(pen, point1, point2, Shape.ROUNDEDRECTCALLOUT);
                picMain.Refresh();
            }
            else if (shouldDrawOvalCallout)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Shape(pen, point1, point2, Shape.OVALCALLOUT);
                picMain.Refresh();
            }
            else if (shouldDrawHeart)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Shape(pen, point1, point2, Shape.HEART);
                picMain.Refresh();
            }
            else if (shouldDrawLightning)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Shape(pen, point1, point2, Shape.LIGHTNING);
                picMain.Refresh();
            }
            else if (shouldDrawImage)
            {
                point2 = e.Location;

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeDrawWithMouseMove);
                g = Graphics.FromImage(bmpMain);

                draw.Shape(pen, point1, point2, Shape.BITMAP);
                picMain.Refresh();
            }
            else if (shouldDrawAirbrush)
            {
                for (int counter = 0; counter < airbrushStrength; counter++)
                {
                    SetRandomPixel(bmpMain, e.Location, airbrushSize);
                }
                picMain.Refresh();
            }
        }
        private void picMain_MouseLeave(object sender, EventArgs e)
        {
            lblCursorLocation.Text = string.Empty;
        }
        private void btnPencil_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPencil);
        }
        private void ActivateButton(Button button)
        {
            // set buttons backcolor:
            DeactivateAllButtons();

            // activate button:
            button.BackColor = activeButtonColor;
        }
        private void DeactivateAllButtons()
        {
            // deactivate all buttons in grpShapes:
            for (int i = 0; i < grpShapes.Controls.Count; i++)
            {
                grpShapes.Controls[i].BackColor = Color.Transparent;
            }
            // deactivate all buttons in grpTools:
            for (int i = 0; i < grpTools.Controls.Count; i++)
            {
                grpTools.Controls[i].BackColor = Color.Transparent;
            }
            // deactivate all buttons in grpImage:
            for (int i = 0; i < grpImage.Controls.Count; i++)
            {
                grpImage.Controls[i].BackColor = Color.Transparent;
            }
        }
        private void btnEraser_Click(object sender, EventArgs e)
        {
            ActivateButton(btnEraser);
        }
        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            penWidth = (float)Convert.ToDouble(cmbSize.SelectedItem);
            pen.Width = penWidth;
            airbrushSize = (int)penWidth * 2;

            if (btnEraser.BackColor == activeButtonColor)
            {
                picMain.Cursor = GetEraserCursor();
            }
        }
        private Cursor GetEraserCursor()
        {
            Cursor eraserCursor = Cursors.Default;

            switch ((int)pen.Width)
            {
                case 2:
                    eraserCursor = new Cursor(Properties.Resources.Eraser02.Handle);
                    break;
                case 4:
                    eraserCursor = new Cursor(Properties.Resources.Eraser04.Handle);
                    break;
                case 6:
                    eraserCursor = new Cursor(Properties.Resources.Eraser06.Handle);
                    break;
                case 8:
                    eraserCursor = new Cursor(Properties.Resources.Eraser08.Handle);
                    break;
                case 10:
                    eraserCursor = new Cursor(Properties.Resources.Eraser10.Handle);
                    break;
            }

            return eraserCursor;
        }
        private void pnlForegroundColor_BackColorChanged(object sender, EventArgs e)
        {
            foregroundColor = pnlForegroundColor.BackColor;
            pen.Color = foregroundColor;
            textBrush.Color = foregroundColor;
        }
        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLine);
        }
        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            ActivateButton(btnOval);
        }
        private void btnDrawRect_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRectangle);
        }
        private void btnPencil_BackColorChanged(object sender, EventArgs e)
        {
            if (btnPencil.BackColor == activeButtonColor)
            {
                picMain.Cursor = new Cursor(Properties.Resources.CursorPencil.Handle);
            }
            else
            {
                shouldDrawPencil = false;
            }
        }
        private void btnEraser_BackColorChanged(object sender, EventArgs e)
        {
            if (btnEraser.BackColor == activeButtonColor)
            {
                picMain.Cursor = GetEraserCursor();
            }
            else
            {
                shouldErase = false;
            }
        }
        private void btnDrawLine_BackColorChanged(object sender, EventArgs e)
        {
            if (btnLine.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawLine = false;
            }
        }
        private void btnDrawCircle_BackColorChanged(object sender, EventArgs e)
        {
            if (btnOval.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawOval = false;
            }
        }
        private void btnDrawRect_BackColorChanged(object sender, EventArgs e)
        {
            if (btnRectangle.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawRect = false;
            }
        }
        private void btnRightTriangle_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRightTriangle);
        }
        private void btnDrawRightTriangle_BackColorChanged(object sender, EventArgs e)
        {
            if (btnRightTriangle.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawRightTriangle = false;
            }
        }
        private void btnDrawTriangle_Click(object sender, EventArgs e)
        {
            ActivateButton(btnTriangle);
        }
        private void btnDrawTriangle_BackColorChanged(object sender, EventArgs e)
        {
            if (btnTriangle.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawTriangle = false;
            }
        }
        private void btnDrawPolygon_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPolygon);
            isFirstDown = true;
        }
        private void btnDrawPolygon_BackColorChanged(object sender, EventArgs e)
        {
            if (btnPolygon.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawPolygon = false;
            }
        }
        private void btnDrawDiamond_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDiamond);
        }
        private void btnDrawDiamond_BackColorChanged(object sender, EventArgs e)
        {
            if (btnDiamond.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawDiamond = false;
            }
        }
        private void btnDrawPentagon_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPentagon);
        }
        private void btnDrawPentagon_BackColorChanged(object sender, EventArgs e)
        {
            if (btnPentagon.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawPentagon = false;
            }
        }
        private void btnDrawHexagon_Click(object sender, EventArgs e)
        {
            ActivateButton(btnHexagon);
        }
        private void btnDrawHexagon_BackColorChanged(object sender, EventArgs e)
        {
            if (btnHexagon.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawHexagon = false;
            }
        }
        private void btnDrawRightArrow_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRightArrow);
        }
        private void btnDrawRightArrow_BackColorChanged(object sender, EventArgs e)
        {
            if (btnRightArrow.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawRightArrow = false;
            }
        }
        private void btnDrawLeftArrow_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLeftArrow);
        }
        private void btnDrawLeftArrow_BackColorChanged(object sender, EventArgs e)
        {
            if (btnLeftArrow.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawLeftArrow = false;
            }
        }
        private void btnDrawUpArrow_Click(object sender, EventArgs e)
        {
            ActivateButton(btnUpArrow);
        }
        private void btnDrawUpArrow_BackColorChanged(object sender, EventArgs e)
        {
            if (btnUpArrow.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawUpArrow = false;
            }
        }
        private void btnDownArrow_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDownArrow);
        }
        private void btnDownArrow_BackColorChanged(object sender, EventArgs e)
        {
            if (btnDownArrow.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawDownArrow = false;
            }
        }
        private void btnFourPointStar_Click(object sender, EventArgs e)
        {
            ActivateButton(btnFourPointStar);
        }
        private void btnFourPointStar_BackColorChanged(object sender, EventArgs e)
        {
            if (btnFourPointStar.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawFourPointStar = false;
            }
        }
        private void btnFivePointStar_Click(object sender, EventArgs e)
        {
            ActivateButton(btnFivePointStar);
        }
        private void btnFivePointStar_BackColorChanged(object sender, EventArgs e)
        {
            if (btnFivePointStar.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawFivePointStar = false;
            }
        }
        private void btnSixPointStar_Click(object sender, EventArgs e)
        {
            ActivateButton(btnSixPointStar);
        }
        private void btnSixPointStar_BackColorChanged(object sender, EventArgs e)
        {
            if (btnSixPointStar.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawSixPointStar = false;
            }
        }
        private void btnEditColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = colorDialog1.Color;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = colorDialog1.Color;
                }

                GetFirstVoidCustomColorPanel().BackColor = colorDialog1.Color;
                indexOfLastFilledCustomColorPanel++;
            }
        }
        private Panel GetFirstVoidCustomColorPanel()
        {
            switch (indexOfLastFilledCustomColorPanel)
            {
                case 0:
                    return FindPanelByName("pnlCustomColor0");

                case 1:
                    return FindPanelByName("pnlCustomColor1");

                case 2:
                    return FindPanelByName("pnlCustomColor2");

                case 3:
                    return FindPanelByName("pnlCustomColor3");

                case 4:
                    return FindPanelByName("pnlCustomColor4");

                case 5:
                    return FindPanelByName("pnlCustomColor5");

                case 6:
                    return FindPanelByName("pnlCustomColor6");

                case 7:
                    return FindPanelByName("pnlCustomColor7");

                case 8:
                    return FindPanelByName("pnlCustomColor8");

                case 9:
                    return FindPanelByName("pnlCustomColor9");

                default:
                    ShiftCustomColorsToLeft();
                    return FindPanelByName("pnlCustomColor9");
            }
        }
        private Panel FindPanelByName(string panelName)
        {
            for (int i = 0; i < grpCustomColorPanels.Controls.Count; i++)
            {
                if (grpCustomColorPanels.Controls[i].Name == panelName) // if found
                {
                    return (Panel)grpCustomColorPanels.Controls[i];
                }
            }

            Panel panel = new Panel();
            panel.Name = "false";
            return panel;
        }
        private void ShiftCustomColorsToLeft()
        {
            pnlCustomColor0.BackColor = pnlCustomColor1.BackColor;
            pnlCustomColor1.BackColor = pnlCustomColor2.BackColor;
            pnlCustomColor2.BackColor = pnlCustomColor3.BackColor;
            pnlCustomColor3.BackColor = pnlCustomColor4.BackColor;
            pnlCustomColor4.BackColor = pnlCustomColor5.BackColor;
            pnlCustomColor5.BackColor = pnlCustomColor6.BackColor;
            pnlCustomColor6.BackColor = pnlCustomColor7.BackColor;
            pnlCustomColor7.BackColor = pnlCustomColor8.BackColor;
            pnlCustomColor8.BackColor = pnlCustomColor9.BackColor;
        }
        private void pnlColor1_Click(object sender, EventArgs e)
        {
            ActivatePanel(pnlColor1);
        }
        private void pnlForegroundColor_Click(object sender, EventArgs e)
        {
            ActivatePanel(pnlColor1);
        }
        private void lblColor1_Click(object sender, EventArgs e)
        {
            ActivatePanel(pnlColor1);
        }
        private void pnlColor2_Click(object sender, EventArgs e)
        {
            ActivatePanel(pnlColor2);
        }
        private void pnlBackgroudColor_Click(object sender, EventArgs e)
        {
            ActivatePanel(pnlColor2);
        }
        private void lblColor2_Click(object sender, EventArgs e)
        {
            ActivatePanel(pnlColor2);
        }
        private void ActivatePanel(Panel panel)
        {
            if (panel.Name == pnlColor1.Name)
            {
                pnlColor2.BorderStyle = BorderStyle.None;
                pnlColor1.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (panel.Name == pnlColor2.Name)
            {
                pnlColor1.BorderStyle = BorderStyle.None;
                pnlColor2.BorderStyle = BorderStyle.Fixed3D;
            }
        }
        private void pnlBackgroudColor_BackColorChanged(object sender, EventArgs e)
        {
            backgroundColor = pnlBackgroudColor.BackColor;
        }
        private void pnlBlack_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlBlack.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlBlack.BackColor;
            }
        }
        private void pnlWhite_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlWhite.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlWhite.BackColor;
            }
        }
        private void pnlGray50_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlGray50.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlGray50.BackColor;
            }
        }
        private void pnlGray25_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlGray25.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlGray25.BackColor;
            }
        }
        private void pnlDarkRed_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlDarkRed.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlDarkRed.BackColor;
            }
        }
        private void pnlBrown_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlBrown.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlBrown.BackColor;
            }
        }
        private void pnlRed_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlRed.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlRed.BackColor;
            }
        }
        private void pnlLightPink_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlLightPink.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlLightPink.BackColor;
            }
        }
        private void pnlOrange_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlOrange.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlOrange.BackColor;
            }
        }
        private void pnlGold_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlGold.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlGold.BackColor;
            }
        }
        private void pnlYellow_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlYellow.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlYellow.BackColor;
            }
        }
        private void pnlLightYellow_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlLightYellow.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlLightYellow.BackColor;
            }
        }
        private void pnlGreen_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlGreen.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlGreen.BackColor;
            }
        }
        private void pnlLime_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlLime.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlLime.BackColor;
            }
        }
        private void pnlTurquoise_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlTurquoise.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlTurquoise.BackColor;
            }
        }
        private void pnlPaleTurquoise_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlPaleTurquoise.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlPaleTurquoise.BackColor;
            }
        }
        private void pnlIndigo_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlIndigo.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlIndigo.BackColor;
            }
        }
        private void pnlSteelBlue_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlSteelBlue.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlSteelBlue.BackColor;
            }
        }
        private void pnlPurple_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlPurple.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlPurple.BackColor;
            }
        }
        private void pnlLavender_Click(object sender, EventArgs e)
        {
            if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlForegroundColor.BackColor = pnlLavender.BackColor;
            }
            else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
            {
                pnlBackgroudColor.BackColor = pnlLavender.BackColor;
            }
        }
        private void pnlCustomColor0_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor0.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor0.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor0.BackColor;
                }
            }
        }
        private void pnlCustomColor1_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor1.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor1.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor1.BackColor;
                }
            }
        }
        private void pnlCustomColor2_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor2.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor2.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor2.BackColor;
                }
            }
        }
        private void pnlCustomColor3_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor3.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor3.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor3.BackColor;
                }
            }
        }
        private void pnlCustomColor4_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor4.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor4.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor4.BackColor;
                }
            }
        }
        private void pnlCustomColor5_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor5.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor5.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor5.BackColor;
                }
            }
        }
        private void pnlCustomColor6_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor6.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor6.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor6.BackColor;
                }
            }
        }
        private void pnlCustomColor7_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor7.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor7.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor7.BackColor;
                }
            }
        }
        private void pnlCustomColor8_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor8.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor8.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor8.BackColor;
                }
            }
        }
        private void pnlCustomColor9_Click(object sender, EventArgs e)
        {
            if (pnlCustomColor9.BackColor != Color.Transparent)
            {
                if (pnlColor1.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlForegroundColor.BackColor = pnlCustomColor9.BackColor;
                }
                else if (pnlColor2.BorderStyle == BorderStyle.Fixed3D)
                {
                    pnlBackgroudColor.BackColor = pnlCustomColor9.BackColor;
                }
            }
        }
        private void chkFill_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFill.Checked)
            {
                chkOutline.Enabled = true;
            }
            else
            {
                chkOutline.Checked = false;
                chkOutline.Enabled = false;
            }
        }
        private void picMain_SizeChanged(object sender, EventArgs e)
        {
            lblMainPanelSize.Text = picMain.Size.Width.ToString() + " x " + picMain.Size.Height.ToString() + "px";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap savingBitmap = new Bitmap(picMain.Width, picMain.Height);
                picMain.Invalidate(); // call panelPaint event to show bitmap on panel
                picMain.DrawToBitmap(savingBitmap, new Rectangle(0, 0, picMain.Size.Width, picMain.Size.Height)); // save graphics on panel to bitmap

                savingBitmap.Save(saveFileDialog1.FileName); // save the image in bitmap to file
            }
        }
        private void picMain_Paint(object sender, PaintEventArgs e) // this event will be occured by picMain.Refresh()/Validate()
        {
            e.Graphics.DrawImage(bmpMain, 0, 0); // Show bitmap on panel
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmpMain = new Bitmap(Image.FromFile(openFileDialog1.FileName), new Size(picMain.Width, picMain.Height)); // set image from file to bitmap
                g = Graphics.FromImage(bmpMain);
                picMain.Invalidate(); // call panelPaint event to show bitmap on panel
            }
        }
        private void AddCurrentImageToBitmapsList()
        {
            Bitmap b = new Bitmap(picMain.Width, picMain.Height); // create new bitmap
            picMain.DrawToBitmap(b, new Rectangle(0, 0, picMain.Size.Width, picMain.Size.Height)); // draw current image to new bitmap
            AddToBitmapsArray(b); // add new bitmap to bitmapList
            currentIndexOfbitmapsArray++;
            maxIndexOfbitmapsArray = currentIndexOfbitmapsArray;

            btnRedo.Enabled = false;
            btnRedo.BackgroundImage = Properties.Resources.RedoDisabled;

            if (currentIndexOfbitmapsArray > minIndexOfbitmapsArray)
            {
                btnUndo.Enabled = true;
                btnUndo.BackgroundImage = Properties.Resources.Undo;
            }
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (currentIndexOfbitmapsArray > 0)
            {
                currentIndexOfbitmapsArray--;
                if (currentIndexOfbitmapsArray == minIndexOfbitmapsArray)
                {
                    btnUndo.Enabled = false;
                    btnUndo.BackgroundImage = Properties.Resources.UndoDisabled;
                }

                bmpMain = bitmapsArray[currentIndexOfbitmapsArray];
                if (bmpMain != null)
                {
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();
                }

                btnRedo.Enabled = true;
                btnRedo.BackgroundImage = Properties.Resources.Redo;
            }
        }
        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (currentIndexOfbitmapsArray < maxIndexOfbitmapsArray)
            {
                currentIndexOfbitmapsArray++;
                if (currentIndexOfbitmapsArray == maxIndexOfbitmapsArray)
                {
                    btnRedo.Enabled = false;
                    btnRedo.BackgroundImage = Properties.Resources.RedoDisabled;
                }

                bmpMain = bitmapsArray[currentIndexOfbitmapsArray];
                if (bmpMain != null)
                {
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();
                }

                btnUndo.Enabled = true;
                btnUndo.BackgroundImage = Properties.Resources.Undo;
            }
        }
        private void btnCurve_Click(object sender, EventArgs e)
        {
            ActivateButton(btnCurve);
        }
        private void btnCurve_BackColorChanged(object sender, EventArgs e)
        {
            if (btnCurve.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldDrawCurve = false;
            }
        }
        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            ActivateButton(btnColorPicker);
        }
        private void btnColorPicker_BackColorChanged(object sender, EventArgs e)
        {
            if (btnColorPicker.BackColor == activeButtonColor)
            {
                shouldPickColor = true;
                picMain.Cursor = new Cursor(Properties.Resources.ColorPicker.Handle);
            }
            else
            {
                shouldPickColor = false;
            }
        }
        private void btnText_Click(object sender, EventArgs e)
        {
            ActivateButton(btnText);
        }
        private void btnText_BackColorChanged(object sender, EventArgs e)
        {
            if (btnText.BackColor == activeButtonColor)
            {
                grpFont.Visible = true;
                picMain.Cursor = Cursors.IBeam;
            }
            else
            {
                shouldDrawString = false;
                grpFont.Visible = false;
            }
        }
        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            textFont = new Font(cmbFont.SelectedItem.ToString(), textFont.Size, textFont.Style);
            richTextBox1.Font = textFont;
        }
        private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            textFont = new Font(textFont.FontFamily.Name, (float)Convert.ToDouble(cmbFontSize.SelectedItem), textFont.Style);
            richTextBox1.Font = textFont;
        }
        private void btnBold_Click(object sender, EventArgs e)
        {
            if (richTextBox1 != null)
            {
                Font currentFont = richTextBox1.SelectionFont;

                if (btnBold.BackColor != Color.Orange)
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style | FontStyle.Bold);
                    textFont = richTextBox1.Font;
                }
                else
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style & ~FontStyle.Bold);
                    textFont = richTextBox1.Font;
                }

                SetFontStyleButtonsBackColor(richTextBox1.Font.Style);
            }
        }


        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox1 != null)
            {
                Font currentFont = richTextBox1.SelectionFont;

                if (btnItalic.BackColor != Color.Orange)
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style | FontStyle.Italic);
                    textFont = richTextBox1.Font;
                }
                else
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style & ~FontStyle.Italic);
                    textFont = richTextBox1.Font;
                }

                SetFontStyleButtonsBackColor(richTextBox1.Font.Style);
            }
        }
        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox1 != null)
            {
                Font currentFont = richTextBox1.SelectionFont;

                if (btnUnderline.BackColor != Color.Orange)
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style | FontStyle.Underline);
                    textFont = richTextBox1.Font;
                }
                else
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style & ~FontStyle.Underline);
                    textFont = richTextBox1.Font;
                }

                SetFontStyleButtonsBackColor(richTextBox1.Font.Style);
            }
        }
        private void btnStrikeout_Click(object sender, EventArgs e)
        {
            if (richTextBox1 != null)
            {
                Font currentFont = richTextBox1.SelectionFont;

                if (btnStrikeout.BackColor != Color.Orange)
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style | FontStyle.Strikeout);
                    textFont = richTextBox1.Font;
                }
                else
                {
                    richTextBox1.Font = new Font(currentFont, currentFont.Style & ~FontStyle.Strikeout);
                    textFont = richTextBox1.Font;
                }

                SetFontStyleButtonsBackColor(richTextBox1.Font.Style);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblZoomLevel.Text = trackBar1.Value.ToString() + "%";
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value <= trackBar1.Maximum - trackBar1.LargeChange)
            {
                trackBar1.Value += trackBar1.LargeChange;
            }
            else
            {
                trackBar1.Value = trackBar1.Maximum;
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value >= trackBar1.Minimum + trackBar1.LargeChange)
            {
                trackBar1.Value -= trackBar1.LargeChange;
            }
            else
            {
                trackBar1.Value = trackBar1.Minimum;
            }
        }


        private void btnFillWithColor_Click(object sender, EventArgs e)
        {
            ActivateButton(btnFillWithColor);
        }

        private void btnFillWithColor_BackColorChanged(object sender, EventArgs e)
        {
            if (btnFillWithColor.BackColor == activeButtonColor)
            {
                picMain.Cursor = new Cursor(Properties.Resources.Fill.Handle);
                //lblFillTolerance.Visible = true;
                //numFillTolerance.Visible = true;
            }
            else
            {
                //lblFillTolerance.Visible = false;
                //numFillTolerance.Visible = false;
            }
        }

        private void FillWithColor(Bitmap bmp, Point point, Color replacementColor)
        {
            Color initialColor = bmp.GetPixel(point.X, point.Y);
            int tolerance = (int)Math.Round((double)numFillTolerance.Value * 256.0 / 100.0);

            if (initialColor == replacementColor)
            {
                return;
            }

            seeds.Add(point);
            int x = point.X;
            int y = point.Y;

            while (seeds.Count > 0)
            {
                FillRow(bmp, seeds[seeds.Count - 1], initialColor, replacementColor, tolerance);
            }
        }

        private void FillRow(Bitmap bmp, Point point, Color initialColor, Color replacementColor, int tolerance)
        {
            bool aboveRowHasSeed = false;
            bool belowRowHasSeed = false;

            int x = point.X;
            int y = point.Y;

            // toward right:
            while (bmp.GetPixel(x, y) == initialColor)
            //while (IsSimilarColors(bmp.GetPixel(x, y), initialColor, tolerance))
            {
                bmp.SetPixel(x, y, replacementColor);
                //picMain.Refresh();

                // verify above row:
                if (y > 0)
                {
                    if (bmp.GetPixel(x, y - 1) != initialColor)
                    //if (!IsSimilarColors(bmp.GetPixel(x, y - 1), initialColor, tolerance))
                    {
                        aboveRowHasSeed = false;
                    }
                    if (!aboveRowHasSeed)
                    {
                        if (bmp.GetPixel(x, y - 1) == initialColor)
                        //if (!IsSimilarColors(bmp.GetPixel(x, y - 1), initialColor, tolerance))
                        {
                            seeds.Add(new Point(x, y - 1)); // add new seed
                            //picMain.Refresh();
                            aboveRowHasSeed = true;
                        }
                    }
                }

                // verify below row:
                if (y < bmp.Height - 1)
                {
                    if (bmp.GetPixel(x, y + 1) != initialColor)
                    //if (!IsSimilarColors(bmp.GetPixel(x, y + 1), initialColor, tolerance))
                    {
                        belowRowHasSeed = false;
                    }
                    if (!belowRowHasSeed)
                    {
                        if (bmp.GetPixel(x, y + 1) == initialColor)
                        //if (IsSimilarColors(bmp.GetPixel(x, y + 1), initialColor, tolerance))
                        {
                            seeds.Add(new Point(x, y + 1)); // add new seed
                            //picMain.Refresh();
                            belowRowHasSeed = true;
                        }
                    }
                }

                x++;

                if (x >= bmp.Width) // edge of the bitmap
                {
                    break;
                }
            }

            // toward left:
            x = point.X - 1;
            if (x >= 0)
            {
                while (bmp.GetPixel(x, y) == initialColor)
                //while (IsSimilarColors(bmp.GetPixel(x, y), initialColor, tolerance))
                {
                    bmp.SetPixel(x, y, replacementColor);
                    //picMain.Refresh();

                    // verify above row:
                    if (y > 0)
                    {
                        if (bmp.GetPixel(x, y - 1) != initialColor)
                        //if (!IsSimilarColors(bmp.GetPixel(x, y - 1), initialColor, tolerance))
                        {
                            aboveRowHasSeed = false;
                        }
                        if (!aboveRowHasSeed)
                        {
                            if (bmp.GetPixel(x, y - 1) == initialColor)
                            //if (IsSimilarColors(bmp.GetPixel(x, y - 1), initialColor, tolerance))
                            {
                                seeds.Add(new Point(x, y - 1)); // add new seed
                                //picMain.Refresh();
                                aboveRowHasSeed = true;
                            }
                        }
                    }

                    // verify below row:
                    if (y < bmp.Height - 1)
                    {
                        if (bmp.GetPixel(x, y + 1) != initialColor)
                        //if (!IsSimilarColors(bmp.GetPixel(x, y + 1), initialColor, tolerance))
                        {
                            belowRowHasSeed = false;
                        }
                        if (!belowRowHasSeed)
                        {
                            if (bmp.GetPixel(x, y + 1) == initialColor)
                            //if (IsSimilarColors(bmp.GetPixel(x, y + 1), initialColor, tolerance))
                            {
                                seeds.Add(new Point(x, y + 1)); // add new seed
                                //picMain.Refresh();
                                belowRowHasSeed = true;
                            }
                        }
                    }

                    x--;

                    if (x < 0)
                    {
                        break;
                    }
                }
            }
            seeds.Remove(point); // remove current seed
        }

        public bool IsSimilarColors(Color color1, Color color2, int tolerance)
        {
            if (tolerance > 0)
            {
                return (Math.Abs(color1.R - color2.R) <= tolerance &&
                   Math.Abs(color1.G - color2.G) <= tolerance &&
                   Math.Abs(color1.B - color2.B) <= tolerance &&
                   Math.Abs(color1.A - color2.A) <= tolerance);
            }
            else
            {
                return color1 == color2;
            }
        }

        private void ResizeShape(ResizeSide side, PointF point)
        {
            g.Clear(picMain.BackColor);
            bmpMain = new Bitmap(bmpBeforeShape);
            g = Graphics.FromImage(bmpMain);

            switch (side)
            {
                case ResizeSide.LOWERRIGHT:
                    {
                        if (point.X >= dashRectUpperLeftPoint.X + minSize && point.Y >= dashRectUpperLeftPoint.Y + minSize)
                        {
                            dashRectLowerRightPoint = point;
                        }
                        else if (point.X >= dashRectUpperLeftPoint.X + minSize && !(point.Y >= dashRectUpperLeftPoint.Y + minSize))
                        {
                            dashRectLowerRightPoint = new PointF(point.X, dashRectUpperLeftPoint.Y + minSize);
                        }
                        else if (!(point.X >= dashRectUpperLeftPoint.X + minSize) && point.Y >= dashRectUpperLeftPoint.Y + minSize)
                        {
                            dashRectLowerRightPoint = new PointF(dashRectUpperLeftPoint.X + minSize, point.Y);
                        }
                        else
                        {
                            dashRectLowerRightPoint = new PointF(dashRectUpperLeftPoint.X + minSize, dashRectUpperLeftPoint.Y + minSize);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Size = new Size((int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X), (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                        }
                        break;
                    }

                case ResizeSide.LOW:
                    {
                        if (point.Y >= dashRectUpperLeftPoint.Y + minSize) // point droped at lower right side of dashRectUpperLeftPoint
                        {
                            dashRectLowerRightPoint = new PointF(dashRectLowerRightPoint.X, point.Y);
                        }
                        else
                        {
                            dashRectLowerRightPoint = new PointF(dashRectLowerRightPoint.X, dashRectUpperLeftPoint.Y + minSize);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Height = (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y);
                        }
                        break;
                    }

                case ResizeSide.LOWERLEFT:
                    {
                        if (point.X <= dashRectLowerRightPoint.X - minSize && point.Y >= dashRectUpperLeftPoint.Y + minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(point.X, dashRectUpperLeftPoint.Y);
                            dashRectLowerRightPoint = new PointF(dashRectLowerRightPoint.X, point.Y);
                        }
                        else if (point.X <= dashRectLowerRightPoint.X - minSize && !(point.Y >= dashRectUpperLeftPoint.Y + minSize))
                        {
                            dashRectUpperLeftPoint = new PointF(point.X, dashRectUpperLeftPoint.Y);
                            dashRectLowerRightPoint = new PointF(dashRectLowerRightPoint.X, dashRectUpperLeftPoint.Y + minSize);
                        }
                        else if (!(point.X <= dashRectLowerRightPoint.X - minSize) && point.Y >= dashRectUpperLeftPoint.Y + minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectLowerRightPoint.X - minSize, dashRectUpperLeftPoint.Y);
                            dashRectLowerRightPoint = new PointF(dashRectLowerRightPoint.X, point.Y);
                        }
                        else
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectLowerRightPoint.X - minSize, dashRectUpperLeftPoint.Y);
                            dashRectLowerRightPoint = new PointF(dashRectLowerRightPoint.X, dashRectUpperLeftPoint.Y + minSize);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Location = new Point((int)dashRectUpperLeftPoint.X, (int)dashRectUpperLeftPoint.Y);
                            richTextBox1.Size = new Size((int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X), (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                        }
                        break;
                    }

                case ResizeSide.LEFT:
                    {
                        if (point.X <= dashRectLowerRightPoint.X - minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(point.X, dashRectUpperLeftPoint.Y);
                        }
                        else
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectLowerRightPoint.X - minSize, dashRectUpperLeftPoint.Y);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Location = new Point((int)dashRectUpperLeftPoint.X, (int)dashRectUpperLeftPoint.Y);
                            richTextBox1.Width = (int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X);
                        }
                        break;
                    }

                case ResizeSide.UPPERLEFT:
                    {
                        if (point.X <= dashRectLowerRightPoint.X - minSize && point.Y <= dashRectLowerRightPoint.Y - minSize)
                        {
                            dashRectUpperLeftPoint = point;
                        }
                        else if (point.X <= dashRectLowerRightPoint.X - minSize && !(point.Y <= dashRectLowerRightPoint.Y - minSize))
                        {
                            dashRectUpperLeftPoint = new PointF(point.X, dashRectLowerRightPoint.Y - minSize);
                        }
                        else if (!(point.X <= dashRectLowerRightPoint.X - minSize) && point.Y <= dashRectLowerRightPoint.Y - minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectLowerRightPoint.X - minSize, point.Y);
                        }
                        else
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectLowerRightPoint.X - minSize, dashRectLowerRightPoint.Y - minSize);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Location = new Point((int)dashRectUpperLeftPoint.X, (int)dashRectUpperLeftPoint.Y);
                            richTextBox1.Size = new Size((int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X), (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                        }
                        break;
                    }

                case ResizeSide.UP:
                    {
                        if (point.Y <= dashRectLowerRightPoint.Y - minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectUpperLeftPoint.X, point.Y);
                        }
                        else
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - minSize);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Location = new Point(richTextBox1.Location.X, (int)dashRectUpperLeftPoint.Y);
                            richTextBox1.Height = (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y);
                        }
                        break;
                    }

                case ResizeSide.UPPERRIGHT:
                    {
                        if (point.X >= dashRectUpperLeftPoint.X + minSize && point.Y <= dashRectLowerRightPoint.Y - minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectUpperLeftPoint.X, point.Y);
                            dashRectLowerRightPoint = new PointF(point.X, dashRectLowerRightPoint.Y);
                        }
                        else if (point.X >= dashRectUpperLeftPoint.X + minSize && !(point.Y <= dashRectLowerRightPoint.Y - minSize))
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - minSize);
                            dashRectLowerRightPoint = new PointF(point.X, dashRectLowerRightPoint.Y);
                        }
                        else if (!(point.X >= dashRectUpperLeftPoint.X + minSize) && point.Y <= dashRectLowerRightPoint.Y - minSize)
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectUpperLeftPoint.X, point.Y);
                            dashRectLowerRightPoint = new PointF(dashRectUpperLeftPoint.X + minSize, dashRectLowerRightPoint.Y);
                        }
                        else
                        {
                            dashRectUpperLeftPoint = new PointF(dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - minSize);
                            dashRectLowerRightPoint = new PointF(dashRectUpperLeftPoint.X + minSize, dashRectLowerRightPoint.Y);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Location = new Point(richTextBox1.Location.X, (int)dashRectUpperLeftPoint.Y);
                            richTextBox1.Size = new Size((int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X), (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                        }
                        break;
                    }

                case ResizeSide.RIGHT:
                    {
                        if (point.X >= dashRectUpperLeftPoint.X + minSize)
                        {
                            dashRectLowerRightPoint = new PointF(point.X, dashRectLowerRightPoint.Y);
                        }
                        else
                        {
                            dashRectLowerRightPoint = new PointF(dashRectUpperLeftPoint.X + minSize, dashRectLowerRightPoint.Y);
                        }

                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Width = (int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X);
                        }
                        break;
                    }
            }

            DrawSurroundedShape(surroundedShape);
        }

        private void MoveShape(PointF point)
        {
            g.Clear(picMain.BackColor);
            bmpMain = new Bitmap(bmpBeforeShape);
            g = Graphics.FromImage(bmpMain);

            dashRectUpperLeftPoint = new PointF(point.X - diffXFromUpperLeft, point.Y - diffYFromUpperLeft);
            dashRectLowerRightPoint = new PointF(point.X + diffXFromLowerRight, point.Y + diffYFromLowerRight);

            DrawSurroundedShape(surroundedShape);
        }

        private void DrawSurroundedShape(SurroundedShapes surroundedShape)
        {
            switch (surroundedShape)
            {
                case SurroundedShapes.OVAL:
                    draw.Ellipse(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.RECTANGLE:
                    draw.Rectangle(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.RIGHTTRIANGLE:
                    draw.RightTriangle(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.TRIANGLE:
                    draw.Triangle(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.DIAMOND:
                    draw.Diamond(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.PENTAGON:
                    draw.Pentagon(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.HEXAGON:
                    draw.Hexagon(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.RIGHTARROW:
                    draw.RightArrow(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.LEFTARROW:
                    draw.LeftArrow(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.UPARROW:
                    draw.UpArrow(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.DOWNARROW:
                    draw.DownArrow(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.FOURPOINTSTAR:
                    draw.FourPointStar(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.FIVEPOINTSTAR:
                    draw.FivePointStar(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.SIXPOINTSTAR:
                    draw.SixPointStar(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint);
                    break;

                case SurroundedShapes.TEXT:
                    {
                        if (isRichTextBoxDrawn)
                        {
                            richTextBox1.Location = new Point((int)dashRectUpperLeftPoint.X, (int)dashRectUpperLeftPoint.Y);
                        }

                        g.DrawString(text, textFont, textBrush, new RectangleF(dashRectUpperLeftPoint.X, dashRectUpperLeftPoint.Y, dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                        break;
                    }

                case SurroundedShapes.POLYGON:
                    {
                        newWidth = dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X;
                        newHeight = dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y;

                        PointF[] newPolygonPoints = new PointF[polygonPointsArray.Length];
                        for (int i = 0; i < polygonPointsArray.Length; i++)
                        {
                            newPolygonPoints[i] = new PointF(dashRectUpperLeftPoint.X + polygonPointsRatio[i].X * newWidth, dashRectUpperLeftPoint.Y + polygonPointsRatio[i].Y * newHeight);
                        }

                        if (chkFill.Checked)
                        {
                            solidBrush = new SolidBrush(backgroundColor);
                            g.FillPolygon(solidBrush, newPolygonPoints);
                        }
                        else
                        {
                            g.DrawLines(pen, newPolygonPoints);
                        }
                        if (chkOutline.Checked)
                        {
                            g.DrawLines(pen, newPolygonPoints);
                        }

                        break;
                    }

                case SurroundedShapes.CLOUDCALLOUT:
                    {
                        draw.Shape(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint, Shape.CLOUDCALLOUT);
                        break;
                    }

                case SurroundedShapes.ROUNDEDRECTCALLOUT:
                    {
                        draw.Shape(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint, Shape.ROUNDEDRECTCALLOUT);
                        break;
                    }

                case SurroundedShapes.OVALCALLOUT:
                    {
                        draw.Shape(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint, Shape.OVALCALLOUT);
                        break;
                    }

                case SurroundedShapes.HEART:
                    {
                        draw.Shape(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint, Shape.HEART);
                        break;
                    }

                case SurroundedShapes.LIGHTNING:
                    {
                        draw.Shape(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint, Shape.LIGHTNING);
                        break;
                    }

                case SurroundedShapes.BITMAP:
                    {
                        draw.Shape(pen, dashRectUpperLeftPoint, dashRectLowerRightPoint, Shape.BITMAP);
                        break;
                    }
            }
        }

        private void SetDashRectPoints(PointF pt1, PointF pt2)
        {
            if (pt2.X > pt1.X && pt2.Y > pt1.Y)
            {
                dashRectUpperLeftPoint = pt1;
                dashRectLowerRightPoint = pt2;
            }
            else if (pt2.X < pt1.X && pt2.Y < pt1.Y)
            {
                dashRectUpperLeftPoint = pt2;
                dashRectLowerRightPoint = pt1;
            }
            else if (pt2.X > pt1.X && pt2.Y < pt1.Y)
            {
                dashRectUpperLeftPoint = new PointF(pt1.X, pt2.Y);
                dashRectLowerRightPoint = new PointF(pt2.X, pt1.Y);
            }
            else if (pt2.X < pt1.X && pt2.Y > pt1.Y)
            {
                dashRectUpperLeftPoint = new PointF(pt2.X, pt1.Y);
                dashRectLowerRightPoint = new PointF(pt1.X, pt2.Y);
            }
        }

        private void SetPolygonPointsRatio()
        {
            polygonPointsRatio = new PointF[polygonPointsArray.Length];

            float oldWidth = dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X;
            float oldHeight = dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y;

            for (int i = 0; i < polygonPointsArray.Length; i++)
            {
                polygonPointsRatio[i].X = (polygonPointsArray[i].X - dashRectUpperLeftPoint.X) / oldWidth;
                polygonPointsRatio[i].Y = (polygonPointsArray[i].Y - dashRectUpperLeftPoint.Y) / oldHeight;
            }
        }


        private void btnFreeFromSelection_Click(object sender, EventArgs e)
        {
            ActivateButton(btnFreeFromSelection);
        }

        private void btnRectSelection_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRectSelection);
        }

        private void btnRectSelection_BackColorChanged(object sender, EventArgs e)
        {
            if (btnRectSelection.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldRectSelect = false;
            }
        }

        private void btnFreeFromSelection_BackColorChanged(object sender, EventArgs e)
        {
            if (btnFreeFromSelection.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else
            {
                shouldFreeSelect = false;
            }
        }

        private void SetFontStyleButtonsBackColor(FontStyle fontStyle)
        {
            if (fontStyle == FontStyle.Bold)
            {
                btnBold.BackColor = Color.Orange;
            }
            else if (fontStyle != FontStyle.Bold)
            {
                btnBold.BackColor = Color.Transparent;
            }

            if (fontStyle == FontStyle.Italic)
            {
                btnItalic.BackColor = Color.Orange;
            }
            else if (fontStyle != FontStyle.Italic)
            {
                btnItalic.BackColor = Color.Transparent;
            }

            if (fontStyle == FontStyle.Strikeout)
            {
                btnStrikeout.BackColor = Color.Orange;
            }
            else if (fontStyle != FontStyle.Strikeout)
            {
                btnStrikeout.BackColor = Color.Transparent;
            }

            if (fontStyle == FontStyle.Underline)
            {
                btnUnderline.BackColor = Color.Orange;
            }
            else if (fontStyle != FontStyle.Underline)
            {
                btnUnderline.BackColor = Color.Transparent;
            }

            if (fontStyle == (FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline))
            {
                btnUnderline.BackColor = Color.Orange;
                btnItalic.BackColor = Color.Orange;
                btnStrikeout.BackColor = Color.Orange;
                btnBold.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout))
            {
                btnBold.BackColor = Color.Orange;
                btnItalic.BackColor = Color.Orange;
                btnStrikeout.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Bold | FontStyle.Italic | FontStyle.Underline))
            {
                btnBold.BackColor = Color.Orange;
                btnItalic.BackColor = Color.Orange;
                btnUnderline.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Bold | FontStyle.Strikeout | FontStyle.Underline))
            {
                btnUnderline.BackColor = Color.Orange;
                btnStrikeout.BackColor = Color.Orange;
                btnBold.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline))
            {
                btnUnderline.BackColor = Color.Orange;
                btnItalic.BackColor = Color.Orange;
                btnStrikeout.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Bold | FontStyle.Italic))
            {
                btnBold.BackColor = Color.Orange;
                btnItalic.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Bold | FontStyle.Strikeout))
            {
                btnBold.BackColor = Color.Orange;
                btnStrikeout.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Bold | FontStyle.Underline))
            {
                btnBold.BackColor = Color.Orange;
                btnUnderline.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Italic | FontStyle.Strikeout))
            {
                btnItalic.BackColor = Color.Orange;
                btnStrikeout.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Italic | FontStyle.Underline))
            {
                btnItalic.BackColor = Color.Orange;
                btnUnderline.BackColor = Color.Orange;
            }
            else if (fontStyle == (FontStyle.Strikeout | FontStyle.Underline))
            {
                btnStrikeout.BackColor = Color.Orange;
                btnUnderline.BackColor = Color.Orange;
            }
        }


        private void grpFont_VisibleChanged(object sender, EventArgs e)
        {
            if (grpFont.Visible == false)
            {
                SetFontStyleButtonsBackColor(FontStyle.Regular);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isDashRectDrawn || isTinySquaresOnLineDrawn)
            {
                if (e.KeyData == Keys.Escape)
                {
                    // delete dash rect around shape:
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDashRect);
                    g = Graphics.FromImage(bmpMain);

                    picMain.Refresh();
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;

                    isDashRectDrawn = false;
                    btnCrop.Enabled = false;
                    //bmpInsideDashRect = null;
                    isTinySquaresOnLineDrawn = false;
                    picMain.Cursor = Cursors.Cross;

                    shouldPickColor = false;
                    shouldDrawPencil = false;
                    shouldErase = false;
                    shouldDrawLine = false;
                    shouldDrawOval = false;
                    shouldDrawRect = false;
                    shouldDrawRightTriangle = false;
                    shouldDrawTriangle = false;
                    shouldDrawPolygon = false;
                    shouldDrawDiamond = false;
                    shouldDrawPentagon = false;
                    shouldDrawHexagon = false;
                    shouldDrawRightArrow = false;
                    shouldDrawLeftArrow = false;
                    shouldDrawUpArrow = false;
                    shouldDrawDownArrow = false;
                    shouldDrawFourPointStar = false;
                    shouldDrawFivePointStar = false;
                    shouldDrawSixPointStar = false;
                    shouldDrawCurve = false;
                    shouldDrawCloudCallout = false;
                    shouldDrawRoundedRectCallout = false;
                    shouldDrawOvalCallout = false;
                    shouldDrawHeart = false;
                    shouldDrawLightning = false;
                    shouldDrawString = false;
                    shouldRectSelect = false;
                    shouldFreeSelect = false;
                }
                else if (e.KeyData == Keys.Delete)
                {
                    // delete shape and dash rect around it:
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeShape);
                    g = Graphics.FromImage(bmpMain);

                    picMain.Refresh();
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;

                    isDashRectDrawn = false;
                    btnCrop.Enabled = false;
                    //bmpInsideDashRect = null;
                    isTinySquaresOnLineDrawn = false;
                    picMain.Cursor = Cursors.Cross;

                    shouldPickColor = false;
                    shouldDrawPencil = false;
                    shouldErase = false;
                    shouldDrawLine = false;
                    shouldDrawOval = false;
                    shouldDrawRect = false;
                    shouldDrawRightTriangle = false;
                    shouldDrawTriangle = false;
                    shouldDrawPolygon = false;
                    shouldDrawDiamond = false;
                    shouldDrawPentagon = false;
                    shouldDrawHexagon = false;
                    shouldDrawRightArrow = false;
                    shouldDrawLeftArrow = false;
                    shouldDrawUpArrow = false;
                    shouldDrawDownArrow = false;
                    shouldDrawFourPointStar = false;
                    shouldDrawFivePointStar = false;
                    shouldDrawSixPointStar = false;
                    shouldDrawCurve = false;
                    shouldDrawCloudCallout = false;
                    shouldDrawRoundedRectCallout = false;
                    shouldDrawOvalCallout = false;
                    shouldDrawHeart = false;
                    shouldDrawLightning = false;
                    shouldDrawString = false;
                    shouldRectSelect = false;
                    shouldFreeSelect = false;
                }
            }
        }
        private void DrawTinySquaresAroundPicMain()
        {
            tinRight = new TinySquare(picMain.Right, picMain.Location.Y + picMain.Height / 2 - TinySquare.Size / 2);
            tinBottom = new TinySquare(picMain.Location.X + picMain.Width / 2 - TinySquare.Size, picMain.Bottom);
            tinBottomRight = new TinySquare(tinRight.Location.X, tinBottom.Location.Y);

            backgroundGraphics.FillRectangle(Brushes.White, tinRight.Location.X, tinRight.Location.Y, TinySquare.Size, TinySquare.Size);
            backgroundGraphics.DrawRectangle(Pens.Black, tinRight.Location.X, tinRight.Location.Y, TinySquare.Size, TinySquare.Size);
            backgroundGraphics.FillRectangle(Brushes.White, tinBottom.Location.X, tinBottom.Location.Y, TinySquare.Size, TinySquare.Size);
            backgroundGraphics.DrawRectangle(Pens.Black, tinBottom.Location.X, tinBottom.Location.Y, TinySquare.Size, TinySquare.Size);
            backgroundGraphics.FillRectangle(Brushes.White, tinBottomRight.Location.X, tinBottomRight.Location.Y, TinySquare.Size, TinySquare.Size);
            backgroundGraphics.DrawRectangle(Pens.Black, tinBottomRight.Location.X, tinBottomRight.Location.Y, TinySquare.Size, TinySquare.Size);
            pnlBackground.Refresh();
        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpBackgroundPanel, 0, 0);
        }

        private void pnlBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (tinRight.IsInside(e.Location, margin))
            {
                pnlBackground.Cursor = Cursors.SizeWE;
            }
            else if (tinBottom.IsInside(e.Location, margin))
            {
                pnlBackground.Cursor = Cursors.SizeNS;
            }
            else if (tinBottomRight.IsInside(e.Location, margin))
            {
                pnlBackground.Cursor = Cursors.SizeNWSE;
            }
            else
            {
                pnlBackground.Cursor = Cursors.Default;
            }

            if (shouldResizePicMainFromRight)
            {
                picMain.Size = new Size(e.Location.X - picMain.Location.X, picMain.Height);
            }
            else if (shouldResizePicMainFromBottom)
            {
                picMain.Size = new Size(picMain.Width, e.Location.Y);
            }
            else if (shouldResizePicMainFromBottomRight)
            {
                picMain.Size = new Size(e.Location.X - picMain.Location.X, e.Location.Y);
            }
        }

        private void pnlBackground_MouseDown(object sender, MouseEventArgs e)
        {
            if (tinRight.IsInside(e.Location, margin))
            {
                shouldResizePicMainFromRight = true;

                if (isDashRectDrawn)
                {
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDashRect);
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();

                    isDashRectDrawn = false;
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;
                }

                bmpBeforeResizePicMain = new Bitmap(picMain.Width, picMain.Height);
                picMain.DrawToBitmap(bmpBeforeResizePicMain, new Rectangle(0, 0, picMain.Width, picMain.Height));

                backgroundGraphics.Clear(pnlBackground.BackColor);
                pnlBackground.Refresh();
            }
            else if (tinBottom.IsInside(e.Location, margin))
            {
                shouldResizePicMainFromBottom = true;

                if (isDashRectDrawn)
                {
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDashRect);
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();

                    isDashRectDrawn = false;
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;
                }

                bmpBeforeResizePicMain = new Bitmap(picMain.Width, picMain.Height);
                picMain.DrawToBitmap(bmpBeforeResizePicMain, new Rectangle(0, 0, picMain.Width, picMain.Height));

                backgroundGraphics.Clear(pnlBackground.BackColor);
                pnlBackground.Refresh();
            }
            else if (tinBottomRight.IsInside(e.Location, margin))
            {
                shouldResizePicMainFromBottomRight = true;

                if (isDashRectDrawn)
                {
                    g.Clear(picMain.BackColor);
                    bmpMain = new Bitmap(bmpBeforeDashRect);
                    g = Graphics.FromImage(bmpMain);
                    picMain.Refresh();

                    isDashRectDrawn = false;
                    picMain.DrawToBitmap(bmpBeforeShape, new Rectangle(0, 0, picMain.Width, picMain.Height));
                    surroundedShape = SurroundedShapes.NONE;
                }

                bmpBeforeResizePicMain = new Bitmap(picMain.Width, picMain.Height);
                picMain.DrawToBitmap(bmpBeforeResizePicMain, new Rectangle(0, 0, picMain.Width, picMain.Height));

                backgroundGraphics.Clear(pnlBackground.BackColor);
                pnlBackground.Refresh();
            }
        }
        private void pnlBackground_MouseUp(object sender, MouseEventArgs e)
        {
            if (shouldResizePicMainFromRight || shouldResizePicMainFromBottom || shouldResizePicMainFromBottomRight)
            {
                bmpMain = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeDrawWithMouseMove = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeDashRect = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeShape = new Bitmap(picMain.Width, picMain.Height);

                g = Graphics.FromImage(bmpMain);
                g.DrawImage(bmpBeforeResizePicMain, new Rectangle(0, 0, bmpBeforeResizePicMain.Width, bmpBeforeResizePicMain.Height));
                picMain.Refresh();

                shouldResizePicMainFromRight = false;
                shouldResizePicMainFromBottom = false;
                shouldResizePicMainFromBottomRight = false;
                DrawTinySquaresAroundPicMain();
            }
        }

        private void btnCloudCallout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnCloudCallout);
        }

        private void btnCloudCallout_BackColorChanged(object sender, EventArgs e)
        {
            if (btnCloudCallout.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else if (btnCloudCallout.BackColor == Color.Transparent)
            {
                picMain.Cursor = Cursors.Default;
                shouldDrawCloudCallout = false;
            }
        }

        private void btnRoundedRectCallout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRoundedRectCallout);
        }

        private void btnRoundedRectCallout_BackColorChanged(object sender, EventArgs e)
        {
            if (btnRoundedRectCallout.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else if (btnRoundedRectCallout.BackColor == Color.Transparent)
            {
                picMain.Cursor = Cursors.Default;
                shouldDrawRoundedRectCallout = false;
            }
        }

        private void btnOvalCallout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnOvalCallout);
        }

        private void btnOvalCallout_BackColorChanged(object sender, EventArgs e)
        {
            if (btnOvalCallout.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else if (btnOvalCallout.BackColor == Color.Transparent)
            {
                picMain.Cursor = Cursors.Default;
                shouldDrawOvalCallout = false;
            }
        }

        private void btnHeart_Click(object sender, EventArgs e)
        {
            ActivateButton(btnHeart);
        }

        private void btnHeart_BackColorChanged(object sender, EventArgs e)
        {
            if (btnHeart.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else if (btnHeart.BackColor == Color.Transparent)
            {
                picMain.Cursor = Cursors.Default;
                shouldDrawHeart = false;
            }
        }

        private void btnLightning_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLightning);
        }

        private void btnLightning_BackColorChanged(object sender, EventArgs e)
        {
            if (btnLightning.BackColor == activeButtonColor)
            {
                picMain.Cursor = Cursors.Cross;
            }
            else if (btnLightning.BackColor == Color.Transparent)
            {
                picMain.Cursor = Cursors.Default;
                shouldDrawLightning = false;
            }
        }

        private void btnAirBrush_Click(object sender, EventArgs e)
        {
            ActivateButton(btnAirBrush);
        }

        private void btnAirBrush_BackColorChanged(object sender, EventArgs e)
        {
            if (btnAirBrush.BackColor == activeButtonColor)
            {
                picMain.Cursor = new Cursor(Properties.Resources.AirbrushCursor.Handle);
                lblAirbrushStrength.Visible = true;
                numAirbrushStrength.Visible = true;

                cmbSize.SelectedIndex = 4;
            }
            else
            {
                picMain.Cursor = Cursors.Default;
                shouldDrawAirbrush = false;

                lblAirbrushStrength.Visible = false;
                numAirbrushStrength.Visible = false;

                cmbSize.SelectedIndex = 0;
            }
        }

        private void SetRandomPixel(Bitmap bmp, Point eLocation, int size)
        {
            Point pntUL = new Point(eLocation.X - size / 2, eLocation.Y - size / 2); // upper left point
            Point pntLR = new Point(pntUL.X + size, pntUL.Y + size); // lower right point

            int randomX = random.Next(pntUL.X, pntUL.X + size + 1);
            int randomY = random.Next(pntUL.Y, pntUL.Y + size + 1);

            if (randomX >= bmp.Width || randomX < 0 || randomY >= bmp.Height || randomY < 0)
            {
                return;
            }

            bmp.SetPixel(randomX, randomY, foregroundColor);
        }

        private void numAirbrushStrength_ValueChanged(object sender, EventArgs e)
        {
            airbrushStrength = (int)numAirbrushStrength.Value;
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            picMain.Size = new Size((int)(dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X), (int)(dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
            bmpMain = new Bitmap(bmpInsideDashRect, picMain.Width, picMain.Height);
            bmpBeforeDrawWithMouseMove = new Bitmap(bmpInsideDashRect, picMain.Width, picMain.Height);
            bmpBeforeDashRect = new Bitmap(bmpInsideDashRect, picMain.Width, picMain.Height);
            bmpBeforeShape = new Bitmap(bmpInsideDashRect, picMain.Width, picMain.Height);

            g = Graphics.FromImage(bmpMain);
            g.DrawImage(bmpInsideDashRect, new Rectangle(0, 0, bmpInsideDashRect.Width, bmpInsideDashRect.Height));
            picMain.Refresh();

            backgroundGraphics.Clear(pnlBackground.BackColor);
            DrawTinySquaresAroundPicMain();
            btnCrop.Enabled = false;
        }

        private void btnFlipVertical_Click(object sender, EventArgs e)
        {
            if (isDashRectDrawn)
            {
                bmpInsideDashRect.RotateFlip(RotateFlipType.RotateNoneFlipX);

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeShape);
                g = Graphics.FromImage(bmpMain);

                g.DrawImage(bmpInsideDashRect, new RectangleF(dashRectUpperLeftPoint.X, dashRectUpperLeftPoint.Y, dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.RotateNoneFlipX);
                g.DrawImage(bmpMain, new RectangleF(0, 0, picMain.Width, picMain.Height));
            }

            picMain.Refresh();
        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (isDashRectDrawn)
            {
                bmpInsideDashRect.RotateFlip(RotateFlipType.RotateNoneFlipY);

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeShape);
                g = Graphics.FromImage(bmpMain);

                g.DrawImage(bmpInsideDashRect, new RectangleF(dashRectUpperLeftPoint.X, dashRectUpperLeftPoint.Y, dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.RotateNoneFlipY);
                g.DrawImage(bmpMain, new RectangleF(0, 0, picMain.Width, picMain.Height));
            }

            picMain.Refresh();
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            if (isDashRectDrawn)
            {
                bmpInsideDashRect.RotateFlip(RotateFlipType.Rotate270FlipNone);

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeShape);
                g = Graphics.FromImage(bmpMain);

                g.DrawImage(bmpInsideDashRect, new RectangleF(dashRectUpperLeftPoint.X, dashRectUpperLeftPoint.Y, dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.Rotate270FlipNone);

                picMain.Size = new Size(picMain.Height, picMain.Width);
                bmpMain = new Bitmap(bmpMain, picMain.Width, picMain.Height);
                bmpBeforeDrawWithMouseMove = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeDashRect = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeShape = new Bitmap(picMain.Width, picMain.Height);

                backgroundGraphics.Clear(pnlBackground.BackColor);
                //DrawTinySquaresAroundPicMain();

                g.DrawImage(bmpMain, new RectangleF(0, 0, picMain.Width, picMain.Height));
            }

            picMain.Refresh();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (isDashRectDrawn)
            {
                bmpInsideDashRect.RotateFlip(RotateFlipType.Rotate90FlipNone);

                g.Clear(picMain.BackColor);
                bmpMain = new Bitmap(bmpBeforeShape);
                g = Graphics.FromImage(bmpMain);

                g.DrawImage(bmpInsideDashRect, new RectangleF(dashRectUpperLeftPoint.X, dashRectUpperLeftPoint.Y, dashRectLowerRightPoint.X - dashRectUpperLeftPoint.X, dashRectLowerRightPoint.Y - dashRectUpperLeftPoint.Y));
                picMain.DrawToBitmap(bmpBeforeDashRect, new Rectangle(0, 0, picMain.Width, picMain.Height));
                draw.DashRectangle(dashRectUpperLeftPoint, dashRectLowerRightPoint);
            }
            else
            {
                bmpMain.RotateFlip(RotateFlipType.Rotate90FlipNone);

                picMain.Size = new Size(picMain.Height, picMain.Width);
                bmpMain = new Bitmap(bmpMain, picMain.Width, picMain.Height);
                bmpBeforeDrawWithMouseMove = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeDashRect = new Bitmap(picMain.Width, picMain.Height);
                bmpBeforeShape = new Bitmap(picMain.Width, picMain.Height);

                backgroundGraphics.Clear(pnlBackground.BackColor);
                //DrawTinySquaresAroundPicMain();

                g.DrawImage(bmpMain, new RectangleF(0, 0, picMain.Width, picMain.Height));
            }

            picMain.Refresh();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "New", MessageBoxButtons.OKCancel);
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                for (int y = 0; y < bmpMain.Height; y++)
                {
                    for (int x = 0; x < bmpMain.Width; x++)
                    {
                        bmpMain.SetPixel(x, y, Color.White);
                    }
                }

                picMain.Invalidate();
            }
        }

        private void btnDrawImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmpInsideDashRect = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                ActivateButton(btnDrawImage);
            }
        }

        private void btnDrawImage_BackColorChanged(object sender, EventArgs e)
        {
            if (btnDrawImage.BackColor == Color.Transparent)
            {
                shouldDrawImage = false;
            }
        }
    }
}
