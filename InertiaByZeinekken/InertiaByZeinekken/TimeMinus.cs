using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{


    class TimeMinus : Cell
    {
        private Image image;
        public TimeMinus(int x, int y) : base(x, y)
        {
            ElementType = ElementType.TimeMinus;
            image = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/clockMinus.png"))
            };
            image.Width = image.Height = CellSize;

        }
        public override void Draw()
        {
            image.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(image);
        }
    }

}