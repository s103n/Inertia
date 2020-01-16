using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{

    class DoorKey : Cell
    {
        private Image keyImage;
        public DoorKey(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Key;
            keyImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/key.png"))
            };
            keyImage.Width = keyImage.Height = CellSize;

        }
        public override void Draw()
        {
            keyImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(keyImage);
        }
    }

}