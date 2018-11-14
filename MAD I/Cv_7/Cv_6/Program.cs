using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    class Program
    {
        // 1. k-NN classification
        //  nahodně rozdělit do 10 skupin po 15 (trenovací x testovací)
        // zkusit ruzna K
        static void Main(string[] args)
        {
            DataLoader dl = new DataLoader();
            List<Iris> irisList = dl.LoadData();

            int K = 3;

            var groups = MakeRandomGroups(irisList);



            for (int i = 0; i < groups.Count; i++)
            {
                for (int j = 0; j < groups[i].Count; j++)
                {
                    var distances = GetDistances(groups, i, j).ToList();
                    distances.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                    var topK = 
                    
                } 
            }


            
            
        }

        static double EuclideanDistance(double a, double b)
        {
            double result = Math.Pow(a - b, 2);

            return Math.Sqrt(result);
        }

        static List<List<Iris>> MakeRandomGroups(List<Iris> irises)
        {
            Random rnd = new Random();
            var groups = new List<List<Iris>>(10);
            var originalList = new List<Iris>(irises);

            for (int j = 0; j < groups.Capacity; j++)
            {
                groups.Add(new List<Iris>());
                for (int i = 0; i < 15; i++)
                {
                    var randomIndex = rnd.Next(originalList.Count);
                    groups[j].Add(originalList[randomIndex]);
                    originalList.RemoveAt(randomIndex);
                }
            }

            return groups;
        }

        static Dictionary<int, double> GetDistances(List<List<Iris>> groups, int actualGroupIndex, int actualIris)
        {
            var distances = new Dictionary<int, double>();
            for (int i = 0; i < groups.Count; i++)
            {
                if (i == actualGroupIndex)
                    continue;
                for (int j = 0; j < groups[i].Count; j++)
                {
                    distances.Add(groups[actualGroupIndex][actualIris].Id, HelperTools.EuclideanDistance(groups[i][j].ToArray(), groups[actualGroupIndex][actualIris].ToArray()));
                }
            }

            return distances;
        }

    }
    
}
