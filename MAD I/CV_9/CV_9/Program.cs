using System;
using System.Collections.Generic;

namespace CV_9
{
    class Program
    {
        static void Main(string[] args)
        {
            const int m0 = 10;
            const int m = 3;
            const double P = 0.2;
            const int numberOfNodes = 1000;
            Random rnd = new Random();

            var graph = new Graph(m0);

            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                while (graph.NodeList[i].Neighbours.Count < 2)
                {
                    int randomNumber = rnd.Next(graph.NodeList.Count - 1);
                    while (randomNumber == i)
                    {
                        randomNumber = rnd.Next(graph.NodeList.Count - 1);
                    }
                    graph.NodeList[i].Neighbours.Add(graph.NodeList[randomNumber]);
                }
            }

            var nodes = new List<int>();
            foreach (var node in graph.NodeList)
            {
                node.Degree = node.Neighbours.Count;
                Console.WriteLine(node.Degree);
                for (int i = 0; i < node.Degree; i++)
                {
                    nodes.Add(node.Id);
                }
            }


            for (int i = 0; i < numberOfNodes; i++)
            {
                var node = new GraphNode(m0 + 1 + i);
                var usedNodes = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    int randomIndex = rnd.Next(nodes.Count - 1);
                    while (usedNodes.Contains(randomIndex))
                    {
                        randomIndex = rnd.Next(nodes.Count - 1);
                    }

                    GraphNode newNeighbour = graph.NodeList.Find(x => x.Id == nodes[randomIndex]);
                    node.Neighbours.Add(newNeighbour);
                    newNeighbour.Neighbours.Add(node);
                }

                for (int j = 0; j < m; j++)
                {
                    nodes.Add(node.Id);
                    nodes.Add(node.Neighbours[j].Id);
                }

                graph.NodeList.Add(node);
            }

            graph.WriteToCSV("test.csv");
        }
    }
}
