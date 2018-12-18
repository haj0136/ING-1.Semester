using System;
using System.Collections.Generic;

namespace CV_9
{
    class Program
    {
        static void Main(string[] args)
        {
            const int m0 = 3;
            const int m = 3;
            const int numberOfNodes = 2000;
            var rnd = new Random();

            var graph = new Graph(m0);

            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                for (int j = 0; j < graph.NodeList.Count; j++)
                {
                    if(i == j)
                        continue;
                    graph.NodeList[i].Neighbors.Add(graph.NodeList[j]);
                }
            }

            var nodes = new List<int>();
            foreach (var node in graph.NodeList)
            {
                node.Degree = node.Neighbors.Count;
                for (int i = 0; i < node.Degree; i++)
                {
                    nodes.Add(node.Id);
                }
            }


            for (int i = 0; i < numberOfNodes; i++)
            {
                var node = new GraphNode(m0 + 1 + i);
                var usedNodesIds = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    int randomIndex = rnd.Next(nodes.Count - 1);
                    while (usedNodesIds.Contains(nodes[randomIndex]))
                    {
                        randomIndex = rnd.Next(nodes.Count - 1);
                    }

                    GraphNode newNeighbour = graph.NodeList.Find(x => x.Id == nodes[randomIndex]);
                    node.Neighbors.Add(newNeighbour);
                    newNeighbour.Neighbors.Add(node);
                    usedNodesIds.Add(nodes[randomIndex]);
                }

                for (int j = 0; j < m; j++)
                {
                    nodes.Add(node.Id);
                    nodes.Add(node.Neighbors[j].Id);
                }

                graph.NodeList.Add(node);
            }

            graph.WriteToCSV("test.csv");
        }
    }
}
