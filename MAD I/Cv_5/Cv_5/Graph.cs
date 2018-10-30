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
    }
}
