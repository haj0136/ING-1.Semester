using CV_4_WF.Functions;
using ILNumerics.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CV_4_WF.Algorithms
{
    public class HillClimbing : IAlgorithm
    {
        private Random rnd;

        public HillClimbing()
        {
            rnd = new Random();
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox bestNodeTextBox)
        {
            ILPoints points;
            float[,] population, renderNode;

            float x1 = (float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX;
            float x2 = (float)rnd.NextDouble() * (testFunction.MaxY - testFunction.MinY) + testFunction.MinY;

            float[,] actualNode = { { x1, x2, (float)testFunction.getResult(x1, x2) + 500 } };


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

                // Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                if(panel1 != null)
                {
                    points = new ILPoints
                    {
                        Color = Color.Red,
                        Positions = population
                    };

                    plotCube.Add(points);
                    panel1.Refresh();
                    listOfPoints.Add(points);
                }

                for (int j = 0; j < population.GetLength(0); j++)
                {
                    if (population[j, 2] < actualNode[0, 2])
                    {
                        actualNode = new float[,] { { population[j, 0], population[j, 1], population[j, 2] } };
                    }
                }

                //Thread.Sleep(100);
            }
            return actualNode;
        }
    }
}
