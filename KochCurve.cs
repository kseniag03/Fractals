using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс реализации кривой Коха.
    /// </summary>
    class KochCurve : Fractal
    {
        /// <summary>
        /// Рекурсивная отрисовка кривой Коха.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="pointA"> Вершина треугольника </param>
        /// <param name="pointB"> Вершина треугольника </param>
        /// <param name="pointC"> Вершина треугольника </param>
        /// <param name="depth"> Глубина </param>
        public override void Draw(Canvas canvas, Point pointA, Point pointB, Point pointC, uint depth)
        {
            if (depth > 0)
            {
                Point pointD = new((pointB.X + 2 * pointA.X) / 3, (pointB.Y + 2 * pointA.Y) / 3);
                Point pointE = new((2 * pointB.X + pointA.X) / 3, (pointA.Y + 2 * pointB.Y) / 3);

                Point pointP = new((pointB.X + pointA.X) / 2, (pointB.Y + pointA.Y) / 2);
                Point pointQ = new((4 * pointP.X - pointC.X) / 3, (4 * pointP.Y - pointC.Y) / 3);

                Line lineDQ = new() {
                    X1 = pointD.X, Y1 = pointD.Y,
                    X2 = pointQ.X, Y2 = pointQ.Y,
                    StrokeThickness = 1, Stroke = Brushes.Snow
                };
                Line lineEQ = new() {
                    X1 = pointE.X, Y1 = pointE.Y,
                    X2 = pointQ.X, Y2 = pointQ.Y,
                    StrokeThickness = 1, Stroke = Brushes.Snow
                };
                Line lineDE = new() {
                    X1 = pointD.X, Y1 = pointD.Y,
                    X2 = pointE.X, Y2 = pointE.Y,
                    StrokeThickness = 1, Stroke = Brushes.LightSlateGray
                };

                canvas.Children.Add(lineDQ);
                canvas.Children.Add(lineEQ);
                canvas.Children.Add(lineDE);

                Draw(canvas, pointD, pointQ, pointE, depth - 1);
                Draw(canvas, pointQ, pointE, pointD, depth - 1);
                Draw(canvas, pointA, pointD, new Point((2 * pointA.X + pointC.X) / 3, (2 * pointA.Y + pointC.Y) / 3), depth - 1);
                Draw(canvas, pointE, pointB, new Point((2 * pointB.X + pointC.X) / 3, (2 * pointB.Y + pointC.Y) / 3), depth - 1);
            }
        }
    }
}
