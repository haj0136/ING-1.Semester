using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CV_4_WF.Functions;
using ILNumerics.Drawing;

namespace CV_4_WF.Algorithms
{
    class SimulatedAnnealing : IAlgorithm
    {
        private Random rnd;
        private readonly float alpha;

        public SimulatedAnnealing()
        {
            rnd = new Random();
            alpha = 0.99f;
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox bestNodeTextBox)
        {
            ILPoints points;
            float[,] population, renderNode;
            float t0 = 200; // initial temperature

            float x1 = (float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX;
            float x2 = (float)rnd.NextDouble() * (testFunction.MaxY - testFunction.MinY) + testFunction.MinY;

            float[,] actualNode = { { x1, x2, (float)testFunction.getResult(x1, x2) } };

            for (int i = 0; i < iterations; i++)
            {
                if(panel1 != null)
                {
                    renderNode = (float[,])actualNode.Clone();
                    renderNode[0, 2] += 100;

                    points = new ILPoints
                    {
                       Color = Color.Black,
                      Positions = renderNode,
                      Size = 10
                    };

                    plotCube.Add(points);
                    panel1.Refresh();
                    listOfPoints.Add(points);
                }

                population = PopulationGenerator.GeneratePopulation(1, testFunction, actualNode[0, 0], actualNode[0, 1]);

                if(panel1 != null)
                {
                    points = new ILPoints
                    {
                        Color = Color.MediumPurple,
                        Positions = population
                    };

                    plotCube.Add(points);
                    panel1.Refresh();
                    listOfPoints.Add(points);
                }

                float deltaF = population[0, 2] - actualNode[0, 2];

                if (deltaF < 0)
                {
                    actualNode = new float[,] { { population[0, 0], population[0, 1], population[0, 2] } };
                }
                else
                {
                    float r = (float)rnd.NextDouble();
                    if(r < Math.Pow(Math.E, -deltaF / t0))
                    {
                    actualNode = new float[,] { { population[0, 0], population[0, 1], population[0, 2] } };
                    }
                }
                // temperature reduce
                t0 = t0 * alpha;

                //Thread.Sleep(100);
            }
            PrintBestNode(bestNodeTextBox, actualNode);
            return actualNode;
        }

        private void PrintBestNode(TextBox textBox, float[,] bestNode)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bestNode.Length; i++)
            {
                sb.Append($" x{i + 1} = {bestNode[0,i]}");
            }

            textBox.Text = sb.ToString();
        }
    }
}
