using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{

    class Coin : Cell
    {
        private Image coinImage;
        public Coin(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Coin;
            coinImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/coin.png"))
            };
            coinImage.Width = coinImage.Height = CellSize;

        }
        public override void Draw()
        {
            coinImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(coinImage);
        }
    }

}