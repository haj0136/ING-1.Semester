using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cv_6
{
    public static class HelperTools
    {
        public static void WriteToCSVDistribution(string filePath, double variance, double mean, double min, double max, List<Iris> irisList)
        {
            var csv = new StringBuilder();
            csv.Append("x;Fx;;x;Fx\n");

            int i = 0;
            for (double f = min; f <= max; f += 0.1d)
            {
                // normal distribution
                f = Math.Round(f, 1);
                var newLine = string.Format("{0};{1}", f, NormalDistribution(variance, mean, f));
                csv.Append(newLine);

                //probabilty
                var g = irisList.GroupBy(iris => iris.Sepal_lenght).ToList();
                if (g.Count > i)
                {
                    newLine = $";;{g[i].Key};{HelperTools.ProbabilityChance(irisList.Count, g[i].Count())}\n";
                }
                else
                {
                    newLine = "\n";
                }
                csv.Append(newLine);
                i++;
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        public static double NormalDistribution(double variance, double mean, double x)
        {
            double result = 0;
            result = (1 / Math.Sqrt(2 * Math.PI * variance)) * Math.Exp(-(Math.Pow(x - mean, 2) / 2 * variance));
            return result;
        }
        public static double ProbabilityChance(int totalCount, int xCount)
        {
            double result = 0;
            result = xCount / ((float)totalCount / 100);
            return result / 100;
        }

        public static double EuclideanDistance(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += Math.Pow(a[i] - b[i], 2);
            }

            return Math.Sqrt(result);
        }

        public static void WriteToCSVByCluster(string filePath, List<Iris> irisList)
        {
            var csv = new StringBuilder();
            csv.Append("Sepal lenght;Sepal width;Petal lenght;Petal width\n");

            var clusters = irisList.GroupBy(iris => iris.ClusterIndex).OrderBy(s => s.Key);

            foreach (var cluster in clusters)
            {
                var irises = cluster.ToList();
                foreach (var iris in irises)
                {
                    var attributes = iris.ToArray();
                    var newLine = string.Format("{0};{1}\n", attributes[0], attributes[1]);
                    csv.Append(newLine);

                }

                csv.Append("\n end of cluster \n");
            }


            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
