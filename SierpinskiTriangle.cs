using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс реализации треугольника Серпинского.
    /// </summary>
    class SierpinskiTriangle : Fractal
    {
        /// <summary>
        /// Рекурсивная отрисовка треугольника Серпинского.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="top"> Верхняя вершина треугольника </param>
        /// <param name="left"> Левая вершина треугольника </param>
        /// <param name="right"> Правая вершина треугольника</param>
        /// <param name="depth"> Глубина </param>
        public override void Draw(Canvas canvas, Point top, Point left, Point right, uint depth)
        {
            if (depth == 1)
            {
                canvas.Children.Add(new Polygon
                {
                    Points = new PointCollection() { top, left, right },
                    StrokeThickness = 1,
                    Stroke = Brushes.Snow
                });
            }
            else
            {
                Point leftMid = new((top.X + left.X) / 2, (top.Y + left.Y) / 2);
                Point rightMid = new((top.X + right.X) / 2, (top.Y + right.Y) / 2);
                Point topMid = new((left.X + right.X) / 2, (left.Y + right.Y) / 2);

                Draw(canvas, top, leftMid, rightMid, depth - 1);
                Draw(canvas, leftMid, left, topMid, depth - 1);
                Draw(canvas, rightMid, topMid, right, depth - 1);
            }
        }
    }
}
