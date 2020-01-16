using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Media.Imaging;

namespace InertiaByZeinekken

{
    public class Player : Character
    {
        public bool Moving { get; set; }
        public Direction Pressed { get; set; }
        private Field PlayerField { get; set; }

        public Player(int x, int y) : base(x, y)
        {
            ElementType = ElementType.Player;
            Moving = false;
            image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/player.png"));
        }

        public void Move(bool reverse = false)
        {
            switch (Pressed)
            {
                case Direction.Up:
                    if (reverse)
                        Y++;
                    else
                        Y--;
                    break;
                case Direction.Down:
                    if (reverse)
                        Y--;
                    else
                        Y++;
                    break;
                case Direction.Left:
                    if (reverse)
                        X++;
                    else
                        X--;
                    break;
                case Direction.Right:
                    if (reverse)
                        X--;
                    else
                        X++;
                    break;
                case Direction.Up_Right:
                    if (reverse)
                    {
                        X--;
                        Y++;
                    }
                    else
                    {
                        X++;
                        Y--;
                    }
                    break;
                case Direction.Up_Left:
                    if (reverse)
                    {
                        X++;
                        Y++;
                    }
                    else
                    {
                        X--;
                        Y--;
                    }
                    break;
                case Direction.Down_Left:
                    if (reverse)
                    {
                        X++;
                        Y--;
                    }
                    else
                    {
                        X--;
                        Y++;
                    }
                    break;
                case Direction.Down_Right:
                    if (reverse)
                    {
                        X--;
                        Y--;
                    }
                    else
                    {
                        X++;
                        Y++;
                    }
                    break;
            }
        }

        public void Go(Field gameField) // mmm?
        {
            PlayerField = gameField;
            Move();
            switch (gameField[X, Y].ElementType)
            {
                case ElementType.Wall:
                    Wall();
                    break;
                case ElementType.Trap:
                    Trap();
                    break;
                case ElementType.Prize:
                    Diamond();
                    break;
                case ElementType.Stop:
                    StopCell();
                    break;
                case ElementType.Key:
                    KeyPick();
                    break;
                case ElementType.Coin:
                    Coin();
                    break;
                case ElementType.Door:
                    DoorPick();
                    break;
                case ElementType.Chest:
                    ChestPick();
                    break;
                case ElementType.Teleport:
                    Teleportation();
                    break;
                case ElementType.TimeMinus:
                    TimeMinus();
                    break;
                case ElementType.TimePlus:
                    TimePlus();
                    break;
                case ElementType.Empty:
                default:
                    break;
            }
        }

        private void Wall()
        {
            Move(true);
            Moving = false;
        }

        private void Trap()
        {
            Lifes--;
            Move(true);
            Moving = false;
            Pressed = Direction.None;
            Level.Act("Игрок ударился об ловушку");
        }

        private void Diamond()
        {
            Score += 50;
            Level.Act("Игрок подобрал алмаз");
        }

        private void StopCell()
        {
            Stop.StopCellsList.Add(new Point(X, Y));
            Moving = false;
            Pressed = Direction.None;
            Level.Act("Игрок остановился");
        }

        private void KeyPick()
        {
            Key++;
            Level.Act("Игрок подобрал ключ");
        }

        private void Coin()
        {
            AddScore += 10;
            Level.Act("Игрок собрал монетку");
        }

        private void DoorPick()
        {
            if (!Door.isOpenDoor(X, Y))
            {
                if (Key > 0)
                {
                    Key--;
                    Door.DoorsList.Add(new Point(X, Y));
                    Level.Act("Игрок открыл двери");
                }
                else
                {
                    Move(true);
                    Moving = false;
                    Level.Act("Двери закрыты вам нужен ключ");
                }
            }
        }

        private void ChestPick()
        {
            Move(true);
            Moving = false;
            if (Key > 0)
            {
                Key--;
                Point tempPoint = Chest.FindChest(PlayerField, X, Y);
                Chest.SpawnRandomDrop(tempPoint.X, tempPoint.Y, PlayerField);
                Pressed = Direction.None;
                Level.Act("Игрок открыл сундук");
            }
            else Level.Act("Сундук закрыт вам нужен ключ");
        }

        private void Teleportation()
        {
            Level.isTeleport = true;
            Moving = false;
            Teleport.TeleportP(PlayerField, this);
            Level.Act("Игрок телепортировался");
        }

        private void TimeMinus()
        {
            Time -= 10;
            Level.Act("Игрок потерял 10 сек.");
        }

        private void TimePlus()
        {
            Time += 20;
            Level.Act("Игроку добавилось 20 сек.");
        }
    }


}
