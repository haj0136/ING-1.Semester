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

            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                Console.WriteLine("Node: " + (i + 1) + ", degree =  " + graph.NodeList[i].Degree);
            }

            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                var subGraph = new List<GraphNode>(graph.NodeList[i].Neighbours);
                int edgesCount = 0;
                for (int j = 0; j < subGraph.Count; j++)
                {
                    for (int k = 0; k < subGraph[j].Neighbours.Count; k++)
                    {
                        if (!graph.NodeList[i].Neighbours.Contains(subGraph[j].Neighbours[k]))
                        {
                            subGraph[j].Neighbours.Remove(subGraph[j].Neighbours[k]);
                        }
                    }
                }

                for (int j = 0; j < subGraph.Count; j++)
                {
                    edgesCount += subGraph[j].Neighbours.Count;
                }

                edgesCount /= 2;

                //graph.NodeList[i].ClusteringCoeficient = 
            }
        }
    }
}
