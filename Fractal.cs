using System.Windows;
using System.Windows.Controls;

namespace Fractals
{
    /// <summary>
    /// Базовый класс фрактала.
    /// </summary>
    abstract class Fractal
    {
        /// <summary>
        /// Глубина рекурсии.
        /// </summary>
        protected static uint s_depth = 1;

        /// <summary>
        /// Отрисовка фрактала с одной точкой и углом.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="point"> Точка начала </param>
        /// <param name="length"> Длина отрезка на конкретной итерации </param>
        /// <param name="angle"> Угол </param>
        /// <param name="depth"> Глубина </param>
        public virtual void Draw(Canvas canvas, Point point, double length, double angle, uint depth) { }

        /// <summary>
        /// Отрисовка фрактала с тремя точками.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="pointA"> Вершина треугольника </param>
        /// <param name="pointB"> Вершина треугольника </param>
        /// <param name="pointC"> Вершина треугольника </param>
        /// <param name="depth"> Глубина </param>
        public virtual void Draw(Canvas canvas, Point pointA, Point pointB, Point pointC, uint depth) { }

        /// <summary>
        /// Отрисовка фрактала с помощью прямоугольника.
        /// </summary>
        /// <param name="canvas"> Текущий холст </param>
        /// <param name="length"> Длина стороны </param>
        /// <param name="left"> Смещение слева </param>
        /// <param name="top"> Смещение сверху </param>
        /// <param name="depth"> Глубина </param>
        public virtual void Draw(Canvas canvas, double length, double left, double top, uint depth) { }
    }
}
