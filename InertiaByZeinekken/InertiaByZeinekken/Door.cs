using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{
    enum doorStyle
    {
        Closed, Open
    }
    class Door : Cell
    {
        private Image doorImage;
        private readonly Dictionary<doorStyle, BitmapImage> imageByStyle;
        public static List<Point> DoorsList = new List<Point>();

        public Door(int x, int y, doorStyle Style = doorStyle.Closed) : base(x, y)
        {
            ElementType = ElementType.Door;
            imageByStyle = new Dictionary<doorStyle, BitmapImage>()
            {
                [doorStyle.Open] = new BitmapImage(new Uri(@"pack://application:,,,/Images/opendoor.png")),
                [doorStyle.Closed] = new BitmapImage(new Uri(@"pack://application:,,,/Images/closedDoor.png"))
            };
            SetImageUsingStyle(Style);
            doorImage.Width = doorImage.Height = CellSize;

        }

        public void SetImageUsingStyle(doorStyle style) => doorImage = new Image
        {
            Source = imageByStyle[style]
        };

        public override void Draw()
        {
            doorImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(doorImage);
        }

        public static bool isOpenDoor(int x, int y)
        {
            for (int i = 0; i < DoorsList.Count; i++)
            {
                if (x == DoorsList[i].X && y == DoorsList[i].Y)
                    return true;
            }
            return false;
        }

        public static void DrawDoors(Field gameField)
        {
            for (int i = 0; i < DoorsList.Count; i++)
            {
                gameField[DoorsList[i].X, DoorsList[i].Y] = new Door(DoorsList[i].X, DoorsList[i].Y, doorStyle.Open);
            }
        }
    }

}