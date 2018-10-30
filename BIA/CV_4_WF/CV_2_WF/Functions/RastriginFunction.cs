using System;

namespace CV_4_WF.Functions
{
    class RastriginFunction : AbstractFunction
    {
        public RastriginFunction() : base(-5.12f, 5.12f)
        {

        }

        public override double getResult(params double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                sum += Math.Pow(x[i], 2) - 10 * Math.Cos(2 * Math.PI * x[i]);
            }

            return 10 * d + sum;
        }
    }
}
