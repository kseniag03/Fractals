using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс реализации ковра Серпинского.
    /// </summary>
    class SierpinskiCarpet : Fractal
    {
        /// <summary>
        /// Рекурсивная отрисовка ковра Серпинского.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="length"> Сторона квадрата </param>
        /// <param name="left"> Смещение слева </param>
        /// <param name="top"> Смещение сверху </param>
        /// <param name="depth"> Глубина </param>
        public override void Draw(Canvas canvas, double length, double left, double top, uint depth)
        {
            Rectangle rectangle = new() {
                Width = length,
                Height = length,
                StrokeThickness = 1,
                Stroke = Brushes.Snow
            };
            if (depth == 1)
            {
                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, left);
                Canvas.SetTop(rectangle, top);
            }
            else
            {
                double width = rectangle.Width / 3;

                Draw(canvas, width, left, top, depth - 1);
                Draw(canvas, width, left + width, top, depth - 1);
                Draw(canvas, width, left + 2 * width, top, depth - 1);

                Draw(canvas, width, left, top + width, depth - 1);
                Draw(canvas, width, left + 2 * width, top + width, depth - 1);

                Draw(canvas, width, left, top + 2 * width, depth - 1);
                Draw(canvas, width, left + width, top + 2 * width, depth - 1);
                Draw(canvas, width, left + 2 * width, top + 2 * width, depth - 1);
            }
        }
    }
}
