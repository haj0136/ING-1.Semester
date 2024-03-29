﻿using System;

namespace CV_4_WF.Functions
{
    class SchwefelFunction : AbstractFunction
    {
        public SchwefelFunction() : base(-500f, 500f)
        {

        }

        public override double getResult(params double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                sum += x[i] * Math.Sin(Math.Sqrt(Math.Abs(x[i])));
            }
            return 418.9829 * d - sum;
        }
    }
}
