using CV_4_WF.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_8_WF.Functions
{
    public class F1
    {
        public float MinX { get; }
        public float MaxX { get; }
        public float MinY { get; }
        public float MaxY { get; }

        public F1()
        {
            MinX = -55;
            MaxX = 55;
            MinY = -7;
            MaxY = 1;

        }
        public float GetResult(double X)
        {
            return (float)-Math.Pow(X, 2);
        }
    }
}
