using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1_Kmeans
{
    public class KMeansParallel
    {
        private readonly List<PokerHand> _data;
        /// <summary>
        /// number of clusters
        /// </summary>
        private readonly int _k;
        /// <summary>
        /// 
        /// </summary>
        private readonly List<double[]> _clusters;

        public KMeansParallel(IEnumerable<PokerHand> dataSet)
        {
            _k = 11;
            _data = new List<PokerHand>(dataSet);
            _clusters = new List<double[]>();
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

            return _data;
        }

        private void InitializeCentroids()
        {
            var rnd = new Random();
            for (int i = 0; i < _k; i++)
            {
                _data[i].ClusterIndex = i;
            }

            Parallel.For(_k, _data.Count, index =>
            {
                _data[index].ClusterIndex = rnd.Next(0, _k);
            });
        }

        private void UpdateMeans()
        {
            var group = _data.GroupBy(pokerHand => pokerHand.ClusterIndex).OrderBy(s => s.Key);
            Parallel.ForEach(group, item =>
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
                lock (_clusters)
                {
                    _clusters.Add(means);
                }
            });
        }

        private bool UpdateClusters()
        {
            bool changed = false;

            Parallel.ForEach(_data, pokerHand =>
            {
                var distances = new double[_k];
                for (int j = 0; j < _k; j++)
                {
                    distances[j] = HelperTools.EuclideanDistance(pokerHand.ToArray(), _clusters[j]);
                }

                int newClusterId = Array.IndexOf(distances, distances.Min());
                if (newClusterId != pokerHand.ClusterIndex)
                {
                    changed = true;
                    pokerHand.ClusterIndex = newClusterId;
                }
            });


            return changed;
        }
    }
}
