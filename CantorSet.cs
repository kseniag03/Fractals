using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс реализации множества Кантора.
    /// </summary>
    class CantorSet : Fractal
    {
        /// <summary>
        /// Расстояние между отрезками.
        /// </summary>
        private static double s_verticalLength = 50;

        /// <summary>
        /// Расстояние между отрезками (значение от 10 до 100).
        /// </summary>
        private static double VerticalLength
        {
            set
            {
                if (10 <= value && value <= 100)
                {
                    s_verticalLength = value;
                }
            }
        }

        /// <summary>
        /// Устанавливает расстояние между отрезками.
        /// </summary>
        /// <param name="length"> Расстояние </param>
        public static void SetVerticalLength(uint length)
        {
            VerticalLength = length;
        }

        /// <summary>
        /// Рекурсивная отрисовка множества Кантора.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="left"> Смещение слева </param>
        /// <param name="top"> Смещение сверху </param>
        /// <param name="length"> Длина прямоугольника </param>
        /// <param name="depth"> Глубина </param>
        public override void Draw(Canvas canvas, double left, double top, double length, uint depth)
        {            
            Rectangle rectangle = new() {
                Width = length,
                Height = 20,
                StrokeThickness = 2,
                Stroke = Brushes.Snow,
                Fill = Brushes.Snow
            };

            if (depth >= 1)
            {
                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, left);
                Canvas.SetTop(rectangle, top);

                top += s_verticalLength;

                Draw(canvas, left + rectangle.Width * 2 / 3, top, length / 3, depth - 1);
                Draw(canvas, left, top, length / 3, depth - 1);
            }
        }
    }
}
