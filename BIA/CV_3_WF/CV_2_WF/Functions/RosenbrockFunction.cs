using CV_3_WF.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_3_WF
{
    class RosenbrockFunction : AbstractFunction
    {
        public RosenbrockFunction() : base(-5f, 10f)
        {

        }

        public override double getResult(params double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d - 1; i++)
            {
                sum += 100 * Math.Pow(x[i + 1] - Math.Pow(x[i], 2), 2) + Math.Pow(x[i] - 1, 2);
            }
            return sum;
        }
    }
}
