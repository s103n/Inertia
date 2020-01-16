using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{


    class TimePlus : Cell
    {
        private Image image;
        public TimePlus(int x, int y) : base(x, y)
        {
            ElementType = ElementType.TimePlus;
            image = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/clockPlus.png"))
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