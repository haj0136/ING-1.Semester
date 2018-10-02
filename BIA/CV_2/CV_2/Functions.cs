using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_2
{
    // schwefel funcition, ackley
    public static class Functions
    {
        public static double SphereFunction(params double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                sum += Math.Pow(x[i], 2);
            }
            return sum;
        }

        public static double RosenbrockFunction(params double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 1; i < d; i++)
            {
                sum += 100*(x[i+1]- Math.Pow(x[i],2)) + Math.Pow((1 - x[i]), 2);
            }
            return sum;
        }
        
        public static double AckleyFunction(double a = 20, double b = 0.2, double c = 2*Math.PI, params double[] x)
        {
            int d = x.Length;
            double sum1 = 0;
            double sum2 = 0;
            for (int i = 1; i <= d; i++)
            {
                sum1 += Math.Pow(x[i], 2);
                sum2 += Math.Cos(c * x[i]);
            }

            double term1 = -a * Math.Exp(-b * Math.Sqrt(sum1 / d));
            double term2 = -Math.Exp(sum2 / d);
            return term1 + term2 + a + Math.Exp(1);
        }

        public static double SchwefelFunction(params double [] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 1; i <= d; i++)
            {
                sum += x[i] * Math.Sin(Math.Sqrt(Math.Abs(x[i])));
            }
            return 418.9829 * d - sum;
        }

    }
}
