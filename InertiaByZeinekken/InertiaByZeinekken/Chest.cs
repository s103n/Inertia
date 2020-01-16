using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{
    enum DropType
    {
        Prize, Trap, Coin, Key
    }

    class Chest : Cell
    {
        private Image chestImage;
        static List<Point> AllChests = new List<Point>();

        public Chest(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Chest;
            chestImage = new Image
            {
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/chest.png"))
            };
            chestImage.Width = chestImage.Height = CellSize;

        }
        public override void Draw()
        {
            chestImage.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(chestImage);
        }

        public static Point FindChest(Field gameField, int x, int y)
        {
            FindAllChest(gameField);
            for(int i = 0; i < AllChests.Count; i++)
            {
                if(AllChests[i].X == x || AllChests[i].Y == y)
                {
                    return new Point(AllChests[i].X, AllChests[i].Y);
                }
            }
            return new Point();
        }

        private static void FindAllChest(Field gameField)
        {
            for (int i = 0; i < gameField.Width; i++)
            {
                for (int j = 0; j < gameField.Height; j++)
                {
                    if (gameField[i, j].ElementType == ElementType.Chest)
                    {
                        AllChests.Add(new Point(i, j));
                    }
                }
            }
        }

        public static void SpawnRandomDrop(int x, int y, Field gameField)
        {
            Random rand = new Random();
            int k = rand.Next(1, 4);
            switch (k)
            {
                case 1:
                    gameField[x, y] = new Prize(x, y);
                    break;
                case 2:
                    gameField[x, y] = new Trap(x, y);
                    break;
                case 3:
                    gameField[x, y] = new Coin(x, y);
                    break;
                case 4:
                    gameField[x, y] = new DoorKey(x, y);
                    break;
            }
        }
    }

}