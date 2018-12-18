using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Project
{
    public class Graph
    {
        public List<GraphNode> NodeList { get; }
        public List<int> DegreeFrequency { get; private set; }
        public List<double> DegreeDistribution { get; private set; }
        public double AverageDegree { get; private set; }
        public int[] DiameterInfo { get; set; }
        public double AverageCC { get; set; }

        // private readonly Random _rnd;

        public Graph(int nodes)
        {
            NodeList = new List<GraphNode>();
            // _rnd = new Random();
            for (int i = 0; i < nodes; i++)
            {
                NodeList.Add(new GraphNode(i + 1));
            }
        }

        //shlukovací efekt
        public void WriteToCsvWithCc(string filePath)
        {
            var map = new Dictionary<int, double>();
            var set = new HashSet<int>();
            var csv = new StringBuilder();
            csv.Append("Degree;Average Clustering Coefficient\n");
            foreach (var t in NodeList)
            {
                set.Add(t.Degree);
            }
            foreach (var item in set)
            {
                var temp = NodeList.Where(x => x.Degree == item).Select(x => x);
                map[item] = temp.Sum(x => x.ClusteringCoefficient) / temp.Count();
            }
            foreach (var item in map)
            {
                string newLine = $"{item.Key};{item.Value}\n";
                csv.Append(newLine);
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        public void PrintToCSV(string filePath)
        {
            var csv = new StringBuilder();
            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = i + 1; j < NodeList.Count; j++)
                {
                    if (i != j && NodeList[i].Neighbors.Contains(NodeList[j]))
                    {
                        csv.Append($"{NodeList[i].Id};{NodeList[j].Id}\n");
                    }
                }
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        public void CountNeighbors()
        {
            foreach (var node in NodeList)
            {
                node.Degree = node.Neighbors.Count;
            }
        }

        public double GetAverageDegree()
        {
            foreach (var node in NodeList)
            {
                AverageDegree += node.Degree;
            }
            AverageDegree /= NodeList.Count;

            return AverageDegree;
        }

        public void CalculateDegreeDistribution()
        {
            DegreeFrequency = new List<int>();
            DegreeDistribution = new List<double>();
            int maxDegree = NodeList.Max(node => node.Degree);

            for (int i = 0; i <= maxDegree; i++)
            {
                DegreeFrequency.Add(NodeList.Count(node => node.Degree == i));
                double probability = (double)DegreeFrequency[i] / NodeList.Count;
                DegreeDistribution.Add(probability);
            }
        }

        public double[,] ToMatrix()
        {
            var matrix = new double[NodeList.Count, NodeList.Count];

            for (int i = 0; i < NodeList.Count; i++)
            {
                foreach (var t in NodeList[i].Neighbors)
                {
                    int nodeIndex = NodeList.IndexOf(t);
                    matrix[i, nodeIndex] = 1;
                }
            }

            return matrix;
        }

        public int CountEdges()
        {
            int numberOfEdges = 0;

            foreach (var node in NodeList)
            {
                foreach (var t in node.Neighbors)
                {
                    numberOfEdges++;
                }
            }

            return numberOfEdges / 2;
        }

        public void CalculateClusteringCoefficient()
        {
            for (int i = 0; i < NodeList.Count; i++)
            {
                var subGraph = new List<GraphNode>();
                foreach (var node in NodeList[i].Neighbors)
                {
                    subGraph.Add(new GraphNode(node));
                }

                foreach (var t in subGraph)
                {
                    for (int k = t.Neighbors.Count - 1; k > -1; k--)
                    {
                        if (!subGraph.Contains(t.Neighbors[k]))
                        {
                            t.Neighbors.Remove(t.Neighbors[k]);
                        }
                    }
                }

                int edgesCount = subGraph.Sum(t => t.Neighbors.Count);

                float result = ((float)edgesCount) / (subGraph.Count * (subGraph.Count - 1));
                if (!float.IsNaN(result))
                {
                    NodeList[i].ClusteringCoefficient = result;
                }
                else
                {
                    NodeList[i].ClusteringCoefficient = 0;
                }
            }

            foreach (var node in NodeList)
            {
                AverageCC += node.ClusteringCoefficient;
            }
            AverageCC /= NodeList.Count;
        }

        public void PrintGraphInfoToFile(string filePath)
        {
            var text = new StringBuilder();
            text.Append("Graph is undirected and unweighted\n" +
                        $"Number of nodes: {NodeList.Count}\n" +
                        $"Number of edges: {CountEdges()} \n\n" +
                        "Degree frequency distribution: \n(");
            for (int i = 0; i < DegreeFrequency.Count; i++)
            {
                text.Append($"N{i},");
            }
            text.Length--;
            text.Append(")\n(");
            foreach (int t in DegreeFrequency)
            {
                text.Append($"{t},");
            }
            text.Length--;
            text.Append(")\n" +
                        "Probability distribution: \n(");
            var nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            foreach (double t in DegreeDistribution)
            {
                text.Append($"{Math.Round(t, 4, MidpointRounding.AwayFromZero).ToString(nfi)},");
            }
            text.Length--;
            text.Append(")\n" +
                        "Clustering coefficient: \n(");
            foreach (var node in NodeList)
            {
                text.Append($"{Math.Round(node.ClusteringCoefficient, 4, MidpointRounding.AwayFromZero).ToString(nfi)},");
            }
            text.Length--;
            text.Append(")\n\n");
            text.Append($"Average degree = {AverageDegree}\n");
            text.Append($"Average clustering coefficient = {AverageCC}\n");

            text.Append($"Graph diameter is {DiameterInfo[2]} from {NodeList[DiameterInfo[0]].Id} to {NodeList[DiameterInfo[1]].Id}");
            File.WriteAllText(filePath, text.ToString());
        }
    }
}
