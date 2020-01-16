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

namespace InertiaByZeinekken
{
    /// <summary>
    /// Логика взаимодействия для Levels.xaml
    /// </summary>
    public partial class Levels : Window
    {
        public int LevelNumber { get; set; }
        public Levels()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LevelNumber = 1;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LevelNumber = 2;
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LevelNumber = 3;
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LevelNumber = 100;
            Close();
        }
    }
}
