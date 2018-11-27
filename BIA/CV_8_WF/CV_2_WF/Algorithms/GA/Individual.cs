using CV_4_WF;
using CV_7_WF.Algorithms.Shared;
using System;
using System.Collections.Generic;

namespace CV_7_WF.Algorithms.GA
{
    public class Individual
    {

        public List<City> Path { get; set; }
        public double CostValue { get; set; }

        public Individual()
        {
            Path = new List<City>();
        }
        public Individual(int pathLenght)
        {
            Path = new List<City>();
            for (int i = 0; i < pathLenght; i++)
            {
                Path.Add(new City());
            }
        }

        public void CalculateCostValue()
        {
            CostValue = 0;
            for (int i = 0; i < Path.Count - 1; i++)
            {
                CostValue += HelpTools.EuclideanDistance(Path[i].ToIntArray(), Path[i + 1].ToIntArray());
            }
            CostValue += HelpTools.EuclideanDistance(Path[Path.Count - 1].ToIntArray(), Path[0].ToIntArray());
        }

        public void MakeMutation(Random rnd)
        {
            int c1 = rnd.Next(Path.Count - 1);
            int c2 = rnd.Next(Path.Count - 1);
            while (c1 == c2)
            {
                c2 = rnd.Next(Path.Count - 1);
            }

            City temp = Path[c1];
            Path[c1] = Path[c2];
            Path[c2] = temp;
        }
    }
}
