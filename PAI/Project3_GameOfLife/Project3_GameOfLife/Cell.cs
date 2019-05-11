using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_GameOfLife
{
    public class Cell
    {
        public bool IsAlive { get; private set; }
        public readonly Point Point;

        public Cell(Point point, bool isAlive)
        {
            this.Point = point;
            IsAlive = isAlive;
        }

        public Cell(Cell cell)
        {
            IsAlive = cell.IsAlive;
            this.Point = new Point(cell.Point.X, cell.Point.Y);
        }

        public void Revive()
        {
            IsAlive = true;
        }

        public void Kill()
        {
            IsAlive = false;
        }
    }
}
