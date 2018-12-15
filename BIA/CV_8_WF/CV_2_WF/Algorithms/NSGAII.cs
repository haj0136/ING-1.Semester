using CV_4_WF.Algorithms;
using CV_4_WF.Functions;
using CV_8_WF.NSGA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CV_8_WF.Algorithms
{
    public class NSGAII : IAlgorithm
    {
        private readonly Random rnd;
        private readonly int populationSize;
        private Population population;

        public NSGAII()
        {
            rnd = new Random();
            populationSize = 30;
            population = new Population();
        }

        public void SetStrategy(int strategyIndex)
        {
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILNumerics.Drawing.ILGroup plotCube, Panel panel1, List<ILNumerics.Drawing.ILPoints> listOfPoints, TextBox outPutTextBox)
        {
            if (panel1 != null)
                panel1.Paint += Panel1_Paint;

            population.GenerateFirstPop(populationSize);


            for (int k = 0; k < iterations; k++)
            {

                population.GenerateOffsprings(populationSize);

                var n = new Dictionary<int, int>();

                for (int i = 0; i < population.individuals.Count; i++)
                {
                    n.Add(i, 0);
                    for (int j = 0; j < population.individuals.Count; j++)
                    {
                        if (i == j)
                            continue;

                        if (population.costF1[i] <= population.costF1[j] && population.costF2[i] <= population.costF2[j])
                        {
                            n[i]++;
                        }
                    }
                }

                var sortedDic = n.OrderBy(x => x.Value).Take(populationSize);
                var individuals = new List<float>();
                var costF1 = new List<float>();
                var costF2 = new List<float>();

                foreach (KeyValuePair<int, int> item in sortedDic)
                {
                    individuals.Add(population.individuals[item.Key]);
                    costF1.Add(population.costF1[item.Key]);
                    costF2.Add(population.costF2[item.Key]);
                }

                population.individuals = individuals;
                population.costF1 = costF1;
                population.costF2 = costF2;


            }
            if (panel1 != null)
            {
                panel1.Refresh();
                panel1.Paint -= Panel1_Paint;
            }

            return null;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            int xMove = 150;
            int yMove = 100;
            int step = 100;


            var graphics = e.Graphics;

            var pen = new Pen(Color.Black, 1);
            var font = new Font(FontFamily.GenericSansSerif, 8);
            var brush = new SolidBrush(Color.Black);


            // axis Y
            var p1 = new Point(0 + xMove, -10 + yMove);
            var p2 = new Point(0 + xMove, (8 * step / 2) + yMove);

            graphics.DrawLine(pen, p1, p2);

            // axis X

            p1 = new Point(0 + xMove, (8 * step / 2) + yMove);
            p2 = new Point((9 * step / 2) + 10 + xMove, (8 * step / 2) + yMove);

            graphics.DrawLine(pen, p1, p2);

            // axis Y points (step = 0.5)

            for (int i = 0; i < 8; i++)
            {
                p1 = new Point(-10 + xMove, (i * step / 2) + yMove);
                p2 = new Point(0 + xMove, (i * step / 2) + yMove);

                graphics.DrawLine(pen, p1, p2);

                graphics.DrawString(-i * 0.5 + "", font, brush, -35 + xMove, (i * step / 2) - (font.Height / 2) - 1 + yMove);
            }

            // axis X points (step = 0.5)

            for (int i = 1; i <= 8 + 1; i++)
            {
                p1 = new Point((i * step / 2) + xMove, (8 * step / 2) + yMove);
                p2 = new Point((i * step / 2) + xMove, (8 * step / 2) + 10 + yMove);

                graphics.DrawLine(pen, p1, p2);

                graphics.DrawString(-4 + ((i - 1) * 0.5) + "", font, brush, (i * step / 2) - 9 + xMove, (8 * step / 2) + yMove + 15);
            }

            // results
            brush = new SolidBrush(Color.Red);

            for (int i = 0; i < population.individuals.Count; i++)
            {
                float x = population.costF1[i];
                float y = population.costF2[i];
                graphics.FillEllipse(brush, ((4.5f + x) * step + xMove - 4), ((y * -1) * step + yMove - 4), 8, 8);
            }
        }
    }
}
