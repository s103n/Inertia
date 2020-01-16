using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InertiaByZeinekken
{

    public class Character : Cell
    {
        protected Image image;

        public int Lifes { get; set; }
        public int Time { get; set; }
        public int Score { get; set; }
        public int Key { get; set; }
        public int AddScore { get; set; }

        public Character(int x, int y) : base(x, y)
        {
            X = x;
            Y = y;
            Key = 0;
            Score = 0;
            Time = 100;
            Lifes = 10;
            AddScore = 0;
            image = new Image();
            image.Width = image.Height = CellSize;
        }

        public override void Draw()
        {
            image.Margin = new Thickness(X * CellSize, Y * CellSize, 0, 0);
            Level.canvas.Children.Add(image);
        }
    }

}
