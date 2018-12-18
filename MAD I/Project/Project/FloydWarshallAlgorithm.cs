namespace Project
{
    public static class FloydWarshallAlgorithm
    {
        public static double[,] GetResult(double[,] d)
        {
            int length = d.GetLength(0);
            var distance = new double[length, length];
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    if (d[i, j] == 0 && i != j)
                    {
                        distance[i, j] = double.MaxValue;
                    }
                    else
                    {
                        distance[i, j] = d[i, j];
                    }
                }

            for (int k = 0; k < d.GetLength(0); k++)
            {
                for (int i = 0; i < d.GetLength(0); i++)
                {
                    for (int j = 0; j < d.GetLength(0); j++)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                        {
                            distance[i, j] = distance[i, k] + distance[k, j];
                        }
                    }
                }
            }
            return distance;
        }
    }
}
