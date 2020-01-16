using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace InertiaByZeinekken
{
    public class Level
    {
        private Field gameField;
        public Player player;
        private Enemy enemy;
        public bool Game;
        public static Canvas canvas;
        public static string GameOverMessage;
        public static string LastAction;
        public static bool isTeleport;

        public Level(int level, Canvas canvas)
        {
            Door.DoorsList.Clear();
            Teleport.AllTeleportsOnMap.Clear();
            Stop.StopCellsList.Clear();
            Game = true;
            isTeleport = false;
            LastAction = $"Welcome on the {level} level";
            new LevelLoader(level).LoadField(out gameField, out player, out enemy);
            player.Pressed = Direction.None;
            Level.canvas = canvas;
        }

        public void PrintLevel()
        {
            canvas.Children.Clear();
            gameField.Print();
            Stop.DrawStopCells(gameField);
            Door.DrawDoors(gameField);
            Teleport.DrawTeleportsCell(gameField);
        }

        public void UpdateField()
        {
            player.Go(gameField);
            enemy.findPlayerOnField(player);
            GameEnd();
            if (isTeleport)
            {
                RenderAllField();
            }
            else
            {
                PutPlayerOnField(player);
                PutPlayerOnField(enemy);
            }
            isTeleport = false;
        }

        public static void Act(string action) => LastAction = action;

        private void GameEnd()
        {
            if (player.Lifes <= 0)
            {
                GameOverMessage = "You lost";
                Game = false;
            }
            else if(player.Time == 0)
            {
                GameOverMessage = "You lost by enough time";
                Game = false;
            }
            else if(player.Score == 100)
            {
                GameOverMessage = "You win";
                Game = false;
            }
        }

        public void EnemyGo()
        {
            enemy.Go(player);
        }

        public void TimerGo()
        {
            enemy.AttackPlayer();
            player.Time--;
        }
        
        private void RenderAllField()
        {
            for(int j = 0; j < gameField.Height; j++)
            {
                for(int i = 0; i < gameField.Width; i++)
                {
                    if(!(gameField[i, j] is Teleport) && !(gameField[i, j] is Chest) && !(gameField[i, j] is Coin) && !(gameField[i, j] is Door)
                        && !(gameField[i, j] is DoorKey) && !(gameField[i, j] is Prize) && !(gameField[i, j] is Stop) && !(gameField[i, j] is Trap) &&
                        !(gameField[i, j] is Wall) && !(gameField[i, j] is TimeMinus) && !(gameField[i, j] is TimePlus))
                    {
                        gameField[i, j] = new Empty(i, j);
                    }
                }
            }
            gameField[player.X, player.Y] = player;
            gameField[enemy.X, enemy.Y] = enemy;
        }

        private void PutPlayerOnField(Character character)
        {
            int x = character.X;
            int y = character.Y;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx + x >= 0 && dx + x < gameField.Width &&
                        dy + y >= 0 && dy + y < gameField.Height &&
                        gameField[dx + x, dy + y] == character)
                    {
                        gameField[dx + x, dy + y] = new Empty(dx + x, dy + y);
                        break;
                    }
                }
            }
            gameField[x, y] = character;
        }
    }

}
