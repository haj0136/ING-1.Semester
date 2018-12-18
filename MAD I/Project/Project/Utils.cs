using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Utils
    {
        public static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /// <returns>Int[3] vector where [0] = i (index of node)
        /// <para/>[1] = j (index of node)
        /// <para/>[2] = diameter</returns>
        public static int[] GetDiameter(double[,] matrix)
        {
            // result[0] = i, result[1] = j, result[2] = diameter
            var result = new int[3];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] > result[2])
                    {
                        result[0] = i;
                        result[1] = j;
                        result[2] = (int)matrix[i, j];
                    }
                }
            }

            return result;
        }
    }
}
