using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.Algorithms.DE
{
    public class Population
    {
        public List<Individual> Individuals { get; set; }

        public Population()
        {
            Individuals = new List<Individual>();
        }

        public float[,] To2dArray()
        {
            var vectorList = new List<float[]>();
            foreach (var individual in Individuals)
            {
                vectorList.Add(individual.ToFloatArray());
            }
            return HelpTools.CreateRectangularArray(vectorList);
        }
    }
}
