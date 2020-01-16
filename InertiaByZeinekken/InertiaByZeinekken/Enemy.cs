using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken
{
    struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class Enemy : Character
    {
        private readonly Field enemyField;
        public static int playerX;
        public static int playerY;
        private int nextPointX = 0;
        private int nextPointY = 0;
        private List<Point> AllUsingPoints = new List<Point>();
        private Player _player;
        double CalculateWaySize(int aX, int aY, int bX, int bY) => Math.Sqrt(Math.Pow((aX - bX), 2) + Math.Pow((aY - bY), 2));

        public Enemy(int x, int y, Field gameField) : base(x, y)
        {
            ElementType = ElementType.Enemy;
            enemyField = gameField;
            image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Player1.png"));
        }

        public void MoveByDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    switch (enemyField[X, Y - 1].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            Y--;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Down:
                    switch (enemyField[X, Y + 1].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            Y++;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Right:
                    switch (enemyField[X + 1, Y].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            X++;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Left:
                    switch (enemyField[X - 1, Y].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            X--;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Up_Left:
                    switch (enemyField[X - 1, Y - 1].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            X--;
                            Y--;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Up_Right:
                    switch (enemyField[X + 1, Y - 1].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            X++;
                            Y--;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Down_Left:
                    switch (enemyField[X - 1, Y + 1].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            X--;
                            Y++;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
                case Direction.Down_Right:
                    switch (enemyField[X + 1, Y + 1].ElementType)
                    {
                        case ElementType.Empty:
                            enemyField[X, Y] = new Empty(X, Y);
                            X++;
                            Y++;
                            enemyField[X, Y] = this;
                            break;
                    }
                    break;
            }
        }

        public void Go(Player player)
        {
            _player = player;
            FindCurrentPoints(CheckAllPosibilitiesToGo());
            MoveToPoint(nextPointX, nextPointY);
        }

        public bool AttackPlayer() // Если игрок в квадрате видимиости противника, монстр его атакует
        {
            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i == playerX && j == playerY)
                    {
                        _player.Lifes--;
                        Level.Act("Monster атаковал игрока");
                        return true;
                    }
                }
            }
            return false;
        }

        #region Алгоритмы поиска игрока в подфункциях

        bool IsPreviousPoint(int x, int y) // Проверка предыдущих точек
        {
            for (int i = 0; i < AllUsingPoints.Count; i++)
            {
                if (AllUsingPoints[i].X == x && AllUsingPoints[i].Y == y)
                    return true;
            }
            return false;
        }

        void FindCurrentPoints(Point[] k) // Поиск подходящей точки
        {
            double res = 999.0;
            for (int i = 0; i < k.Length; i++)
            {
                double compareNumber = CalculateWaySize(playerX, playerY, k[i].X, k[i].Y);

                if (compareNumber < res && !IsPreviousPoint(k[i].X, k[i].Y))
                {
                    res = compareNumber;
                    nextPointX = k[i].X;
                    nextPointY = k[i].Y;
                }
            }
        }
        private void MoveToPoint(int x, int y) // Движение к точке
        {
            if (AllUsingPoints.Count == 9 || AttackPlayer())
            {
                AllUsingPoints.Clear();
            }
            AllUsingPoints.Add(new Point(x, y));

            if (X - 1 == x)
                MoveByDirection(Direction.Left);
            else if (X + 1 == x)
                MoveByDirection(Direction.Right);

            if (Y - 1 == y)
                MoveByDirection(Direction.Up);
            else if (Y + 1 == y)
                MoveByDirection(Direction.Down);

            if (X - 1 == x && Y - 1 == y)
                MoveByDirection(Direction.Up_Left);
            else if (X + 1 == x && Y - 1 == y)
                MoveByDirection(Direction.Up_Right);
            else if (X - 1 == x && Y + 1 == y)
                MoveByDirection(Direction.Down_Left);
            else if (X + 1 == x && Y + 1 == y)
                MoveByDirection(Direction.Down_Right);
        }
        private Point[] CheckAllPosibilitiesToGo() // поиск всех возможных ходов
        {
            List<Point> points = new List<Point>();
            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    switch (enemyField[i, j].ElementType)
                    {
                        case ElementType.Empty:
                            points.Add(new Point(i, j));
                            break;
                    }
                }
            }
            return points.ToArray();
        }
        public void findPlayerOnField(Player player)
        {
            playerX = player.X;
            playerY = player.Y;
        }
        #endregion

    }

}
