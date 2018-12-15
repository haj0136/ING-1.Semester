using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_10
{
    class DataLoader
    {
        public Graph LoadData()
        {
            var dataSet = new Graph();
            List<string> rows = LoadCsvFile("../../Dataset.txt");
            var verticesInfo = rows[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int verticesCount = int.Parse(verticesInfo[1]);
            int verticesTypeChangeCount = int.Parse(verticesInfo[2]);

            var vertices = rows.Skip(1).Take(verticesCount).ToList();
            foreach (var vertex in vertices)
            {
                var vertexInfo = vertex.Split(" \"".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var newNode = new GraphNode(int.Parse(vertexInfo[0]))
                {
                    Name = string.Join(" ",vertexInfo.Skip(1).ToArray())
                };
                if(verticesTypeChangeCount > 0)
                {
                    newNode.Type = GraphNode.NodeType.Actor;
                    verticesTypeChangeCount--;
                } else
                {
                    newNode.Type = GraphNode.NodeType.Movie;
                }
                dataSet.NodeList.Add(newNode);
            }

            int edgesIndex = rows.FindIndex(row => row.Contains("Edges"));
            var edges = rows.Skip(edgesIndex + 1).ToList();
            foreach (var edge in edges)
            {
                var edgeInfo = edge.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                dataSet.NodeList[int.Parse(edgeInfo[0]) - 1].Neighbours.Add(dataSet.NodeList[int.Parse(edgeInfo[1]) - 1]);
                dataSet.NodeList[int.Parse(edgeInfo[1]) - 1].Neighbours.Add(dataSet.NodeList[int.Parse(edgeInfo[0]) - 1]);
            }

            return dataSet;
        }

        private List<string> LoadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath), Encoding.UTF8);
            var searchList = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }

            return searchList;
        }
    }
}
