using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{


    class Teleport : Cell
    {
        private Image teleportImage;
        public static List<Point> AllTeleportsOnMap = new List<Point>();

        public Teleport(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Teleport;
            teleportImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/MagicMirror.png"))
            };
            teleportImage.Width = teleportImage.Height = CellSize;
        }
        public override void Draw()
        {
            teleportImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(teleportImage);
        }

        public static void TeleportP(Field gameField, Player player)
        {
            FindAllPosibleTeleports(gameField);

            int t = ChooseRandTeleport();
            if (AllTeleportsOnMap[t].X != player.X && AllTeleportsOnMap[t].Y != player.Y)
            {
                player.X = AllTeleportsOnMap[t].X;
                player.Y = AllTeleportsOnMap[t].Y;
            }
        }
        private static int ChooseRandTeleport()
        {
            Random RandWayToT = new Random();
            return RandWayToT.Next(0, AllTeleportsOnMap.Count);
        }
        private static void FindAllPosibleTeleports(Field gameField)
        {
            for(int i = 0; i < gameField.Width; i++)
            {
                for(int j = 0;  j < gameField.Height; j++)
                {
                    if(gameField[i, j].ElementType == ElementType.Teleport)
                    {
                        AllTeleportsOnMap.Add(new Point(i, j));
                    }
                }
            }
        }
        public static void DrawTeleportsCell(Field gameField)
        {
            for (int i = 0; i < AllTeleportsOnMap.Count; i++)
            {
                gameField[AllTeleportsOnMap[i].X, AllTeleportsOnMap[i].Y] = new Teleport(AllTeleportsOnMap[i].X, AllTeleportsOnMap[i].Y);
            }
        }
    }

}