using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4
{
    public static class Centralities
    {
        public static void GetClosenessCentrality(int[,] matrix)
        {
            double averageCC = 0;
            int numberOfNodes = matrix.GetLength(0);
            for (int i = 0; i < numberOfNodes; i++)
            {
                double closenessCentrality = 0;
                double sum = 0;
                for (int j = 0; j < numberOfNodes; j++)
                {
                    sum += matrix[i, j];
                }
                closenessCentrality = numberOfNodes / sum;
                Console.WriteLine("Vertex {0}: Closeness centrality = {1}", i + 1, closenessCentrality);
                averageCC += closenessCentrality;
            }
            Console.WriteLine();
            averageCC = averageCC / numberOfNodes;
            Console.WriteLine("Average closeness centrality = {0}", averageCC);
            Console.WriteLine();
        }
    }
}
