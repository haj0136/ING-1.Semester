using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Kmeans
{
    public class KMeans
    {
        private List<PokerHand> data;
        /// <summary>
        /// number of clusters
        /// </summary>
        private readonly int _k;
        /// <summary>
        /// values in array: Sepal length; Sepal width; Petal length; Petal width;
        /// </summary>
        private List<double[]> clusters;

        public KMeans(IEnumerable<PokerHand> dataSet)
        {
            _k = 11;
            data = new List<PokerHand>(dataSet);
            clusters = new List<double[]>();
        }

        public List<PokerHand> GetClusters()
        {
            bool changed = true;
            int maxIterations = _k * 20;

            InitializeCentroids();

            for (int i = 0; i < maxIterations; i++)
            {
                UpdateMeans();
                changed = UpdateClusters();

                if (changed == false)
                    break;
            }

            return data;
        }

        private void InitializeCentroids()
        {
            var rnd = new Random();
            for (int i = 0; i < _k; i++)
            {
                data[i].ClusterIndex = i;
            }
            for (int i = _k; i < data.Count; i++)
            {
                data[i].ClusterIndex = rnd.Next(0, _k);
            }
        }

        private void UpdateMeans()
        {
            var group = data.GroupBy(pokerHand => pokerHand.ClusterIndex).OrderBy(s => s.Key);
            foreach (var item in group)
            {
                var means = new double[11];
                means[0] = item.ToList().Sum(pokerHand => pokerHand.S1) / item.Count();
                means[1] = item.ToList().Sum(pokerHand => pokerHand.C1) / item.Count();
                means[2] = item.ToList().Sum(pokerHand => pokerHand.S2) / item.Count();
                means[3] = item.ToList().Sum(pokerHand => pokerHand.C2) / item.Count();
                means[4] = item.ToList().Sum(pokerHand => pokerHand.S3) / item.Count();
                means[5] = item.ToList().Sum(pokerHand => pokerHand.C3) / item.Count();
                means[6] = item.ToList().Sum(pokerHand => pokerHand.S4) / item.Count();
                means[7] = item.ToList().Sum(pokerHand => pokerHand.C4) / item.Count();
                means[8] = item.ToList().Sum(pokerHand => pokerHand.S5) / item.Count();
                means[9] = item.ToList().Sum(pokerHand => pokerHand.S5) / item.Count();
                means[10] = item.ToList().Sum(pokerHand => pokerHand.Class) / item.Count();
                clusters.Add(means);
            }
        }

        private bool UpdateClusters()
        {
            bool changed = false;
            var distances = new double[_k];

            foreach (var pokerHand in data)
            {
                for (int j = 0; j < _k; j++)
                {
                    distances[j] = HelperTools.EuclideanDistance(pokerHand.ToArray(), clusters[j]);
                }

                int newClusterId = Array.IndexOf(distances, distances.Min());
                if (newClusterId != pokerHand.ClusterIndex)
                {
                    changed = true;
                    pokerHand.ClusterIndex = newClusterId;
                }
            }

            return changed;
        }
    }
}
