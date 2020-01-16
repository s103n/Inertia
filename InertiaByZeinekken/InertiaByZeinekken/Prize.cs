using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{

    class Prize : Cell
    {
        private Image prizeImage;
        public Prize(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Prize;
            prizeImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/diamond.png"))
            };
            prizeImage.Width = prizeImage.Height = CellSize;

        }
        public override void Draw()
        {
            prizeImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(prizeImage);
        }
    }

}