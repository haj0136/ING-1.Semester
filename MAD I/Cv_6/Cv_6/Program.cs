using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    class Program
    {
        // 1. Normální rozdělení vs. realné hodnoty
        // 2. shlukování (k-means)
        static void Main(string[] args)
        {
            DataLoader dl = new DataLoader();
            List<Iris> irisList = dl.LoadData();

            double x1 = 0;
            double x2 = 0;
            double x3 = 0;
            double x4 = 0;

            foreach (var item in irisList)
            {
                x1 += item.Petal_lenght;
                x2 += item.Petal_width;
                x3 += item.Sepal_lenght;
                x4 += item.Sepal_width;
            }

            double[] meanVector = { x1 / irisList.Count(), x2 / irisList.Count(), x3 / irisList.Count(), x4 / irisList.Count() };

            double totalVariance = 0;
            double[] variance = { 0, 0, 0, 0 };

            foreach (var item in irisList)
            {
                double[] temp = { item.Petal_lenght, item.Petal_width, item.Sepal_lenght, item.Sepal_width };
                totalVariance += Math.Pow(HelperTools.EuclideanDistance(temp, meanVector), 2);
                for (int i = 0; i < variance.Length; i++)
                {
                    variance[i] += Math.Pow(EuclideanDistance(temp[i], meanVector[i]), 2);
                }
            }

            for (int i = 0; i < variance.Length; i++)
            {
                variance[i] /= irisList.Count;
                Console.WriteLine($"Variance X{i + 1}: {variance[i]}");
            }
            totalVariance = totalVariance / irisList.Count;

            Console.WriteLine("Total variance = " + totalVariance + "\n");

            var g = irisList.GroupBy(iris => iris.Sepal_lenght);
            foreach (var item in g)
            {
                Console.WriteLine($"{item.Key} = {HelperTools.ProbabilityChance(irisList.Count, item.Count())}");
            }

            for (int i = 0; i < variance.Length; i++)
            {
                HelperTools.WriteToCSVDistribution($"statistics{i+1}.csv", variance[i], meanVector[i], meanVector[i] * 0.4, meanVector[i] * 1.6, irisList);
            }

            Console.WriteLine(HelperTools.NormalDistribution(variance[2], meanVector[2] , 4.3));


            // Clusters
            KMeans k_Means = new KMeans(irisList);

            var clusteredIrisList = k_Means.GetClusters();

            HelperTools.WriteToCSVByCluster("clusters.csv",clusteredIrisList);
            
        }

        static double EuclideanDistance(double a, double b)
        {
            double result = Math.Pow(a - b, 2);

            return Math.Sqrt(result);
        }

    }
    
}
