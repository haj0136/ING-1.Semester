using CV_4_WF.Functions;
using CV_4_WF.SOMA;
using ILNumerics.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CV_4_WF.Algorithms
{
    public class SOMA : IAlgorithm
    {
        private readonly Random rnd;
        private readonly float pathLenght;
        private readonly float stepSize;
        private readonly float PRT;
        private readonly int populationSize;
        private readonly int dimensions;
        private readonly int stepsCount;
        private readonly float negativePRT;

        private float[,] renderNode;
        private ILPoints bestPoint = null;

        public SOMA()
        {
            rnd = new Random();
            pathLenght = 3.0f;
            stepSize = 0.11f;
            PRT = 0.5f; // for more than 2 dimensions set 0.1
            populationSize = 10;
            dimensions = 3;
            stepsCount = (int)(pathLenght / stepSize);
            negativePRT = PRT / 4;
        }

        public void SetStrategy(int strategyIndex)
        {
            
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox bestNodeTextBox)
        {
            ILPoints points = null;
            Population population;

            population = GenerateFirstPopulation(testFunction);

            if (panel1 != null)
            {
                renderNode = population.To2dArray();
                for (int j = 0; j < renderNode.GetLength(0); j++)
                {
                    renderNode[j, dimensions - 1] += 100;
                }
                points = new ILPoints
                {
                    Color = Color.DarkBlue,
                    Positions = renderNode,
                    Size = 10
                };

                plotCube.Add(points);
                panel1.Refresh();
                listOfPoints.Add(points);
            }
            var bestNode = new Node(dimensions);

            for (int j = 0; j < population.Nodes.Count; j++)
            {
                if (bestNode.X[dimensions - 1] > population.Nodes[j].X[dimensions - 1])
                {
                    bestNode.X = new List<float>(population.Nodes[j].X);
                }
            }

            if (panel1 != null)
            {
                PaintBestNode(bestNode, plotCube, panel1, listOfPoints);
            }

            for (int i = 0; i < iterations; i++)
            {

                for (int j = 0; j < population.Nodes.Count; j++)
                {
                    population.Nodes[j].CalculatePRTVector(PRT, negativePRT);
                }

                for (int j = 1; j <= stepsCount; j++)
                {
                    for (int k = 0; k < population.Nodes.Count; k++)
                    {
                        population.Nodes[k].CalculateNewPosition(bestNode.ToFloatArray(), stepSize * j, testFunction);
                    }

                    if (panel1 != null)
                    {
                        Thread.Sleep(200);
                        plotCube.Remove(points);
                        points.Dispose();
                        panel1.Refresh();
                        listOfPoints.Remove(points);

                        renderNode = population.GetNextPositions();
                        for (int k = 0; k < renderNode.GetLength(0); k++)
                        {
                            renderNode[k, dimensions - 1] += 100;
                        }

                        points = new ILPoints
                        {
                            Color = Color.DarkBlue,
                            Positions = renderNode,
                            Size = 10
                        };

                        plotCube.Add(points);
                        panel1.Refresh();
                        listOfPoints.Add(points);
                    }


                }

                for (int j = 0; j < population.Nodes.Count; j++)
                {
                    population.Nodes[j].SetBestPosition();
                }

                if (panel1 != null)
                {
                    Thread.Sleep(800);
                    plotCube.Remove(points);
                    points.Dispose();
                    panel1.Refresh();
                    listOfPoints.Remove(points);

                    renderNode = population.To2dArray();
                    for (int k = 0; k < renderNode.GetLength(0); k++)
                    {
                        renderNode[k, dimensions - 1] += 100;
                    }

                    points = new ILPoints
                    {
                        Color = Color.DarkBlue,
                        Positions = renderNode,
                        Size = 10
                    };

                    plotCube.Add(points);
                    panel1.Refresh();
                    listOfPoints.Add(points);

                    Thread.Sleep(400);
                    plotCube.Remove(bestPoint);
                    bestPoint.Dispose();
                    panel1.Refresh();
                    listOfPoints.Remove(bestPoint);
                }

                for (int j = 0; j < population.Nodes.Count; j++)
                {
                    if (bestNode.X[dimensions - 1] > population.Nodes[j].X[dimensions - 1])
                    {
                        bestNode.X = new List<float>(population.Nodes[j].X);
                    }
                }

                if (panel1 != null)
                {
                    PaintBestNode(bestNode, plotCube, panel1, listOfPoints);
                }
            }
            PrintBestNode(bestNodeTextBox, bestNode);
            return bestNode.To2dArray();
        }

        private Population GenerateFirstPopulation(AbstractFunction testFunction)
        {
            var population = new Population();

            for (int i = 0; i < populationSize; i++)
            {
                population.Nodes.Add(new Node());
                for (int j = 0; j < dimensions - 1; j++)
                {
                    population.Nodes[i].X.Add((float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX);
                }
                population.Nodes[i].X.Add((float)testFunction.getResult(Array.ConvertAll(population.Nodes[i].X.ToArray(), x => (double)x)));
            }

            return population;
        }

        private void PaintBestNode(Node bestNode, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints)
        {
            Thread.Sleep(1000);

            renderNode = bestNode.To2dArray();
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

        private void PrintBestNode(TextBox bestNodeTextBox, Node bestNode)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bestNode.X.Count; i++)
            {
                sb.Append($" x{i + 1} = {bestNode.X[i]}");
            }

            bestNodeTextBox.Text = sb.ToString();

        }
    }
}
