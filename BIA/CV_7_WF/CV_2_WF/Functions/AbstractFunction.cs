namespace CV_4_WF.Functions
{
    public abstract class AbstractFunction
    {
        public float MinX { get; }
        public float MaxX { get; }
        public float MinY { get; }
        public float MaxY { get; }

        public AbstractFunction(float min, float max)
        {
            MinX = min;
            MaxX = max;
            MinY = min;
            MaxY = max;

        }
        public AbstractFunction(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public abstract double getResult(params double[] values);
    }
}
