using CV_4_WF;
using CV_7_WF.Algorithms.Shared;
using System.Collections.Generic;

namespace CV_7_WF.AntColony
{
    public class Ant
    {
        public List<City> Path { get; set; }
        public double CostValue { get; set; }

        public Ant()
        {
            Path = new List<City>();
        }

        public Ant(int pathLenght)
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
    }
}
