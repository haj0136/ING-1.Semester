using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CV_10
{
    public class Graph
    {
        public List<GraphNode> NodeList { get; }

        public double AverageDegree { get; set; }

        public double AverageCC { get; set; }

        private Random rnd;

        public Graph()
        {
            rnd = new Random();
            NodeList = new List<GraphNode>();
        }
        public Graph(int nodes) : this()
        {
            for (int i = 0; i < nodes; i++)
            {
                NodeList.Add(new GraphNode(i + 1));
            }
        }

        public void MakeRandomEdges(double p)
        {
            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = i + 1; j < NodeList.Count; j++)
                {
                    if (i != j && !NodeList[i].Neighbours.Contains(NodeList[j]))
                    {
                        if (rnd.NextDouble() < p)
                        {
                            NodeList[i].Neighbours.Add(NodeList[j]);
                            NodeList[j].Neighbours.Add(NodeList[i]);
                        }
                    }
                }
            }
        }

        //shlukovací efekt
        public void WriteToCSV(string filePath)
        {
            var map = new Dictionary<int, double>();
            var set = new HashSet<int>();
            var csv = new StringBuilder();
            csv.Append("Degree;Average Clustering Coeficient\n");
            for (int i = 0; i < NodeList.Count; i++)
            {
                set.Add(NodeList[i].Degree);
            }
            foreach (var item in set)
            {
                var temp = NodeList.Where(x => x.Degree == item).Select(x => x);
                map[item] = temp.Sum(x => x.ClusteringCoeficient) / temp.Count();
            }
            foreach (var item in map)
            {
                var newLine = string.Format("{0};{1}\n", item.Key, item.Value);
                csv.Append(newLine);
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        public double[,] ToMatrix()
        {
            var matrix = new double[NodeList.Count, NodeList.Count];

            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = 0; j < NodeList[i].Neighbours.Count; j++)
                {
                    int nodeIndex = NodeList.IndexOf(NodeList[i].Neighbours[j]);
                    matrix[i, nodeIndex] = 1;
                }
            }

            return matrix;
        }

        public void CalculateAverageDistance(double[,] matrix)
        {
            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = 0; j < NodeList.Count; j++)
                {
                    NodeList[i].AverageDistance += matrix[i, j];
                }
                NodeList[i].AverageDistance /= NodeList.Count;
            }
        }
    }
}
