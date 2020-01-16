using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Records.xaml
    /// </summary>
    public partial class Records : Window
    {
        private readonly string path = $"../../Data/records.txt";

        public Records()
        {
            InitializeComponent();
            records.Text = ReadFromFile();
        }

        public void WriteRecordToFile(Level CurrentLevel, int NumberOfLevel, string Name)
        {
            string text = $"{Name} - Time:{CurrentLevel.player.Time}, Score:{CurrentLevel.player.Score}, Level:{NumberOfLevel}.";

            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(text);
            }
        }
        public string ReadFromFile()
        {
            string[] text = new FileStorer(path).Result;
            string result = "";

            for(int i = 0; i < text.Length; i++)
            {
                result += text[i] + "\n";
            }
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
