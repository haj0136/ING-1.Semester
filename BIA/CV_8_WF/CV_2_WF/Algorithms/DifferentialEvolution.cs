using CV_4_WF.Algorithms.DE;
using CV_4_WF.Functions;
using ILNumerics.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CV_4_WF.Algorithms
{
    class DifferentialEvolution : IAlgorithm
    {
        private readonly Random rnd;
        private readonly double CR;
        private readonly double F;
        private readonly int populationSize;
        private readonly int dimensionsCount;
        /// <summary>
        /// <para>0 == DE/rand/1/bin</para> 
        /// <para>1 == DE/current-to-pbest/1/bin</para>
        /// </summary>
        private int strategyIndex;

        private float[,] renderNode;
        private ILPoints populationPoints = null;

        public DifferentialEvolution()
        {
            CR = 0.9;
            F = 0.8;
            dimensionsCount = 3;
            populationSize = 10 * (dimensionsCount - 1);
            rnd = new Random();
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            Population population;

            population = GenerateFirstPopulation(testFunction);
            PaintPopulation(panel1, population.To2dArray(), plotCube, listOfPoints);

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < populationSize; j++)
                {

                    int r1, r2, r3 = -1;
                    var randomList = new List<int>
                    {
                        j
                    };

                    r1 = UniqueRandomNumber(randomList);
                    r2 = UniqueRandomNumber(randomList);
                    r3 = UniqueRandomNumber(randomList);

                    var noisyVector = new double[dimensionsCount - 1];
                    var trialVector = new double[dimensionsCount - 1];
                    Individual pBest = null;
                    //strategy DE/current-to-pbest/1/bin
                    if (strategyIndex == 1)
                    {
                        var pBestList = new List<Individual>(population.Individuals);
                        pBestList.Sort((ind1, ind2) => ind1.X[dimensionsCount - 1].CompareTo(ind2.X[dimensionsCount - 1]));
                        pBestList = pBestList.Take(5).ToList();
                        pBest = pBestList[rnd.Next(0,4)]; 
                    }

                    for (int k = 0; k < noisyVector.Length; k++)
                    {
                        // Xr3 + F*(Xr1 - Xr2)
                        switch (strategyIndex)
                        {
                            case 0:
                                noisyVector[k] = Math.Round(population.Individuals[r3].X[k] + F * (population.Individuals[r1].X[k] - population.Individuals[r2].X[k]), 8, MidpointRounding.AwayFromZero);
                                break;
                            case 1:
                                noisyVector[k] = Math.Round(population.Individuals[j].X[k] + F * (pBest.X[k]-population.Individuals[j].X[k]) + F * (population.Individuals[r1].X[k] - population.Individuals[r2].X[k]), 8, MidpointRounding.AwayFromZero);
                                break;
                            default:
                                noisyVector[k] = Math.Round(population.Individuals[r3].X[k] + F * (population.Individuals[r1].X[k] - population.Individuals[r2].X[k]), 8, MidpointRounding.AwayFromZero);
                                break;
                        }

                        if (rnd.NextDouble() < CR)
                        {
                            if (noisyVector[k] < testFunction.MinX || noisyVector[k] > testFunction.MaxX)
                            {
                                noisyVector[k] = (float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX;
                            }
                            trialVector[k] = noisyVector[k];
                        }
                        else
                        {
                            trialVector[k] = population.Individuals[j].X[k];
                        }
                    }

                    double costValue = Math.Round(testFunction.getResult(trialVector), 8, MidpointRounding.AwayFromZero);

                    if (costValue < population.Individuals[j].X[dimensionsCount - 1])
                    {
                        for (int k = 0; k < trialVector.Length; k++)
                        {
                            population.Individuals[j].X[k] = (float)trialVector[k];
                        }
                        population.Individuals[j].X[dimensionsCount - 1] = (float)costValue;
                    }


                }
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

            PaintBestNode(bestIndividual.To2dArray(), plotCube, panel1, listOfPoints);
            PrintBestNode(outPutTextBox, bestIndividual.X);
            return bestIndividual.To2dArray();
        }

        public void SetStrategy(int _strategyIndex)
        {
            strategyIndex = _strategyIndex;
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

        private int UniqueRandomNumber(List<int> randomNumbers)
        {
            int randomNumber = 0;
            do
            {
                randomNumber = rnd.Next(0, populationSize - 1);
            } while (randomNumbers.Contains(randomNumber));

            randomNumbers.Add(randomNumber);

            return randomNumber;
        }

        private void PrintBestNode(TextBox bestNodeTextBox, List<float> bestNode)
        {
            StringBuilder sb = new StringBuilder();
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
                Thread.Sleep(100);
                if (populationPoints != null)
                {
                    plotCube.Remove(populationPoints);
                    populationPoints.Dispose();
                    panel1.Refresh();
                    listOfPoints.Remove(populationPoints);
                }

                renderNode = population;
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
                renderNode = bestNode;
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
