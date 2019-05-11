using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Kmeans
{
    public class PokerHand
    {
        public double S1 { get; set; }
        public double C1 { get; set; }
        public double S2 { get; set; }
        public double C2 { get; set; }
        public double S3 { get; set; }
        public double C3 { get; set; }
        public double S4 { get; set; }
        public double C4 { get; set; }
        public double S5 { get; set; }
        public double C5 { get; set; }
        public double Class { get; set; }
        public int ClusterIndex { get; set; }

        public PokerHand()
        {
            ClusterIndex = 0;
        }

        public double[] ToArray()
        {
            return new[]
            {
                S1, C1, S2, C2, S3, C3, S4, C4, S5, C5, Class
            };
        }

    }
}
