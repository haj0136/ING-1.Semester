using CV_4_WF.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF
{
    public static class PopulationGenerator
    {
        private static Random rnd = new Random();
        public static float[,] GeneratePopulation(int sizeOfPopulation, AbstractFunction testFunction, params float[] mainNode)
        {
            List<float[]> result = new List<float[]>();
            for (int i = 0; i < sizeOfPopulation; i++)
            {

                double u1 = rnd.NextDouble();
                double u2 = rnd.NextDouble();

                double z2 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                double z1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

                z1 += mainNode[0];
                z2 += mainNode[1];

                float[] node = { (float)z1, (float)z2, (float)testFunction.getResult(z1, z2) };

                result.Add(node);
            }

            return HelpTools.CreateRectangularArray(result);
        }
    }
}
