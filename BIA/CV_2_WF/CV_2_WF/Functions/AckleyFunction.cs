using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_2_WF.Functions
{
    class AckleyFunction : AbstractFunction
    {

        private double a = 20;
        private double b = 0.2;
        private double c = 2 * Math.PI;
        public AckleyFunction() : base(-32.768f, 32.768f)
        {

        }

        public override double getResult(params double[] x)
        {
            int d = x.Length;
            double sum1 = 0;
            double sum2 = 0;
            for (int i = 0; i < d; i++)
            {
                sum1 += Math.Pow(x[i], 2);
                sum2 += Math.Cos(c * x[i]);
            }

            double term1 = -a * Math.Exp(-b * Math.Sqrt(sum1 / d));
            double term2 = -Math.Exp(sum2 / d);
            return term1 + term2 + a + Math.Exp(1);
        }
    }
}
