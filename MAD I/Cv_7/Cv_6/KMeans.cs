using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    public class KMeans
    {
        private List<Iris> data;
        /// <summary>
        /// number of clusters
        /// </summary>
        private readonly int k;
        /// <summary>
        /// values in array: Sepal lenght; Sepal width; Petal lenght; Petal width;
        /// </summary>
        private List<double[]> clusters;

        public KMeans(List<Iris> dataset)
        {
            k = 10;
            data = new List<Iris>(dataset);
            clusters = new List<double[]>();
        }

        public List<Iris> GetClusters()
        {
            bool changed = true;
            int maxIterations = k * 20;

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
            Random rnd = new Random();
            for (int i = 0; i < k; i++)
            {
                data[i].ClusterIndex = i;
            }
            for (int i = k; i < data.Count; i++)
            {
                data[i].ClusterIndex = rnd.Next(0, k);
            }
        }

        private void UpdateMeans()
        {
            var group = data.GroupBy(iris => iris.ClusterIndex).OrderBy(s => s.Key);
            foreach (var item in group)
            {
                var means = new double[4];
                means[0] = item.ToList().Sum(iris => iris.Sepal_lenght) / item.Count();
                means[1] = item.ToList().Sum(iris => iris.Sepal_width) / item.Count();
                //means[2] = item.ToList().Sum(iris => iris.Petal_lenght) / item.Count();
                //means[3] = item.ToList().Sum(iris => iris.Petal_width) / item.Count();
                clusters.Add(means);
            }
        }

        private bool UpdateClusters()
        {
            bool changed = false;
            double[] distances = new double[k];

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    distances[j] = HelperTools.EuclideanDistance(data[i].ToArray(), clusters[j]);
                }

                int newClusterId = Array.IndexOf(distances, distances.Min());
                if (newClusterId != data[i].ClusterIndex)
                {
                    changed = true;
                    data[i].ClusterIndex = newClusterId;
                }
            }

            return changed;
        }
    }
}
