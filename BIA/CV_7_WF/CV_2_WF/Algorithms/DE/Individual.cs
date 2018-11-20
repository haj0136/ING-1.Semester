using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.Algorithms.DE
{
    public class Individual
    {
        public List<float> X { get; set; }

        public Individual()
        {
            X = new List<float>();
        }

        public Individual(Individual individual)
        {
            X = new List<float>(individual.X);
        }

        public float[] ToFloatArray()
        {
            return X.ToArray();
        }

        public float[,] To2dArray()
        {
            var vectorList = new List<float[]>
            {
                this.ToFloatArray()
            };
            return HelpTools.CreateRectangularArray(vectorList);
        }
    }
}
