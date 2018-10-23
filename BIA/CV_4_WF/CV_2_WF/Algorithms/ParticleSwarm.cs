using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CV_4_WF.ParticleSwarm;
using CV_4_WF.Functions;
using ILNumerics.Drawing;

namespace CV_4_WF.Algorithms
{
    public class ParticleSwarm : IAlgorithm
    {
        private readonly Random rnd;
        private readonly int populationSize;
        private readonly int dimensions;
        private readonly float c1, c2;

        public ParticleSwarm()
        {
            rnd = new Random();
            populationSize = 10;
            dimensions = 3;
            c1 = c2 = 0.2f;
        }
        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            ILPoints points = null;
            Population population;

            population = GenerateFirstPopulation(testFunction);

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < population.Particles.Count; j++)
                {

                }
            }

            return null;
        }

        private Population GenerateFirstPopulation(AbstractFunction testFunction)
        {
            var population = new Population();

            for (int i = 0; i < populationSize; i++)
            {
                population.Particles.Add(new Particle());
                for (int j = 0; j < dimensions - 1; j++)
                {
                    population.Particles[i].X.Add((float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX);
                }
                population.Particles[i].X.Add((float)testFunction.getResult(Array.ConvertAll(population.Particles[i].X.ToArray(), x => (double)x)));
                population.Particles[i].PBest = new List<float>(population.Particles[i].X);
                if(population.GBest == null || population.GBest[dimensions - 1] > population.Particles[i].X[dimensions - 1])
                {
                    population.GBest = new List<float>(population.Particles[i].X);
                }
            }

            return population;
        }
    }
}
