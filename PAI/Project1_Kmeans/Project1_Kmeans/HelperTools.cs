using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project1_Kmeans
{
    public static class HelperTools
    {
        public static double EuclideanDistance(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += Math.Pow(a[i] - b[i], 2);
            }

            return Math.Sqrt(result);
        }

        public static void WriteToCsvByCluster(string filePath, List<PokerHand> pokerHandList)
        {
            var csv = new StringBuilder();
            csv.Append("S1;C1\n");

            var clusters = pokerHandList.GroupBy(pokerHand => pokerHand.ClusterIndex).OrderBy(s => s.Key);

            foreach (var cluster in clusters)
            {
                var pokerHands = cluster.ToList();
                foreach (var pokerHand in pokerHands)
                {
                    var attributes = pokerHand.ToArray();
                    string newLine = $"{attributes[0]};{attributes[1]}\n";
                    csv.Append(newLine);

                }

                csv.Append("\n end of cluster \n");
            }


            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
