using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4
{
    public static class FloydWarshallAlgorithm
    {
        public static double[,] GetResult(double[,] d)
        {
            int lenght = d.GetLength(0);
            var distance = new double[lenght, lenght];
            for (int i = 0; i < lenght; i++)
                for (int j = 0; j < lenght; j++)
                {
                    if (d[i, j] == 0 && i != j)
                    {
                        distance[i, j] = double.MaxValue;
                    }
                    else
                    {
                        distance[i, j] = d[i, j];
                    }
                }

            for (int k = 0; k < d.GetLength(0); k++)
            {
                for (int i = 0; i < d.GetLength(0); i++)
                {
                    for (int j = 0; j < d.GetLength(0); j++)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                        {
                            distance[i, j] = distance[i, k] + distance[k, j];
                        }
                    }
                }
            }
            return distance;
        }
    }
}
