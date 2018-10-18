using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_5
{
    public class Graph
    {
        public List<GraphNode> NodeList { get; }

        public double AverageDegree { get; set; }

        public double AverageCC { get; set; }

        public Graph(int nodes)
        {
            NodeList = new List<GraphNode>();
            for (int i = 0; i < nodes; i++)
            {
                NodeList.Add(new GraphNode(i + 1));
            }
        }

        public void WriteToCSV(string filePath)
        {
            var csv = new StringBuilder();
            csv.Append("Id;Degree;Clustering Coeficient\n");
            for (int i = 0; i < NodeList.Count; i++)
            {
                var newLine = string.Format("{0};{1};{2}\n", NodeList[i].Id, NodeList[i].Degree, NodeList[i].ClusteringCoeficient);
                csv.Append(newLine);
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
