using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InertiaByZeinekken
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            while (true)
            {
                Menu menu = new Menu();
                menu.ShowDialog();
                if (menu.Choice == 1)
                {
                    Levels level = new Levels();
                    level.ShowDialog();
                    if (level.LevelNumber != 100)
                    {
                        GameWindow Game = new GameWindow(level.LevelNumber);
                        Game.ShowDialog();
                    }
                }
                else if (menu.Choice == 2)
                {
                    Records record = new Records();
                    record.ShowDialog();
                }
                else break;
            }
            Close();
        }



    }
}