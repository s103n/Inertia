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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public int Choice { get; set; }
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Choice = 1;
            Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Choice = 2;
            Close();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Choice = 3;
            Close();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            Choice = 100;
            Close();
        }
    }
}
