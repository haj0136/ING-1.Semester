using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_3_WF.Functions
{
    class SphereFunction : AbstractFunction
    {
        public SphereFunction() : base( -5.12f * 3, 5.12f * 3)
        {

        }

        public override double getResult(params double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                sum += Math.Pow(x[i], 2);
            }
            return sum;
        }
    }
}
