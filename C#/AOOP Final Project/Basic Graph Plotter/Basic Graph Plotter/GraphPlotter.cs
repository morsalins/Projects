using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;

namespace Basic_Graph_Plotter
{
    public class GraphPlotter
    {
        public double OriginX { set; get; }
        public double OriginY { set; get; }
        public double UnitWidth { set; get; }
        public double UnitHeight { set; get; }
        public Canvas GraphCanvas { set; get; }

        public double LowerX { set; get; }
        public double UpperX { set; get; }
        public double LowerY { set; get; }
        public double UpperY { set; get; }

        private const double axisThickness = 2.0;
        private const double graphlineThickness = 1.0;
        private const double steps = 0.5;

        private Brush blackBrush = Brushes.Black;

        public GraphPlotter(Canvas canvas, double lowerX, double upperX, double lowerY, double upperY)
        {
            this.GraphCanvas = canvas;
            this.OriginX = GraphCanvas.Width / 2.0;
            this.OriginY = GraphCanvas.Height / 2.0;
            this.updateHorizontalRange(lowerX, upperX);
            this.updateVerticalRange(lowerY, upperY);
            this.UnitHeight = GraphCanvas.Height / (UpperY - LowerY);
            this.UnitWidth = GraphCanvas.Width / (UpperX - LowerX);
        }

        public void drawAxis() 
        {
            if (LowerX <= 0 && 0 <= UpperX)
            {
                drawVerticalLine();
            }
            if (LowerY <= 0 && 0 <= UpperY)
            {
                drawHorizontalLine();
            }
        }

        public void drawGraph(string expr, Brush graphColor)
        {
            List<Point> points = getPointsFromExpression(expr);
            drawGraph(points, graphColor);
        }

        public void drawGraph(List<Point> points)
        {
            drawGraph(points, blackBrush);
        }

        public void drawGraph(List<Point> points, Brush graphColor)
        {
            for (int i = 1; i < points.Count; i++)
            {
                drawLine(points[i - 1], points[i], graphlineThickness, graphColor);
            }
        }

        public void drawGraph(List<Point> points, int start, int end, Brush graphColor)
        {
            for (int i = start; i <= end; i++)
            {
                drawLine(points[i - 1], points[i], graphlineThickness, graphColor);
            }
        }

        private List<Point> getPointsFromExpression(string variableExpr)
        {
            List<Point> points = new List<Point>();
            string expr;

            for (double x = 0; x <= GraphCanvas.Width; x += steps)
            {
                double num = LowerX + (x / UnitWidth);
                expr = ParseExpression.replaceVariable(variableExpr, "(" + Convert.ToString(Math.Round(num, 3)) + ")");
            
                try
                {
                    double y = getFx(expr);
                    if (y < 0) y = 0;
                    if (y > GraphCanvas.Height) y = GraphCanvas.Height;
                    points.Add(new Point(x, y));
                    //Trace.WriteLine(num + " " + Convert.ToDouble(parser.Parse(expr)) + "\n");
                }
                catch (Exception e)
                {
                    if (e.Message == "Not Real Expression")
                    {
                        if (x < OriginX) x = OriginX;
                        else x = GraphCanvas.Width;
                    }
                    Trace.WriteLine(num + e.Message);
                }   
            }
            return points;
        }

        private double getFx(string expr)
        {
            //if (expr.StartsWith("log(-") || expr.StartsWith("log((-") || expr.StartsWith("sqrt(-") || expr.StartsWith("sqrt((-"))
            //{
            //    throw new ArithmeticException("Not Real Expression");
            //}
            return (UpperY - ParseExpression.parse(expr)) * UnitHeight;
        }

        private void drawLine(Point startPoint, Point endPoint)
        {
            drawLine(startPoint, endPoint, axisThickness, blackBrush);
        }

        private void drawLine(Point startPoint, Point endPoint, double thickness, Brush colorBrush)
        {
            Line line = new Line();
            line.StrokeThickness = thickness;
            line.Stroke = colorBrush;

            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;

            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;

            if ((line.Y1 == 0 && line.Y2 == 0) || (line.Y1 == GraphCanvas.Height && line.Y2 == GraphCanvas.Height))
            {
                return;
            }
            else
            {
                GraphCanvas.Children.Add(line);
            }
            return;
        }

        public void updateHorizontalRange(double lx, double ux)
        {
            if (ux > lx)
            {
                LowerX = lx;
                UpperX = ux;
                UnitWidth = GraphCanvas.Width / (UpperX - LowerX);
            }
            else
            {
                throw new Exception("Horizontal Range Invalid!");
            }
        }

        public void updateVerticalRange(double ly, double uy)
        {
            if (uy > ly)
            {
                LowerY = ly;
                UpperY = uy;
                UnitHeight = GraphCanvas.Height / (UpperY - LowerY);
            }
            else
            {
                throw new Exception("Vertical Range Invalid!");
            }
        }

        private void drawVerticalLine()
        {
            Point st = new Point(0 + ((0 - LowerX) * UnitWidth), 0);
            Point ed = new Point(0 + ((0 - LowerX) * UnitWidth), GraphCanvas.Height);
            drawLine(st, ed);
        }

        private void drawHorizontalLine()
        {
            Point st = new Point(0, 0 + ((UpperY - 0) * UnitHeight));
            Point ed = new Point(GraphCanvas.Width, 0 + ((UpperY - 0) * UnitHeight));
            drawLine(st, ed);
        }
    }
}
