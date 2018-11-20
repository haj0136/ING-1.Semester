using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_7_WF.Algorithms.GA
{
    public class Population
    {
        public List<Individual> Individuals { get; set; }

        public Population()
        {
            Individuals = new List<Individual>();
        }

        public Individual GetBestIndividual()
        {
            var bestIndividual = new Individual
            {
                CostValue = int.MaxValue
            };

            for (int i = 0; i < Individuals.Count; i++)
            {
                if (Individuals[i].CostValue < bestIndividual.CostValue)
                {
                    bestIndividual.Path = Individuals[i].Path;
                    bestIndividual.CostValue = Individuals[i].CostValue;
                }
            }

            return bestIndividual;
        }

    }
}
