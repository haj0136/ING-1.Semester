using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4
{
    class Program
    {
        // closeness centralita pro každý vrchol ( + průměr)
        static void Main(string[] args)
        {
            const int numberOfNodes = 34;

            Graph graph = new Graph(numberOfNodes);

            DataLoader dt = new DataLoader();
            graph = dt.LoadData(graph);

            foreach (var node in graph.NodeList)
            {
                node.Degree = node.Neighbours.Count;
            }


            foreach (var node in graph.NodeList)
            {
                graph.AverageDegree += (double)node.Degree;
            }
            graph.AverageDegree /= graph.NodeList.Count;

            //Console.WriteLine(graph.AverageDegree);

            //for (int i = 0; i < graph.NodeList.Count; i++)
            //{
            //    Console.WriteLine("Node: " + (i + 1) + ", degree =  " + graph.NodeList[i].Degree);
            //}

            int[,] matrix = new int[numberOfNodes, numberOfNodes];
            matrix = dt.LoadDataToMatrix(matrix);

            //PrintMatrix(matrix);

            int[,] matrixOfShortestPaths = FloydWarshallAlgorithm.GetResult(matrix);

            PrintMatrix(matrixOfShortestPaths);
            Centralities.GetClosenessCentrality(matrixOfShortestPaths);
        }

        static void PrintMatrix(int[,] matrix)
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
    }
}
