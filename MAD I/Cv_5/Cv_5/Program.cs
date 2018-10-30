using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_5
{
    // shlukovací koeficient pro každy vrchol
    // průměrný shlukovací koeficient
    // shlukovací koeficient x Degree (stejny stupen, jiny CC)
    class Program
    {
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

            Console.WriteLine(graph.AverageDegree);
            Console.WriteLine();

            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                var subGraph = new List<GraphNode>();
                foreach (var node in graph.NodeList[i].Neighbours)
                {
                    subGraph.Add(new GraphNode(node));
                }
                int edgesCount = 0;
                for (int j = 0; j < subGraph.Count; j++)
                {
                    for (int k = subGraph[j].Neighbours.Count - 1; k > -1; k--)
                    {
                        if (!subGraph.Contains(subGraph[j].Neighbours[k]))
                        {
                            subGraph[j].Neighbours.Remove(subGraph[j].Neighbours[k]);
                        }
                    }
                }

                for (int j = 0; j < subGraph.Count; j++)
                {
                    edgesCount += subGraph[j].Neighbours.Count;
                }

                float result = ((float)edgesCount) / (subGraph.Count * (subGraph.Count - 1));
                if (!float.IsNaN(result))
                {
                    graph.NodeList[i].ClusteringCoeficient = result;
                }
                else
                {
                    graph.NodeList[i].ClusteringCoeficient = 0;
                }

            }
            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                Console.WriteLine("Node: " + (i + 1) + ", degree =  " + graph.NodeList[i].Degree + ", CC = " + graph.NodeList[i].ClusteringCoeficient);
                graph.AverageCC += graph.NodeList[i].ClusteringCoeficient;
            }
            graph.AverageCC /= graph.NodeList.Count;
            Console.WriteLine();
            Console.WriteLine("Average clustering coificient =  {0}", graph.AverageCC);


            graph.WriteToCSV("statistics.csv");
        }
    }
}
