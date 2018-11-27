using CV_7_WF.Algorithms.Shared;
using System.Collections.Generic;

namespace CV_7_WF.AntColony
{
    public class Population
    {

        public List<Ant> Ants { get; set; }

        public Ant BestAnt { get; set; }

        public Population()
        {
            Ants = new List<Ant>();
        }

        public void FindBestIndividual()
        {
            var bestIndividual = new Ant
            {
                CostValue = int.MaxValue
            };

            for (int i = 0; i < Ants.Count; i++)
            {
                if (Ants[i].CostValue < bestIndividual.CostValue)
                {
                    bestIndividual.Path = new List<City>(Ants[i].Path);
                    bestIndividual.CostValue = Ants[i].CostValue;
                }
            }

            BestAnt = bestIndividual;
        }
    }
}
