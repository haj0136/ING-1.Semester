using System;
using System.Threading.Tasks;

namespace Project2_LU_Decomposition
{
    public class Matrix
    {
        public double[,] M { set; get; }
        private readonly int _colsCount;
        private readonly int _rowsCount;

        public Matrix(int m, int n)
        {
            _rowsCount = m;
            _colsCount = n;
            M = new double[_rowsCount, _colsCount];
        }

        public Matrix(int size) : this(size, size)
        {

        }

        public double this[int i, int j]
        {
            get => M[i, j];
            set => M[i, j] = value;
        }

        public void MakeRandomMatrix()
        {
            var rand = new Random();
            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _colsCount; j++)
                {
                    int x = rand.Next(-99, 99);
                    if (i == j)
                    {
                        while (x == 0)
                        {
                            x = rand.Next(-99, 99);
                        }
                    }

                    M[i, j] = x;
                }
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _colsCount; j++)
                {
                    Console.Write($" {M[i, j]:F1} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public (Matrix L, Matrix U) LuDecomposition()
        {
            var U = new Matrix(_colsCount);
            var L = GetIdentityMatrix();

            for (int i = 0; i < _colsCount; i++)
            {
                for (int j = i; j < _colsCount; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < i; k++)
                    {
                        temp += L[i, k] * U[k, j];
                    }

                    U[i, j] = M[i, j] - temp;
                }

                for (int j = i + 1; j < _rowsCount; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < i; k++)
                    {
                        temp += L[j, k] * U[k, i];
                    }

                    L[j, i] = (M[j, i] - temp) / U[i, i];
                }
            }

            return (L, U);
        }

        public (Matrix L, Matrix U) ParallelLuDecomposition()
        {
            var U = new Matrix(_colsCount);
            var L = GetIdentityMatrix();

            for (int i = 0; i < _colsCount; i++)
            {
                int index = i;
                Parallel.For(index, _colsCount, j =>
                {
                    double temp = 0;
                    for (int k = 0; k < index; k++)
                    {
                        temp += L[index, k] * U[k, j];
                    }
                    U[index, j] = M[index, j] - temp;
                });
                Parallel.For(index + 1, _rowsCount, j =>
                {
                    double temp = 0;
                    for (int k = 0; k < index; k++)
                    {
                        temp += L[j, k] * U[k, index];
                    }
                    L[j, index] = (M[j, index] - temp) / U[index, index];
                });
            }

            return (L, U);
        }

        public Matrix GetIdentityMatrix()
        {
            var matrix = new Matrix(_rowsCount, _colsCount);
            for (int i = 0; i < _rowsCount; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }
    }

}
