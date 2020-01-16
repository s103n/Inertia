using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{


    class Empty : Cell
    {
        private Image emptyImage;

        public Empty(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Empty;
            emptyImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/empty.png"))
            };
            emptyImage.Width = emptyImage.Height = CellSize;

        }

        public override void Draw()
        {
            emptyImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(emptyImage);
        }
    }

}