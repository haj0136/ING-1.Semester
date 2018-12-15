using System;

namespace CV_8_WF.Functions
{
    public class F2
    {
        public float MinX { get; }
        public float MaxX { get; }
        public float MinY { get; }
        public float MaxY { get; }

        public F2()
        {
            MinX = -55;
            MaxX = 55;
            MinY = -7;
            MaxY = 1;

        }
        public float GetResult(double X)
        {
            return (float)-Math.Pow(X - 2, 2);
        }
    }
}
