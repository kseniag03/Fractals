using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс реализации фрактального дерева.
    /// </summary>
    class FractalTree : Fractal
    {
        /// <summary>
        /// Поля: угол для наклона первого и второго отрезков, и коэффициент, 
        /// определяющий отношение длин отрезков на текущей и последующей итерации.
        /// </summary>
        private static double s_leftAngle = 45 * Math.PI / 180;
        private static double s_rightAngle = 45 * Math.PI / 180;
        private static double s_branchesLength = 0.5;

        /// <summary>
        /// Угол наклона первого отрезка (левый угол), в радианах.
        /// </summary>
        private static double LeftAngle
        {
            set
            {
                if (0 <= value && value <= 180)
                {
                    s_leftAngle = value * Math.PI / 180;
                }
            }
        }

        /// <summary>
        /// Угол наклона второго отрезка (правый угол), в радианах.
        /// </summary>
        private static double RightAngle
        {
            set
            {
                if (0 <= value && value <= 180)
                {
                    s_rightAngle = value * Math.PI / 180;
                }
            }
        }

        /// <summary>
        /// Коэффициент отношения длин отрезков (значение от 0.1 до 1 с шагом 0.1).
        /// </summary>
        private static double BranchesLength
        {
            set
            {
                if (1 <= value && value <= 10)
                {
                    s_branchesLength = value * 0.1;
                }
            }
        }

        /// <summary>
        /// Устанавливает угол наклона первого отрезка.
        /// </summary>
        /// <param name="angle"> Угол (в градусах) </param>
        public static void SetLeftAngle(uint angle)
        {
            LeftAngle = angle;
        }

        /// <summary>
        /// Устанавливает угол наклона второго отрезка.
        /// </summary>
        /// <param name="angle"> Угол (в градусах) </param>
        public static void SetRightAngle(uint angle)
        {
            RightAngle = angle;
        }

        /// <summary>
        /// Устанавливает коэффициент отношения длин отрезков.
        /// </summary>
        /// <param name="length"> Кол-во долей 0.1 </param>
        public static void SetBranchesLength(uint length)
        {
            BranchesLength = length;
        }

        /// <summary>
        /// Рекурсивная отрисовка фрактального дерева.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="point"> Точка начала отрезка </param>
        /// <param name="length"> Длина отрезка на итерации </param>
        /// <param name="angle"> Угол </param>
        /// <param name="depth"> Глубина </param>
        public override void Draw(Canvas canvas, Point point, double length, double angle, uint depth)
        {
            double x = point.X + length * Math.Sin(angle);
            double y = point.Y + length * Math.Cos(angle);

            Line line = new() {
                X1 = point.X, Y1 = canvas.ActualHeight - point.Y,
                X2 = x, Y2 = canvas.ActualHeight - y,
                StrokeThickness = 2, Stroke = Brushes.Snow
            };

            canvas.Children.Add(line);

            if (depth > 1)
            {
                Draw(canvas, new Point(x, y), length * s_branchesLength, angle + s_leftAngle, depth - 1);
                Draw(canvas, new Point(x, y), length * s_branchesLength, angle - s_rightAngle, depth - 1);
            }
        }
    }
}
