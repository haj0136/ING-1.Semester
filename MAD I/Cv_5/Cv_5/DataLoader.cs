using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_5
{
    public class DataLoader
    {
        public Graph LoadData(Graph g)
        {
            List<string> rows = LoadCsvFile("KarateClub.csv");
            foreach (string row in rows)
            {
                List<string> attributes = row.Split(';').ToList();
                g.NodeList[Int32.Parse(attributes[0]) - 1].Neighbours.Add(g.NodeList[Int32.Parse(attributes[1]) - 1]);
                g.NodeList[Int32.Parse(attributes[1]) - 1].Neighbours.Add(g.NodeList[Int32.Parse(attributes[0]) - 1]);
            }
            return g;
        }

        public int[,] LoadDataToMatrix(int[,] matrix)
        {
            List<string> rows = LoadCsvFile("KarateClub.csv");
            foreach (string row in rows)
            {
                List<string> attributes = row.Split(';').ToList();
                matrix[Int32.Parse(attributes[0]) - 1, Int32.Parse(attributes[1]) - 1] = 1;
                matrix[Int32.Parse(attributes[1]) - 1, Int32.Parse(attributes[0]) - 1] = 1;
            }
            return matrix;
        }

        private List<string> LoadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> searchList = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }

            return searchList;
        }
    }
}
