using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Kmeans
{
    public class Wine
    {
        public double FixedAcidity { get; set; }
        public double VolatileAcidity { get; set; }
        public double CitricAcid { get; set; }
        public double ResidualSugar { get; set; }
        public double Chlorides { get; set; }
        public double FreeSulfurDioxide { get; set; }
        public double TotalSulfurDioxide { get; set; }
        public double Density { get; set; }
        public double Ph { get; set; }
        public double Sulphates { get; set; }
        public double Alcohol { get; set; }
        public double Quality { get; set; }
        public int ClusterIndex { get; set; }

        public Wine()
        {
            ClusterIndex = 0;
        }

        public double[] ToArray()
        {
            return new[]
            {
                FixedAcidity, VolatileAcidity, CitricAcid, ResidualSugar, Chlorides, FreeSulfurDioxide,
                TotalSulfurDioxide, Density, Ph, Sulphates, Alcohol, Quality
            };
        }
    }
}
