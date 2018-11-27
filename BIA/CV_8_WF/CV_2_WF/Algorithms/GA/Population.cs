using CV_7_WF.Algorithms.Shared;
using System.Collections.Generic;

namespace CV_7_WF.Algorithms.GA
{
    public class Population
    {
        public List<Individual> Individuals { get; set; }
        public Individual BestIndividual { get; set; }

        public Population()
        {
            Individuals = new List<Individual>();
        }

        public void FindBestIndividual()
        {
            var bestIndividual = new Individual
            {
                CostValue = int.MaxValue
            };

            for (int i = 0; i < Individuals.Count; i++)
            {
                if (Individuals[i].CostValue < bestIndividual.CostValue)
                {
                    bestIndividual.Path = new List<City>(Individuals[i].Path);
                    bestIndividual.CostValue = Individuals[i].CostValue;
                }
            }

            BestIndividual = bestIndividual;
        }

    }
}
