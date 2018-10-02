using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_2
{
    class Hill_climbing
    {

        public double Start(double[] startingPoint)
        {

            double result = Functions.SphereFunction(startingPoint);



            return result;
        }
    }
}
