using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Project3_GameOfLife
{
    public class Grid
    {
        private Cell[,] _cells;

        public readonly int MaxCells;
        private const int GridSize = 1000;
        private readonly int _cellSize;
        public int Generations { get; private set; }


        public Grid()
        {
            var rand = new Random();
            Generations = 0;
            MaxCells = 100;
            _cellSize = 8;

            _cells = new Cell[MaxCells, MaxCells];

            for (int i = 0; i < MaxCells; i++)
            {
                int x = _cellSize * i + 12;

                for (int j = 0; j < MaxCells; j++)
                {
                    int y = _cellSize * j + 12;
                    var point = new Point(x, y);
                    if (rand.Next(100) < 20)
                    {
                        _cells[i, j] = new Cell(point, true);
                    }
                    else
                    {
                        _cells[i, j] = new Cell(point, false);
                    }
                }
            }
        }

        public void UpdateGrid()
        {
            var cellsCopy = new Cell[MaxCells, MaxCells];
            
            for (int i = 0; i < MaxCells; i++)
            {
                for (int j = 0; j < MaxCells; j++)
                {
                    cellsCopy[i, j] = new Cell(_cells[i, j]);
                }
            }


            for (int i = 0; i < MaxCells; i++)
            {
                for (int j = 0; j < MaxCells; j++)
                {
                    int neighbors = GetNeighbors(i, j);

                    if (_cells[i, j].IsAlive && neighbors < 2)
                    {
                        cellsCopy[i, j].Kill();
                    }
                    if (_cells[i, j].IsAlive && neighbors > 3)
                    {
                        cellsCopy[i, j].Kill();
                    }
                    if (!_cells[i, j].IsAlive && neighbors == 3)
                    {
                        cellsCopy[i, j].Revive();
                    }
                }
            }

            _cells = cellsCopy;
            Generations++;
        }

        public void UpdateGridParallel()
        {
            var cellsCopy = new Cell[MaxCells, MaxCells];

            Parallel.For(0, MaxCells, i =>
            {
                Parallel.For(0, MaxCells, j => { cellsCopy[i, j] = new Cell(_cells[i, j]); });
            });


            Parallel.For(0, MaxCells, i => { Parallel.For(0, MaxCells, j =>
            {
                    int neighbors = GetNeighbors(i, j);

                    if (_cells[i, j].IsAlive && neighbors < 2)
                    {
                        cellsCopy[i, j].Kill();
                    }
                    if (_cells[i, j].IsAlive && neighbors > 3)
                    {
                        cellsCopy[i, j].Kill();
                    }
                    if (!_cells[i, j].IsAlive && neighbors == 3)
                    {
                        cellsCopy[i, j].Revive();
                    }
            }); });

            _cells = cellsCopy;
            Generations++;
        }

        private int GetNeighbors(int column, int row)
        {
            int neighbors = 0;
            int[] start = { Math.Max(0, column - 1), Math.Max(0, row - 1) };
            int[] end = { Math.Min(MaxCells - 1, column + 1), Math.Min(MaxCells - 1, row + 1) };

            for (int i = start[0]; i < end[0] + 1; i++)
            {
                for (int j = start[1]; j < end[1] + 1; j++)
                {
                    if (_cells[i, j].IsAlive && (i != column || j != row))
                    {
                        neighbors++;
                    }
                }
            }

            return neighbors;
        }

        public Bitmap ToBitmap()
        {
            var gridBitmap = new Bitmap(GridSize, GridSize);
            var cellSize = new Size(_cellSize, _cellSize);

            using (var graphics = Graphics.FromImage(gridBitmap))
            {
                var myPen = new Pen(Color.LightGray);
                var myBrush = new SolidBrush(Color.Black);

                for (int i = 0; i < MaxCells; i++)
                {
                    for (int j = 0; j < MaxCells; j++)
                    {
                        if (!_cells[i, j].IsAlive)
                        {
                            graphics.DrawRectangle(myPen, new Rectangle(_cells[i, j].Point, cellSize));
                        }
                        else
                        {
                            graphics.FillRectangle(myBrush, new Rectangle(_cells[i, j].Point, cellSize));
                        }
                    }
                }
                myPen.Dispose();
                myBrush.Dispose();
            }

            return gridBitmap;
        }
    }
}
