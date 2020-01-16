using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InertiaByZeinekken
{

   public abstract class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ElementType ElementType { get; set; }
        protected int CellSize = 40;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;

        }

        public abstract void Draw();
    }

}
