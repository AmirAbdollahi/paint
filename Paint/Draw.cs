using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class Draw
    {
        Form1 form1;
        SolidBrush solidBrush;
        RectangleF rect;
        PointF upperLeft;
        PointF lowerRight;
        Pen dashPen; // for drawing a dash patterned rectangle around shapes
        public const float tinySquareSize = 6;

        public TinySquare upperLeftSquare;
        public TinySquare upSquare;
        public TinySquare upperRightSquare;
        public TinySquare rightSquare;
        public TinySquare lowerRightSquare;
        public TinySquare lowSquare;
        public TinySquare lowerLeftSquare;
        public TinySquare leftSquare;

        Bitmap bmpColoredCloudCallout;
        Bitmap bmpColoredRoundedRectCallout;
        Bitmap bmpColoredOvalCallout;
        Bitmap bmpColoredHeart;
        Bitmap bmpColoredLightning;

        enum TinySquareLocation { UPPERLEFT, UP, UPPERRIGHT, RIGHT, LOWERRIGHT, LOW, LOWERLEFT, LEFT };

        public Draw()
        {
            dashPen = new Pen(Color.Blue, 1);
            dashPen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
        }
        public void GetForm1(Form1 f1)
        {
            form1 = f1;
        }
        public void Line(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            form1.g.DrawLine(inputPen, inputPoint1, inputPoint2);
        }
        public void Ellipse(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillEllipse(solidBrush, upperLeft.X, upperLeft.Y, rect.Width, rect.Height);
            }
            else
            {
                form1.g.DrawEllipse(inputPen, upperLeft.X, upperLeft.Y, rect.Width, rect.Height);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawEllipse(inputPen, upperLeft.X, upperLeft.Y, rect.Width, rect.Height);
            }
        }
        public void Rectangle(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillRectangle(solidBrush, upperLeft.X, upperLeft.Y, rect.Width, rect.Height);
            }
            else
            {
                form1.g.DrawRectangle(inputPen, upperLeft.X, upperLeft.Y, rect.Width, rect.Height);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawRectangle(inputPen, upperLeft.X, upperLeft.Y, rect.Width, rect.Height);
            }
        }
        public void DashRectangle(PointF inputPoint1, PointF inputPoint2)
        {
            RectangleF dashRect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = dashRect.Location;
            lowerRight = new PointF(dashRect.Right, dashRect.Bottom);

            form1.g.DrawRectangle(dashPen, upperLeft.X, upperLeft.Y, dashRect.Width, dashRect.Height);

            DrawTinySquare(TinySquareLocation.UPPERLEFT, dashRect);
            DrawTinySquare(TinySquareLocation.UP, dashRect);
            DrawTinySquare(TinySquareLocation.UPPERRIGHT, dashRect);
            DrawTinySquare(TinySquareLocation.RIGHT, dashRect);
            DrawTinySquare(TinySquareLocation.LOWERRIGHT, dashRect);
            DrawTinySquare(TinySquareLocation.LOW, dashRect);
            DrawTinySquare(TinySquareLocation.LOWERLEFT, dashRect);
            DrawTinySquare(TinySquareLocation.LEFT, dashRect);
        }
        private void DrawTinySquare(TinySquareLocation loc, RectangleF dashR)
        {
            switch (loc)
            {
                case TinySquareLocation.UPPERLEFT:
                    upperLeftSquare = new TinySquare(dashR.Left - tinySquareSize / 2, dashR.Top - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, upperLeftSquare.Location.X, upperLeftSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.UP:
                    upSquare = new TinySquare(dashR.Left + dashR.Width / 2 - tinySquareSize / 2, dashR.Top - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, upSquare.Location.X, upSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.UPPERRIGHT:
                    upperRightSquare = new TinySquare(dashR.Right - tinySquareSize / 2, dashR.Top - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, upperRightSquare.Location.X, upperRightSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.RIGHT:
                    rightSquare = new TinySquare(dashR.Right - tinySquareSize / 2, dashR.Top + dashR.Height / 2 - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, rightSquare.Location.X, rightSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.LOWERRIGHT:
                    lowerRightSquare = new TinySquare(dashR.Right - tinySquareSize / 2, dashR.Bottom - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, lowerRightSquare.Location.X, lowerRightSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.LOW:
                    lowSquare = new TinySquare(dashR.Left + dashR.Width / 2 - tinySquareSize / 2, dashR.Bottom - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, lowSquare.Location.X, lowSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.LOWERLEFT:
                    lowerLeftSquare = new TinySquare(dashR.Left - tinySquareSize / 2, dashR.Bottom - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, lowerLeftSquare.Location.X, lowerLeftSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;

                case TinySquareLocation.LEFT:
                    leftSquare = new TinySquare(dashR.Left - tinySquareSize / 2, dashR.Top + dashR.Height / 2 - tinySquareSize / 2);
                    form1.g.DrawRectangle(Pens.Black, leftSquare.Location.X, leftSquare.Location.Y, tinySquareSize, tinySquareSize);
                    break;
            }
        }

        public void RightTriangle(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] rightTrianglePoints = new PointF[4];
            rightTrianglePoints[0] = upperLeft;
            rightTrianglePoints[1] = lowerRight;
            rightTrianglePoints[2] = new PointF(upperLeft.X, lowerRight.Y);
            rightTrianglePoints[3] = rightTrianglePoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, rightTrianglePoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, rightTrianglePoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, rightTrianglePoints);
            }
        }
        public void Triangle(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] trianglePoints = new PointF[4];
            trianglePoints[0] = new PointF(upperLeft.X + rect.Width * 0.5f, upperLeft.Y);
            trianglePoints[1] = lowerRight;
            trianglePoints[2] = new PointF(upperLeft.X, lowerRight.Y);
            trianglePoints[3] = trianglePoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, trianglePoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, trianglePoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, trianglePoints);
            }
        }
        public void Diamond(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] diamondPoints = new PointF[5];
            diamondPoints[0] = new PointF(upperLeft.X + rect.Width * 0.5f, upperLeft.Y);
            diamondPoints[1] = new PointF(rect.Right, rect.Top + rect.Height * 0.5f);
            diamondPoints[2] = new PointF(diamondPoints[0].X, rect.Bottom);
            diamondPoints[3] = new PointF(rect.Left, diamondPoints[1].Y);
            diamondPoints[4] = diamondPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, diamondPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, diamondPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, diamondPoints);
            }
        }
        public void Pentagon(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] pentagonPoints = new PointF[6];
            pentagonPoints[0] = new PointF(upperLeft.X + rect.Width * 0.5f, rect.Top);
            pentagonPoints[1] = new PointF(rect.Right, rect.Top + rect.Height * 0.38f);
            pentagonPoints[2] = new PointF(rect.Left + rect.Width * 0.81f, rect.Bottom);
            pentagonPoints[3] = new PointF(rect.Left + rect.Width * 0.19f, rect.Bottom);
            pentagonPoints[4] = new PointF(rect.Left, pentagonPoints[1].Y);
            pentagonPoints[5] = pentagonPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, pentagonPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, pentagonPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, pentagonPoints);
            }
        }
        public void Hexagon(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] hexagonPoints = new PointF[7];
            hexagonPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            hexagonPoints[1] = new PointF(rect.Right, rect.Top + rect.Height * 0.25f);
            hexagonPoints[2] = new PointF(hexagonPoints[1].X, rect.Top + rect.Height * 0.75f);
            hexagonPoints[3] = new PointF(hexagonPoints[0].X, rect.Bottom);
            hexagonPoints[4] = new PointF(rect.Left, hexagonPoints[2].Y);
            hexagonPoints[5] = new PointF(rect.Left, hexagonPoints[1].Y);
            hexagonPoints[6] = hexagonPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, hexagonPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, hexagonPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, hexagonPoints);
            }
        }
        public void RightArrow(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] rightArrowPoints = new PointF[8];
            rightArrowPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            rightArrowPoints[1] = new PointF(rect.Right, rect.Top + rect.Height * 0.5f);
            rightArrowPoints[2] = new PointF(rightArrowPoints[0].X, rect.Bottom);
            rightArrowPoints[3] = new PointF(rightArrowPoints[0].X, rect.Top + rect.Height * 0.75f);
            rightArrowPoints[4] = new PointF(rect.Left, rightArrowPoints[3].Y);
            rightArrowPoints[5] = new PointF(rect.Left, rect.Top + rect.Height * 0.25f);
            rightArrowPoints[6] = new PointF(rightArrowPoints[0].X, rightArrowPoints[5].Y);
            rightArrowPoints[7] = rightArrowPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, rightArrowPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, rightArrowPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, rightArrowPoints);
            }
        }
        public void LeftArrow(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] leftArrowPoints = new PointF[8];
            leftArrowPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            leftArrowPoints[1] = new PointF(rect.Left, rect.Top + rect.Height * 0.5f);
            leftArrowPoints[2] = new PointF(leftArrowPoints[0].X, rect.Bottom);
            leftArrowPoints[3] = new PointF(leftArrowPoints[0].X, rect.Top + rect.Height * 0.75f);
            leftArrowPoints[4] = new PointF(rect.Right, leftArrowPoints[3].Y);
            leftArrowPoints[5] = new PointF(rect.Right, rect.Top + rect.Height * 0.25f);
            leftArrowPoints[6] = new PointF(leftArrowPoints[0].X, leftArrowPoints[5].Y);
            leftArrowPoints[7] = leftArrowPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, leftArrowPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, leftArrowPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, leftArrowPoints);
            }
        }
        public void UpArrow(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] upArrowPoints = new PointF[8];
            upArrowPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            upArrowPoints[1] = new PointF(rect.Right, rect.Top + rect.Height * 0.5f);
            upArrowPoints[2] = new PointF(rect.Left + rect.Width * 0.75f, upArrowPoints[1].Y);
            upArrowPoints[3] = new PointF(upArrowPoints[2].X, rect.Bottom);
            upArrowPoints[4] = new PointF(rect.Left + rect.Width * 0.25f, rect.Bottom);
            upArrowPoints[5] = new PointF(upArrowPoints[4].X, upArrowPoints[1].Y);
            upArrowPoints[6] = new PointF(rect.Left, upArrowPoints[5].Y);
            upArrowPoints[7] = upArrowPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, upArrowPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, upArrowPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, upArrowPoints);
            }
        }
        public void DownArrow(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] downArrowPoints = new PointF[8];
            downArrowPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Bottom);
            downArrowPoints[1] = new PointF(rect.Right, rect.Top + rect.Height * 0.5f);
            downArrowPoints[2] = new PointF(rect.Left + rect.Width * 0.75f, downArrowPoints[1].Y);
            downArrowPoints[3] = new PointF(downArrowPoints[2].X, rect.Top);
            downArrowPoints[4] = new PointF(rect.Left + rect.Width * 0.25f, rect.Top);
            downArrowPoints[5] = new PointF(downArrowPoints[4].X, downArrowPoints[1].Y);
            downArrowPoints[6] = new PointF(rect.Left, downArrowPoints[5].Y);
            downArrowPoints[7] = downArrowPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, downArrowPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, downArrowPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, downArrowPoints);
            }
        }
        public void FourPointStar(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] fourPointStarPoints = new PointF[9];
            fourPointStarPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            fourPointStarPoints[1] = new PointF(rect.Left + rect.Width * 0.62f, rect.Top + rect.Height * 0.38f);
            fourPointStarPoints[2] = new PointF(rect.Right, rect.Top + rect.Height * 0.5f);
            fourPointStarPoints[3] = new PointF(fourPointStarPoints[1].X, rect.Top + rect.Height * 0.62f);
            fourPointStarPoints[4] = new PointF(fourPointStarPoints[0].X, rect.Bottom);
            fourPointStarPoints[5] = new PointF(rect.Left + rect.Width * 0.38f, fourPointStarPoints[3].Y);
            fourPointStarPoints[6] = new PointF(rect.Left, fourPointStarPoints[2].Y);
            fourPointStarPoints[7] = new PointF(fourPointStarPoints[5].X, fourPointStarPoints[1].Y);
            fourPointStarPoints[8] = fourPointStarPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, fourPointStarPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, fourPointStarPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, fourPointStarPoints);
            }
        }
        public void FivePointStar(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] fivePointStarPoints = new PointF[11];
            fivePointStarPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            fivePointStarPoints[1] = new PointF(rect.Left + rect.Width * 0.62f, rect.Top + rect.Height * 0.38f);
            fivePointStarPoints[2] = new PointF(rect.Right, rect.Top + rect.Height * 0.39f);
            fivePointStarPoints[3] = new PointF(rect.Left + rect.Width * 0.7f, rect.Top + rect.Height * 0.62f);
            fivePointStarPoints[4] = new PointF(rect.Left + rect.Width * 0.81f, rect.Bottom);
            fivePointStarPoints[5] = new PointF(fivePointStarPoints[0].X, rect.Top + rect.Height * 0.78f);
            fivePointStarPoints[6] = new PointF(rect.Left + rect.Width * 0.19f, rect.Bottom);
            fivePointStarPoints[7] = new PointF(rect.Left + rect.Width * 0.30f, fivePointStarPoints[3].Y);
            fivePointStarPoints[8] = new PointF(rect.Left, fivePointStarPoints[2].Y);
            fivePointStarPoints[9] = new PointF(rect.Left + rect.Width * 0.38f, fivePointStarPoints[1].Y);
            fivePointStarPoints[10] = fivePointStarPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, fivePointStarPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, fivePointStarPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, fivePointStarPoints);
            }
        }
        public void SixPointStar(Pen inputPen, PointF inputPoint1, PointF inputPoint2)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            PointF[] sixPointStarPoints = new PointF[13];
            sixPointStarPoints[0] = new PointF(rect.Left + rect.Width * 0.5f, rect.Top);
            sixPointStarPoints[1] = new PointF(rect.Left + rect.Width * 0.68f, rect.Top + rect.Height * 0.25f);
            sixPointStarPoints[2] = new PointF(rect.Right, sixPointStarPoints[1].Y);
            sixPointStarPoints[3] = new PointF(rect.Left + rect.Width * 0.84f, rect.Top + rect.Height * 0.5f);
            sixPointStarPoints[4] = new PointF(rect.Right, rect.Top + rect.Height * 0.75f);
            sixPointStarPoints[5] = new PointF(sixPointStarPoints[1].X, sixPointStarPoints[4].Y);
            sixPointStarPoints[6] = new PointF(sixPointStarPoints[0].X, rect.Bottom);
            sixPointStarPoints[7] = new PointF(rect.Left + rect.Width * 0.32f, sixPointStarPoints[4].Y);
            sixPointStarPoints[8] = new PointF(rect.Left, sixPointStarPoints[4].Y);
            sixPointStarPoints[9] = new PointF(rect.Left + rect.Width * 0.16f, sixPointStarPoints[3].Y);
            sixPointStarPoints[10] = new PointF(rect.Left, sixPointStarPoints[1].Y);
            sixPointStarPoints[11] = new PointF(sixPointStarPoints[7].X, sixPointStarPoints[1].Y);
            sixPointStarPoints[12] = sixPointStarPoints[0];

            if (form1.chkFill.Checked)
            {
                solidBrush = new SolidBrush(form1.backgroundColor);
                form1.g.FillPolygon(solidBrush, sixPointStarPoints);
            }
            else
            {
                form1.g.DrawLines(inputPen, sixPointStarPoints);
            }

            if (form1.chkOutline.Checked)
            {
                form1.g.DrawLines(inputPen, sixPointStarPoints);
            }
        }
        public void Shape(Pen inputPen, PointF inputPoint1, PointF inputPoint2, Form1.Shape shape)
        {
            rect = GetRectangle(inputPoint1, inputPoint2);
            upperLeft = rect.Location;
            lowerRight = new PointF(rect.Right, rect.Bottom);

            switch (shape)
            {
                case Form1.Shape.CLOUDCALLOUT:
                    {
                        if (inputPen.Color == Color.Black)
                        {
                            form1.g.DrawImage(form1.bmpCloudCallout, rect);
                        }
                        else
                        {
                            bmpColoredCloudCallout = new Bitmap(form1.bmpCloudCallout.Width, form1.bmpCloudCallout.Height);

                            for (int y = 0; y < bmpColoredCloudCallout.Height; y++)
                            {
                                for (int x = 0; x < bmpColoredCloudCallout.Width; x++)
                                {
                                    if (form1.IsSimilarColors(form1.clrArrayCloudCallout[x, y], Color.Black, 10))
                                    {
                                        bmpColoredCloudCallout.SetPixel(x, y, inputPen.Color);
                                    }
                                }
                            }
                            form1.g.DrawImage(bmpColoredCloudCallout, rect);
                        }
                        break;
                    }

                case Form1.Shape.ROUNDEDRECTCALLOUT:
                    {
                        if (inputPen.Color == Color.Black)
                        {
                            form1.g.DrawImage(form1.bmpRoundedRectCallout, rect);
                        }
                        else
                        {
                            bmpColoredRoundedRectCallout = new Bitmap(form1.bmpRoundedRectCallout.Width, form1.bmpRoundedRectCallout.Height);

                            for (int y = 0; y < bmpColoredRoundedRectCallout.Height; y++)
                            {
                                for (int x = 0; x < bmpColoredRoundedRectCallout.Width; x++)
                                {
                                    if (form1.IsSimilarColors(form1.clrArrayRoundedRectCallout[x, y], Color.Black, 10))
                                    {
                                        bmpColoredRoundedRectCallout.SetPixel(x, y, inputPen.Color);
                                    }
                                }
                            }
                            form1.g.DrawImage(bmpColoredRoundedRectCallout, rect);
                        }
                        break;
                    }

                case Form1.Shape.OVALCALLOUT:
                    {
                        if (inputPen.Color == Color.Black)
                        {
                            form1.g.DrawImage(form1.bmpOvalCallout, rect);
                        }
                        else
                        {
                            bmpColoredOvalCallout = new Bitmap(form1.bmpOvalCallout.Width, form1.bmpOvalCallout.Height);

                            for (int y = 0; y < bmpColoredOvalCallout.Height; y++)
                            {
                                for (int x = 0; x < bmpColoredOvalCallout.Width; x++)
                                {
                                    if (form1.IsSimilarColors(form1.clrArrayOvalCallout[x, y], Color.Black, 10))
                                    {
                                        bmpColoredOvalCallout.SetPixel(x, y, inputPen.Color);
                                    }
                                }
                            }
                            form1.g.DrawImage(bmpColoredOvalCallout, rect);
                        }
                        break;
                    }

                case Form1.Shape.HEART:
                    {
                        if (inputPen.Color == Color.Black)
                        {
                            form1.g.DrawImage(form1.bmpHeart, rect);
                        }
                        else
                        {
                            bmpColoredHeart = new Bitmap(form1.bmpHeart.Width, form1.bmpHeart.Height);

                            for (int y = 0; y < bmpColoredHeart.Height; y++)
                            {
                                for (int x = 0; x < bmpColoredHeart.Width; x++)
                                {
                                    if (form1.IsSimilarColors(form1.clrArrayHeart[x, y], Color.Black, 10))
                                    {
                                        bmpColoredHeart.SetPixel(x, y, inputPen.Color);
                                    }
                                }
                            }
                            form1.g.DrawImage(bmpColoredHeart, rect);
                        }
                        break;
                    }

                case Form1.Shape.LIGHTNING:
                    {
                        if (inputPen.Color == Color.Black)
                        {
                            form1.g.DrawImage(form1.bmpLightning, rect);
                        }
                        else
                        {
                            bmpColoredLightning = new Bitmap(form1.bmpLightning.Width, form1.bmpLightning.Height);

                            for (int y = 0; y < bmpColoredLightning.Height; y++)
                            {
                                for (int x = 0; x < bmpColoredLightning.Width; x++)
                                {
                                    if (form1.IsSimilarColors(form1.clrArrayLightning[x, y], Color.Black, 10))
                                    {
                                        bmpColoredLightning.SetPixel(x, y, inputPen.Color);
                                    }
                                }
                            }
                            form1.g.DrawImage(bmpColoredLightning, rect);
                        }
                        break;
                    }

                //case Form1.Shape.IMAGE:
                //    {
                //        form1.g.DrawImage(form1.bmpExternalImage, rect);
                //        break;
                //    }

                case Form1.Shape.BITMAP:
                    {
                        form1.g.DrawImage(form1.bmpInsideDashRect, rect);
                        break;
                    }
            }

        }
        private RectangleF GetRectangle(PointF pt1, PointF pt2)
        {
            PointF upperLeftPoint;
            PointF lowerRightPoint;
            float width;
            float height;

            if (pt2.X > pt1.X && pt2.Y > pt1.Y) // toward bottom right
            {
                upperLeftPoint = pt1;
                lowerRightPoint = pt2;
            }
            else if (pt2.X < pt1.X && pt2.Y < pt1.Y) // toward up left
            {
                upperLeftPoint = pt2;
                lowerRightPoint = pt1;
            }
            else if (pt2.X > pt1.X && pt2.Y < pt1.Y) // toward up right
            {
                upperLeftPoint = new PointF(pt1.X, pt2.Y);
                lowerRightPoint = new PointF(pt2.X, pt1.Y);
            }
            else if (pt2.X < pt1.X && pt2.Y > pt1.Y) // toward bottom left
            {
                upperLeftPoint = new PointF(pt2.X, pt1.Y);
                lowerRightPoint = new PointF(pt1.X, pt2.Y);
            }
            else
            {
                upperLeftPoint = PointF.Empty;
                lowerRightPoint = PointF.Empty;
            }

            width = lowerRightPoint.X - upperLeftPoint.X;
            height = lowerRightPoint.Y - upperLeftPoint.Y;

            return new RectangleF(upperLeftPoint.X, upperLeftPoint.Y, width, height);
        }
    }
}
