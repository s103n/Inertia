using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InertiaByZeinekken
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    partial class GameWindow : Window
    {
        Level CurrentLevel;
        private int LevelNumber;
        DispatcherTimer gameTimer;
        DispatcherTimer enemyTimer;
        DispatcherTimer Timer;
        public GameWindow(int level)
        {
            InitializeComponent();
            CurrentLevel = new Level(level, GameCanvas);
            LevelNumber = level;
            StartGame();
        }

        void StartGame()
        {
            gameTimer = new DispatcherTimer // Поток игры
            {
                Interval = new TimeSpan(0, 0, 0, 0, 30)
            };
            gameTimer.Tick += new EventHandler(GameCycle);
            gameTimer.Start();

            enemyTimer = new DispatcherTimer // Поток врага
            {
                Interval = new TimeSpan(0, 0, 0, 0, 300)
            };
            enemyTimer.Tick += new EventHandler(EnemyCycle);
            enemyTimer.Start();

            Timer = new DispatcherTimer // Дополнительный поток
            {
                Interval = new TimeSpan(0, 0, 0, 0, 400)
            };
            Timer.Tick += new EventHandler(TimerCycle);
            Timer.Start();
        }
        private void EnemyCycle(object sender, EventArgs e)
        {
            CurrentLevel.EnemyGo();
        }
        private void TimerCycle(object sender, EventArgs e)
        {
            CurrentLevel.TimerGo();
        }
        private void GameCycle(object sender, EventArgs e)
        {
            if (CurrentLevel.Game)
            {
                CurrentLevel.UpdateField();
                CurrentLevel.PrintLevel();
                InfoMessage();
            }
            else
            {
                GameOver(Level.GameOverMessage);
            }
        }
        void GameOver(string message)
        {
            enemyTimer.Stop();
            gameTimer.Stop();
            Timer.Stop();
            GameOver gameOver = new GameOver(CurrentLevel, LevelNumber, message);
            gameOver.ShowDialog();
            GameCanvas.Children.Clear();
            if (gameOver.Choice == 1)
            {
                enemyTimer.Start();
                gameTimer.Start();
                Timer.Start();
                CurrentLevel = new Level(LevelNumber, GameCanvas);
            }
            if (gameOver.Choice == 2 || gameOver.Choice == 3)
            {
                Close();
            }
        }
        void StartPlayerMoving(Direction key)
        {
            CurrentLevel.player.Moving = true;
            CurrentLevel.player.Pressed = key;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!CurrentLevel.player.Moving)
            {
                switch (e.Key)
                {
                    case Key.NumPad8:
                        StartPlayerMoving(Direction.Up);
                        break;
                    case Key.NumPad2:
                        StartPlayerMoving(Direction.Down);
                        break;
                    case Key.NumPad4:
                        StartPlayerMoving(Direction.Left);
                        break;
                    case Key.NumPad6:
                        StartPlayerMoving(Direction.Right);
                        break;
                    case Key.NumPad7:
                        StartPlayerMoving(Direction.Up_Left);
                        break;
                    case Key.NumPad9:
                        StartPlayerMoving(Direction.Up_Right);
                        break;
                    case Key.NumPad1:
                        StartPlayerMoving(Direction.Down_Left);
                        break;
                    case Key.NumPad3:
                        StartPlayerMoving(Direction.Down_Right);
                        break;
                    default:
                        break;
                }
            }
        }
        private static List<string> actions = new List<string>(5) { "", "", "", "", "" };
        void InfoMessage()
        {
            score.Text = $"Score:{CurrentLevel.player.Score}\n" +
                $"Lifes:{CurrentLevel.player.Lifes}\n" +
                $"Time:{CurrentLevel.player.Time} sec \n" +
                $"Additional Score:{CurrentLevel.player.AddScore}\n" +
                $"Keys: { CurrentLevel.player.Key}";
            RecentAction.Text = $"  Previous Actions\n" +
                $"  {actions[0]}\n" +
                $"  {actions[1]}\n" +
                $"  {actions[2]}\n" +
                $"  {actions[3]}\n";

            if (actions.Count == 5)
            {
                LastAction.Text = $"  LastAction\n" + $"  {actions[4]}";
            }
            else LastAction.Text = $"  Last Action\n" + $"  {Level.LastAction}";

            if (!isPreviousAction(Level.LastAction))
            {
                if (actions.Count == 5)
                    actions.RemoveAt(0);
                actions.Add(Level.LastAction);
            }
            
        }
        bool isPreviousAction(string action)
        {
            if (actions[4] != action)
                return false;
            return true;
        }
    }
}
