using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fractals
{
    /// <summary>
    /// Класс реализации приложения.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Объект родительского класса.
        /// </summary>
        private Fractal fractal;

        /// <summary>
        /// Глубина фракталов (разный диапазон).
        /// </summary>
        private uint treeDepth = 1;
        private uint curveDepth = 1;
        private uint carpetDepth = 1;
        private uint triangleDepth = 1;
        private uint setDepth = 1;

        /// <summary>
        /// Словарь с холстами (используется для сохранения).
        /// </summary>
        private static Dictionary<string, Canvas> s_canvases;

        /// <summary>
        /// Запускает приложение.
        /// </summary>
        public MainWindow()
        {
            Height = 500;
            Width = 800;
            MinHeight = Height / 2;
            MinWidth = Width / 2;

            s_canvases = new();

            InitializeComponent();
        }

        /// <summary>
        /// Рисует фрактальное дерево.
        /// </summary>
        private void DrawTree()
        {
            CanvasTree.Children.Clear();
            fractal = new FractalTree();

            fractal.Draw(CanvasTree, new Point(CanvasTree.ActualWidth / 2, 0), CanvasTree.ActualHeight / 4, 0, treeDepth);
        }

        /// <summary>
        /// Рисует кривую Коха.
        /// </summary>
        private void DrawCurve()
        {
            CanvasCurve.Children.Clear();
            fractal = new KochCurve();

            fractal.Draw(CanvasCurve,
                new Point(600 - CanvasCurve.ActualWidth, 800 - CanvasCurve.ActualHeight), 
                new Point(1800 - CanvasCurve.ActualWidth, 800 - CanvasCurve.ActualHeight), 
                new Point(1200 - CanvasCurve.ActualWidth, 1500 - CanvasCurve.ActualHeight),
                curveDepth);
        }

        /// <summary>
        /// Рисует ковёр Серпинского.
        /// </summary>
        private void DrawCarpet()
        {
            CanvasCarpet.Children.Clear();
            fractal = new SierpinskiCarpet();

            fractal.Draw(CanvasCarpet, CanvasCarpet.ActualHeight, CanvasCarpet.ActualWidth / 5, 0, carpetDepth);
            
        }

        /// <summary>
        /// Рисует треугольник Серпинского.
        /// </summary>
        private void DrawTriangle()
        {
            CanvasTriangle.Children.Clear();
            fractal = new SierpinskiTriangle();

            fractal.Draw(CanvasTriangle, new Point(CanvasTriangle.ActualWidth / 2, 0), 
                new Point(0, CanvasTriangle.ActualHeight), new Point(CanvasTriangle.ActualWidth, CanvasTriangle.ActualHeight),
                triangleDepth);
        }

        /// <summary>
        /// Рисует множество Кантора.
        /// </summary>
        private void DrawSet()
        {
            CanvasSet.Children.Clear();
            fractal = new CantorSet();

            fractal.Draw(CanvasSet, CanvasSet.ActualWidth / 4, 10, CanvasSet.ActualWidth / 2, setDepth);
        }

        /// <summary>
        /// Изменяет коэффициент отношения длин отрезков фрактального дерева.
        /// </summary>
        /// <param name="sender"> Поле для ввода текста </param>
        /// <param name="e"> Информация об изменении текста </param>
        private void BranchesLengthChanged(object sender, TextChangedEventArgs e)
        {            
            if (!uint.TryParse((sender as TextBox).Text, out var length))
            {
                MessageBox.Show("Введено неверное значение (необходимо целое положительное число)");
                return;
            }
            if (length == 0 || length > 10)
            {
                MessageBox.Show("Неверное значение (необходимо число от 1 до 10)");
                return;
            }
            FractalTree.SetBranchesLength(length);
            DrawTree();
        }

        /// <summary>
        /// Изменяет углы наклона отрезков фрактального дерева.
        /// </summary>
        /// <param name="sender"> Поле для ввода текста </param>
        /// <param name="e"> Информация об изменении текста </param>
        private void AngleChanged(object sender, TextChangedEventArgs e)
        {
            if (!uint.TryParse((sender as TextBox).Text, out uint angle))
            {
                MessageBox.Show("Введено неверное значение (необходимо целое неотрицательное число)");
                return;
            }
            if (angle > 180)
            {
                MessageBox.Show("Неверный угол (необходимо значение от 0 до 180 градусов)");
                return;
            }
            if (sender == LeftTextBox)
            {
                FractalTree.SetLeftAngle(angle);
            }                
            else
            {
                FractalTree.SetRightAngle(angle);
            }
            DrawTree();
        }

        /// <summary>
        /// Изменяет расстояние между отрезками множества Кантора.
        /// </summary>
        /// <param name="sender"> Поле для ввода текста </param>
        /// <param name="e"> Информация об изменении текста </param>
        private void VerticalLengthChanged(object sender, TextChangedEventArgs e)
        {
            if (!uint.TryParse((sender as TextBox).Text, out uint length))
            {
                MessageBox.Show("Введено неверное значение (необходимо целое положительное число)");
                return;
            }
            if (length < 10 || length > 100)
            {
                MessageBox.Show("Неверное значение (необходимо число от 10 до 100)");
                return;
            }
            CantorSet.SetVerticalLength(length);
            DrawSet();
        }

        /// <summary>
        /// Изменяет глубину рекурсии фрактального дерева.
        /// </summary>
        /// <param name="sender"> Слайдер фрактального дерева </param>
        /// <param name="e"> Информация об изменении значения слайдера </param>
        private void TreeDepthChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            treeDepth = (uint)DepthSliderTree.Value;
            DrawTree();
        }

        /// <summary>
        /// Изменяет глубину рекурсии кривой Коха.
        /// </summary>
        /// <param name="sender"> Слайдер кривой Коха </param>
        /// <param name="e"> Информация об изменении значения слайдера </param>
        private void CurveDepthChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            curveDepth = (uint)DepthSliderCurve.Value;
            DrawCurve();
        }

        /// <summary>
        /// Изменяет глубину рекурсии ковра Серпинского.
        /// </summary>
        /// <param name="sender"> Слайдер ковра Серпинского </param>
        /// <param name="e"> Информация об изменении значения слайдера </param>
        private void CarpetDepthChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            carpetDepth = (uint)DepthSliderCarpet.Value;
            DrawCarpet();
        }

        /// <summary>
        /// Изменяет глубину рекурсии треугольника Серпинского.
        /// </summary>
        /// <param name="sender"> Слайдер треугольника Серпинского </param>
        /// <param name="e"> Информация об изменении значения слайдера </param>
        private void TriangleDepthChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            triangleDepth = (uint)DepthSliderTriangle.Value;
            DrawTriangle();
        }

        /// <summary>
        /// Изменяет глубину рекурсии множества Кантора.
        /// </summary>
        /// <param name="sender"> Слайдер множества Кантора </param>
        /// <param name="e"> Информация об изменении значения слайдера </param>
        private void SetDepthChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setDepth = (uint)DepthSliderSet.Value;
            DrawSet();
        }

        /// <summary>
        /// Выбирает имя файла и записывает по нему текущий холст в словарь.
        /// </summary>
        /// <param name="sender"> Кнопка конкретного холста </param>
        /// <returns> Имя файла </returns>
        private string ReturnFileName(object sender)
        {
            string name = "";
            if (sender == TreeSave)
            {
                name += "fractal_tree";
                if (!s_canvases.ContainsKey(name))
                    s_canvases.Add(name, CanvasTree);
            }
            else if (sender == CurveSave)
            {
                name += "koch_curve";                
                if (!s_canvases.ContainsKey(name))
                    s_canvases.Add(name, CanvasCurve);
            }
            else if (sender == CarpetSave)
            {
                name += "sierpinski_carpet";
                if (!s_canvases.ContainsKey(name))
                    s_canvases.Add(name, CanvasCarpet);
            }
            else if (sender == TriangleSave)
            {
                name += "sierpinski_triangle";
                if (!s_canvases.ContainsKey(name))
                    s_canvases.Add(name, CanvasTriangle);
            }
            else
            {
                name += "cantor_set";
                if (!s_canvases.ContainsKey(name))
                    s_canvases.Add(name, CanvasSet);
            }
            return name;
        }

        /// <summary>
        /// Открывает окно сохранения.
        /// </summary>
        /// <param name="sender"> Кнопка текущего холста </param>
        /// <param name="e"> Информация о событии </param>
        private void SaveCanvasClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            try
            {
                saveFileDialog.Filter = "Формат JPEG (*.jpg)|*.jpg|Формат PNG (*.png)|*.png";
                string filename = ReturnFileName(sender);
                saveFileDialog.FileName = filename;
                if (saveFileDialog.ShowDialog() == true)
                {
                    Save(saveFileDialog.FileName, s_canvases[filename]);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка");
            }
        }
        
        /// <summary>
        /// Сохраняет холст.
        /// </summary>
        /// <param name="filename"> Имя файла </param>
        /// <param name="canvas"> Текущий холст </param>
        private static void Save(string filename, Canvas canvas)
        {
            RenderTargetBitmap rtb = new((int)canvas.ActualWidth, (int)canvas.ActualHeight,
                96d, 96d, PixelFormats.Default);
            rtb.Render(canvas);

            using FileStream fs = new(filename, FileMode.Create);
            BitmapEncoder encoder = System.IO.Path.GetExtension(filename).Equals(".png") ?
                new PngBitmapEncoder() : new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(fs);
        }
    }
}
