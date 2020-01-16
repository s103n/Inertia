using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{

    class Stop : Cell
    {
        private Image stopImage;
        public static List<Point> StopCellsList = new List<Point>();

        public Stop(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Stop;
            stopImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/stopCell.png"))
            };
            stopImage.Width = stopImage.Height = CellSize;

        }

        public override void Draw()
        {
            stopImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(stopImage);
        }

        public static void DrawStopCells(Field gameField)
        {
            for (int i = 0; i < StopCellsList.Count; i++)
            {
                gameField[StopCellsList[i].X, StopCellsList[i].Y] = new Stop(StopCellsList[i].X, StopCellsList[i].Y);
            }
        }
    }

}