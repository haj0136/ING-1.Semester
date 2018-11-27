using CV_4_WF;
using CV_4_WF.Algorithms;
using CV_4_WF.Functions;
using CV_7_WF.Algorithms.Shared;
using CV_7_WF.AntColony;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CV_7_WF.Algorithms
{
    public class AntColony : IAlgorithm
    {
        private readonly List<City> cities;
        private readonly Random rnd;
        private readonly int populationSize;
        private readonly int pathLenght;
        private const float alpha = 3f; //0.2
        private const float beta = 2f; //0.6
        private const float rho = 0.01f; // 0.3
        private float t0 = 0.2f;
        private Dictionary<(City, City), float> pheromoneTable;
        private Ant bestResult;

        public AntColony()
        {
            cities = HelpTools.LoadCSVData("../../cities.csv");
            populationSize = cities.Count;
            rnd = new Random();
            pathLenght = cities.Count;
        }

        public void SetStrategy(int strategyIndex)
        {
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILNumerics.Drawing.ILGroup plotCube, Panel panel1, List<ILNumerics.Drawing.ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            bestResult = new Ant()
            {
                CostValue = double.MaxValue,
            };

            pheromoneTable = GetNewPheromoneTable();

            if (panel1 != null)
                panel1.Paint += Panel1_Paint;

            for (int i = 0; i < iterations; i++)
            {
                var population = GenerateNewPopulation();
                for (int k = 0; k < population.Ants.Count; k++)
                {
                    for (int l = 0; l < pathLenght - 1; l++)
                    {
                        var availableCities = cities.Except(population.Ants[k].Path).ToList();
                        var probabilities = GetProbabilisticValues(availableCities, population.Ants[k].Path[population.Ants[k].Path.Count - 1]);
                        population.Ants[k].Path.Add(SelectNextCity(probabilities));
                    }
                        population.Ants[k].CalculateCostValue();
                }
                UpdatePheromoneTable(population);
                population.FindBestIndividual();
                if (population.BestAnt.CostValue < bestResult.CostValue)
                {
                    bestResult = population.BestAnt;
                }
                outPutTextBox.Text = bestResult.CostValue.ToString();
                if (panel1 != null)
                    panel1.Refresh();
                Console.WriteLine($"iteration {i + 1} done");
            }

            if (panel1 != null)
                panel1.Paint -= Panel1_Paint;
            return null;
        }

        private void UpdatePheromoneTable(Population population)
        {
            for (int i = 0; i < pheromoneTable.Count; i++)
            {
                pheromoneTable[pheromoneTable.ElementAt(i).Key] *= 1 - rho;
            }

            for (int i = 0; i < population.Ants.Count; i++)
            {
                for (int j = 0; j < population.Ants[i].Path.Count - 1; j++)
                {
                    pheromoneTable[(population.Ants[i].Path[j], population.Ants[i].Path[j + 1])] += (1 / (float)population.Ants[i].CostValue) * 1f;
                }
                pheromoneTable[(population.Ants[i].Path[population.Ants[i].Path.Count - 1], population.Ants[i].Path[0])] += (1 / (float)population.Ants[i].CostValue) * 1f;
            }
        }

        private City SelectNextCity(Dictionary<(City, City), float> probabilities)
        {
            var cumulativeDistribution = new Dictionary<(City, City), float>();
            float sum = 0;
            foreach (var probability in probabilities)
            {
                sum += probability.Value;
                cumulativeDistribution.Add(probability.Key, sum);
            }
            cumulativeDistribution[cumulativeDistribution.Last().Key] = 1;

            var randomDouble = rnd.NextDouble();
            foreach (var item in cumulativeDistribution)
            {
                if (randomDouble <= item.Value)
                {
                    return item.Key.Item2;
                }
            }

            return null;
        }

        private Population GenerateNewPopulation()
        {
            var population = new Population();
            var notUsedCities = new List<City>(cities);

            for (int i = 0; i < cities.Count; i++)
            {
                int randomIndex = rnd.Next(notUsedCities.Count - 1);
                population.Ants.Add(new Ant());
                population.Ants[i].Path.Add(notUsedCities[randomIndex]);
                notUsedCities.RemoveAt(randomIndex);
            }

            return population;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            var graphics = e.Graphics;

            var brush = new SolidBrush(Color.Black);
            for (
                int i = 0; i < cities.Count; i++)
            {
                graphics.FillEllipse(brush, cities[i].X[0] * 2, cities[i].X[1] * 2, 10, 10);
            }

            var pen = new Pen(Color.Red, 2);

            for (int i = 0; i < bestResult.Path.Count - 1; i++)
            {
                var p1 = new Point(bestResult.Path[i].X[0] * 2 + 5, bestResult.Path[i].X[1] * 2 + 5);
                var p2 = new Point(bestResult.Path[i + 1].X[0] * 2 + 5, bestResult.Path[i + 1].X[1] * 2 + 5);

                graphics.DrawLine(pen, p1, p2);
            }

            graphics.DrawLine(pen, bestResult.Path[pathLenght - 1].X[0] * 2 + 5,
                bestResult.Path[pathLenght - 1].X[1] * 2 + 5, bestResult.Path[0].X[0] * 2 + 5,
                bestResult.Path[0].X[1] * 2 + 5);

        }

        private Dictionary<(City, City), float> GetNewPheromoneTable()
        {
            var pheromoneTable = new Dictionary<(City, City), float>();
            for (int i = 0; i < cities.Count; i++)
            {
                for (int j = 0; j < cities.Count; j++)
                {
                    if (i != j)
                    {
                        pheromoneTable.Add((cities[i], cities[j]), t0);
                    }
                }
            }

            return pheromoneTable;
        }

        private Dictionary<(City, City), float> GetProbabilisticValues(List<City> availableCities, City actualCity)
        {
            var result = new Dictionary<(City, City), float>();
            double upperPart, lowerPart;
            for (int i = 0; i < availableCities.Count; i++)
            {
                upperPart = Math.Pow(pheromoneTable[(actualCity, availableCities[i])], alpha) * Math.Pow(1 / HelpTools.EuclideanDistance(actualCity.ToIntArray(), availableCities[i].ToIntArray()), beta);
                lowerPart = 0;

                for (int j = 0; j < availableCities.Count; j++)
                {
                    lowerPart += Math.Pow(pheromoneTable[(actualCity, availableCities[j])], alpha) * Math.Pow(1 / HelpTools.EuclideanDistance(actualCity.ToIntArray(), availableCities[j].ToIntArray()), beta);
                }

                result.Add((actualCity, availableCities[i]), (float)(upperPart / lowerPart));
            }


            return result;
        }
    }
}
