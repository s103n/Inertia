using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{

    class Trap : Cell
    {
        private Image trapImage;
        public Trap(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Trap;
            trapImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/trap.png"))
            };
            trapImage.Width = trapImage.Height = CellSize;

        }
        public override void Draw()
        {
            trapImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(trapImage);
        }
    }

}