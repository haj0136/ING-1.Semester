using CV_4_WF;
using CV_4_WF.Algorithms;
using CV_4_WF.Algorithms.DE;
using CV_4_WF.Functions;
using ILNumerics.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CV_8_WF.Algorithms
{
    public class EvolutionaryStrategies : IAlgorithm
    {
        private readonly int populationSize;
        private readonly int dimensionsCount;
        private readonly Random rnd;
        private ILPoints populationPoints = null;

        public EvolutionaryStrategies()
        {
            dimensionsCount = 3;
            populationSize = 20;
            rnd = new Random();
        }

        public void SetStrategy(int strategyIndex)
        {
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            Population population;
            population = GenerateFirstPopulation(testFunction);

            double sigma = 1;

            for (int i = 0; i < iterations; i++)
            {
                int acceptedOffsprings = 0;
                for (int j = 0; j < populationSize; j++)
                {
                    //create new offspring
                    var offspring = new Individual();
                    for (int k = 0; k < population.Individuals[j].X.Count - 1; k++)
                    {
                        offspring.X.Add(population.Individuals[j].X[k] + (float)HelpTools.GetRandomGaussianVariable(rnd, sigma));
                        if (offspring.X[k] < testFunction.MinX || offspring.X[k] > testFunction.MaxX)
                        {
                            offspring.X[k] = population.Individuals[j].X[k];
                        }
                    }
                    // get cost value
                    offspring.X.Add((float)testFunction.getResult(Array.ConvertAll(offspring.X.ToArray(), x => (double)x)));


                    if (offspring.X[dimensionsCount - 1] < population.Individuals[j].X[dimensionsCount - 1])
                    {
                        population.Individuals[j] = offspring;
                        acceptedOffsprings++;
                    }
                }
                if ((acceptedOffsprings / populationSize) > (1 / 5))
                {
                    sigma *= 1.5;
                }
                else if ((acceptedOffsprings / populationSize) < (1 / 5))
                {
                    sigma *= Math.Pow(1.5, -1 / 4);
                }
                if (panel1 != null)
                    PaintPopulation(panel1, population.To2dArray(), plotCube, listOfPoints);
            }
            Individual bestIndividual = population.Individuals[0];
            foreach (var individual in population.Individuals)
            {
                if (individual.X[dimensionsCount - 1] < bestIndividual.X[dimensionsCount - 1])
                {
                    bestIndividual = individual;
                }
            }
            if (panel1 != null)
                PaintBestNode(bestIndividual.To2dArray(), plotCube, panel1, listOfPoints);
            PrintBestNode(outPutTextBox, bestIndividual.X);
            return bestIndividual.To2dArray();
        }

        private Population GenerateFirstPopulation(AbstractFunction testFunction)
        {
            var population = new Population();

            for (int i = 0; i < populationSize; i++)
            {
                population.Individuals.Add(new Individual());
                for (int j = 0; j < dimensionsCount - 1; j++)
                {
                    population.Individuals[i].X.Add((float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX);
                }
                population.Individuals[i].X.Add((float)testFunction.getResult(Array.ConvertAll(population.Individuals[i].X.ToArray(), x => (double)x)));
            }

            return population;
        }

        private void PrintBestNode(TextBox bestNodeTextBox, List<float> bestNode)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < bestNode.Count; i++)
            {
                sb.Append($" x{i + 1} = {bestNode[i]}");
            }

            bestNodeTextBox.Text = sb.ToString();

        }

        private void PaintPopulation(Panel panel1, float[,] population, ILGroup plotCube, List<ILPoints> listOfPoints)
        {
            if (panel1 != null)
            {
                Thread.Sleep(50);
                if (populationPoints != null)
                {
                    plotCube.Remove(populationPoints);
                    populationPoints.Dispose();
                    panel1.Refresh();
                    listOfPoints.Remove(populationPoints);
                }

                float[,]  renderNode = population;
                for (int j = 0; j < renderNode.GetLength(0); j++)
                {
                    renderNode[j, dimensionsCount - 1] += 100;
                }
                var points = new ILPoints
                {
                    Color = Color.DarkBlue,
                    Positions = renderNode,
                    Size = 10
                };

                plotCube.Add(points);
                panel1.Refresh();
                populationPoints = points;
                listOfPoints.Add(populationPoints);
            }
        }

        private void PaintBestNode(float[,] bestNode, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints)
        {
            //Thread.Sleep(100);
            if (panel1 != null)
            {
                float[,] renderNode = bestNode;
                renderNode[0, dimensionsCount - 1] += 150;
                var bestPoint = new ILPoints
                {
                    Color = Color.Pink,
                    Positions = renderNode,
                    Size = 12
                };

                plotCube.Add(bestPoint);
                panel1.Refresh();
                listOfPoints.Add(bestPoint);
            }
        }
    }
}
