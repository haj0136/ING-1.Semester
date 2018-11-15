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
        // nahodně rozdělit do 10 skupin po 15 (trenovací x testovací)
        // zkusit ruzna K
        static void Main(string[] args)
        {
            DataLoader dl = new DataLoader();
            List<Iris> irisList = dl.LoadData();

            int K = 3;

            var groups = MakeRandomGroups(irisList);



            for (int i = 0; i < groups.Count; i++)
            {
                double successRate = 0;

                for (int j = 0; j < groups[i].Count; j++)
                {
                    var distances = GetDistances(groups, i, j).ToList();
                    distances = distances.OrderBy(x => x.Value).Take(K).ToList();

                    var kNearestIrises = new List<Iris>();
                    foreach (var item in distances)
                    {
                        kNearestIrises.Add(irisList.Find(x => x.Id == item.Key));
                    }

                    var result = kNearestIrises.GroupBy(iris => iris.Iris_type).OrderByDescending(g => g.Count()).Select(g => g.Key).First();
                    groups[i][j].Iris_type = result;
                    if(result == irisList.Find(x => x.Id == groups[i][j].Id).Iris_type)
                    {
                        successRate++;
                    }

                }
                successRate /= 15;
                Console.WriteLine(successRate);
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
            var originalList = new List<Iris>();

            foreach (var iris in irises)
            {
                originalList.Add(new Iris(iris));
            }

            for (int j = 0; j < groups.Capacity; j++)
            {
                groups.Add(new List<Iris>());
                for (int i = 0; i < 15; i++)
                {
                    var randomIndex = rnd.Next(originalList.Count);
                    groups[j].Add(originalList[randomIndex]);
                    groups[j][i].Iris_type = null;
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
                    distances.Add(groups[i][j].Id, HelperTools.EuclideanDistance(groups[i][j].ToArray(), groups[actualGroupIndex][actualIris].ToArray()));
                }
            }

            return distances;
        }

    }
    
}
