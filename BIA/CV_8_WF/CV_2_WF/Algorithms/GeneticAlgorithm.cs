using CV_4_WF;
using CV_4_WF.Algorithms;
using CV_4_WF.Functions;
using CV_7_WF.Algorithms.GA;
using CV_7_WF.Algorithms.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CV_7_WF.Algorithms
{
    public class GeneticAlgorithm : IAlgorithm
    {
        private readonly List<City> cities;
        private readonly Random rnd;
        private readonly int populationSize;
        private readonly int pathLenght;
        /// <summary>
        /// Mutation Breeding Probability
        /// </summary>
        private readonly float MBP;
        private Population population;

        public GeneticAlgorithm()
        {
            cities = HelpTools.LoadCSVData("../../cities.csv");
            populationSize = 50;
            rnd = new Random();
            pathLenght = cities.Count;
            MBP = 0.1f;
        }

        public void SetStrategy(int strategyIndex)
        {
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILNumerics.Drawing.ILGroup plotCube, Panel panel1, List<ILNumerics.Drawing.ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            if (panel1 != null)
                panel1.Paint += Panel1_Paint;
            population = GenerateFirstPopulation();
            population.FindBestIndividual();
            if (panel1 != null)
                panel1.Refresh();

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < population.Individuals.Count; j++)
                {
                    int p1 = j;
                    int p2 = rnd.Next(population.Individuals.Count - 1);
                    while (p1 == p2)
                    {
                        p2 = rnd.Next(population.Individuals.Count - 1);
                    }

                    Individual offspring = Crossover(population.Individuals[p1], population.Individuals[p2]);

                    if (rnd.NextDouble() < MBP)
                    {
                        offspring.MakeMutation(rnd);
                    }

                    offspring.CalculateCostValue();

                    if (offspring.CostValue < population.Individuals[j].CostValue)
                    {
                        population.Individuals[j] = offspring;
                    }
                }

                population.FindBestIndividual();
                outPutTextBox.Text = population.BestIndividual.CostValue.ToString();
                if (panel1 != null)
                    panel1.Refresh();
            }

            if (panel1 != null)
                panel1.Paint -= Panel1_Paint;
            float[,] result = new float[1, 3];
            result[0, 2] = (float)population.BestIndividual.CostValue;
            return result;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            var graphics = e.Graphics;

            var brush = new SolidBrush(Color.Black);
            for (int i = 0; i < cities.Count; i++)
            {
                graphics.FillEllipse(brush, cities[i].X[0] * 2, cities[i].X[1] * 2, 10, 10);
            }

            var pen = new Pen(Color.Red, 2);

            for (int i = 0; i < population.BestIndividual.Path.Count - 1; i++)
            {
                var p1 = new Point(population.BestIndividual.Path[i].X[0] * 2 + 5, population.BestIndividual.Path[i].X[1] * 2 + 5);
                var p2 = new Point(population.BestIndividual.Path[i + 1].X[0] * 2 + 5, population.BestIndividual.Path[i + 1].X[1] * 2 + 5);

                graphics.DrawLine(pen, p1, p2);
            }

            graphics.DrawLine(pen, population.BestIndividual.Path[pathLenght - 1].X[0] * 2 + 5,
                population.BestIndividual.Path[pathLenght - 1].X[1] * 2 + 5, population.BestIndividual.Path[0].X[0] * 2 + 5,
                population.BestIndividual.Path[0].X[1] * 2 + 5);

        }

        private Population GenerateFirstPopulation()
        {
            var population = new Population();

            for (int i = 0; i < populationSize; i++)
            {
                population.Individuals.Add(new Individual());
                var usedCities = new List<City>();
                for (int j = 0; j < pathLenght; j++)
                {
                    population.Individuals[i].Path.Add(UniqueRandomCity(usedCities));
                }
                population.Individuals[i].CalculateCostValue();
            }

            return population;
        }

        private City UniqueRandomCity(List<City> usedCities)
        {
            City randomCity;
            var remainingCities = cities.Except(usedCities).ToList();
            randomCity = remainingCities[rnd.Next(remainingCities.Count - 1)];

            usedCities.Add(randomCity);

            return randomCity;
        }

        private Individual Crossover(Individual p1, Individual p2)
        {
            int bound1 = rnd.Next(pathLenght - 1);
            int bound2 = rnd.Next(pathLenght - 1);
            while (bound2 == bound1)
            {
                bound2 = rnd.Next(pathLenght - 1);
            }

            if (bound2 < bound1)
            {
                int temp = bound1;
                bound1 = bound2;
                bound2 = temp;
            }

            var offspring = new Individual(pathLenght);
            // 0 index to bound
            for (int i = 0; i < bound1; i++)
            {
                offspring.Path[i] = p1.Path[i];
            }
            // bound 2 to end
            for (int i = bound2; i < pathLenght; i++)
            {
                offspring.Path[i] = p1.Path[i];
            }
            // between bounds
            for (int i = bound1; i < bound2; i++)
            {
                for (int j = 0; j < p2.Path.Count; j++)
                {
                    if (!offspring.Path.Contains(p2.Path[j]))
                    {
                        offspring.Path[i] = p2.Path[j];
                        //break;
                    }
                }
            }
            offspring.CalculateCostValue();
            return offspring;
        }
    }
}
