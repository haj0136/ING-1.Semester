using CV_4_WF.Functions;
using System;

namespace CV_4_WF
{
    class RosenbrockFunction : AbstractFunction
    {
        public RosenbrockFunction() : base(-10f, 10f)
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
