using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_3
{
    class Program
    {
        // 1. načíst síť
        // 2. pro kazdy vrchol spocitat stupen (stupen = pocet sousedu)
        // 3. Průměrný stupeň
        // 34 nodes
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
                Console.WriteLine("Node: " + (i+1) + " degree =  " +  graph.NodeList[i].Degree);
            }
            
        }
    }
}
