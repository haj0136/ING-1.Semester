using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate and print graph
            var graphGenerator = new GraphGenerator(m0: 3, m: 2, numberOfNodes: 1500);
            var graph = graphGenerator.Generate();
            graph.PrintToCSV("MyNetwork.csv");
            Console.WriteLine("Complete: 10%");

            // Degree distribution, average degree
            graph.CountNeighbors();
            graph.GetAverageDegree();
            graph.CalculateDegreeDistribution();
            Console.WriteLine("Complete: 20%");

            // Graph Diameter
            var distanceMatrix = FloydWarshallAlgorithm.GetResult(graph.ToMatrix());
            Console.WriteLine("Complete: 70%");
            graph.DiameterInfo = Utils.GetDiameter(distanceMatrix);
            Console.WriteLine("Complete: 80%");

            // Clustering coefficient
            graph.CalculateClusteringCoefficient();
            Console.WriteLine("Complete: 90%");

            //Print to File
            graph.PrintGraphInfoToFile("MyNetworkInfo.txt");
            
        }
    }
}
