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
    /// Логика взаимодействия для GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        public int Choice { get; set; }
        private string GameOverMessage;
        private Level CurrentLevel;
        private int LevelNumber;
        public GameOver(Level CurrentLevel, int LevelNumber, string message)
        {
            InitializeComponent();
            this.CurrentLevel = CurrentLevel;
            this.LevelNumber = LevelNumber;
            GameOverMessage = message;
            Message();
        }
        public void Message()
        {
            Label.Text = $"\n\n\n\t\t\t{GameOverMessage}\n" +
            $"\t\t\tScore: {CurrentLevel.player.Score}\n" +
            $"\t\t\tAddScore: {CurrentLevel.player.AddScore}\n" +
                $"\t\t\tLifes: {CurrentLevel.player.Lifes + 1}\n" +
                $"\t\t\tTime: {CurrentLevel.player.Time}";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Choice = 1;
            Close();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Choice = 2;
            Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Choice = 3;
            YourName name = new YourName();
            name.ShowDialog();
            Records record = new Records();
            record.WriteRecordToFile(CurrentLevel, LevelNumber, name.Name);

            MessageBox.Show("Your Result is Saving!");
            Close();
        }
    }
}
