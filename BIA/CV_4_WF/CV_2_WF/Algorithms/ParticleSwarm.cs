using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CV_4_WF.ParticleSwarm;
using CV_4_WF.Functions;
using ILNumerics.Drawing;
using System.Threading;
using System.Drawing;

namespace CV_4_WF.Algorithms
{
    public class ParticleSwarm : IAlgorithm
    {
        private readonly Random rnd;
        private readonly int populationSize;
        private readonly int dimensions;
        private readonly float c1, c2;
        private float vMax;

        private float[,] renderNode;
        private ILPoints bestPoint = null;
        private ILPoints[] populationPoints = null;

        public ParticleSwarm()
        {
            rnd = new Random();
            populationSize = 10;
            dimensions = 3;
            c1 = c2 = 0.2f;
        }
        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            Population population;
            vMax = (testFunction.MaxX - testFunction.MinX) * 0.05f;
            populationPoints = new ILPoints[populationSize];

            population = GenerateFirstPopulation(testFunction);

            for (int i = 0; i < population.Particles.Count; i++)
            {
                PaintPopulation(panel1, population.Particles[i].To2dArray(), plotCube, listOfPoints, i);
            }
            PaintBestNode(population.GbestTo2dArray(), plotCube, panel1, listOfPoints);

            for (int i = 0; i < iterations; i++)
            {

                for (int j = 0; j < population.Particles.Count; j++)
                {
                    //calculate speed
                    population.Particles[j].CalculateSpeed(population.GBest, c1, c2, vMax);
                    //adjust position + fitness function
                    population.Particles[j].AdjustPosition(testFunction);
                    // repait particle
                    PaintPopulation(panel1, population.Particles[j].To2dArray(), plotCube, listOfPoints, j);

                    if (population.Particles[j].PBest.Last() < population.GBest.Last())
                    {
                        population.GBest = new List<float>(population.Particles[j].PBest);
                        // repaint best node
                        PaintBestNode(population.GbestTo2dArray(), plotCube, panel1, listOfPoints);
                    }
                }

            }
            PrintBestNode(outPutTextBox, population.GBest);
            return population.GbestTo2dArray();
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

        private void PaintBestNode(float[,] bestNode, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints)
        {
            //Thread.Sleep(100);
            if (panel1 != null)
            {

                if (bestPoint != null)
                {
                    plotCube.Remove(bestPoint);
                    bestPoint.Dispose();
                    panel1.Refresh();
                    listOfPoints.Remove(bestPoint);
                }

                renderNode = bestNode;
                renderNode[0, dimensions - 1] += 150;
                bestPoint = new ILPoints
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

        private void PaintPopulation(Panel panel1, float[,] population, ILGroup plotCube, List<ILPoints> listOfPoints, int index)
        {
            if (panel1 != null)
            {
                if(populationPoints[index] != null)
                {
                    plotCube.Remove(populationPoints[index]);
                    populationPoints[index].Dispose();
                    panel1.Refresh();
                    listOfPoints.Remove(populationPoints[index]);
                }

                renderNode = population;
                for (int j = 0; j < renderNode.GetLength(0); j++)
                {
                    renderNode[j, dimensions - 1] += 100;
                }
                var points = new ILPoints
                {
                    Color = Color.DarkBlue,
                    Positions = renderNode,
                    Size = 10
                };

                plotCube.Add(points);
                panel1.Refresh();
                listOfPoints.Add(points);
                populationPoints[index] = points;
            }
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
    }
}
