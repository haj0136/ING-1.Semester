using CV_2_WF.Functions;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CV_2_WF
{
    public partial class Form1 : Form
    {

        ILGroup plotCube;
        ILSurface surface;
        List<ILPoints> listOfPoints;

        List<AbstractFunction> testFunctions;


        Random r = new Random();
        public Form1()
        {
            InitializeComponent();

            testFunctions = new List<AbstractFunction>();
            listOfPoints = new List<ILPoints>();

            plotCube = new ILPlotCube(twoDMode: true);
            var scene = new ILScene
            {
                plotCube,
            };

            var panel = new ILPanel
            {
                Scene = scene,
            };

            panel1.Controls.Add(panel);

            InicializeFunctions();


        }

        public void InicializeFunctions()
        {
            functionsComboBox.Items.Add("Sphere Function");
            testFunctions.Add(new SphereFunction());
            functionsComboBox.Items.Add("Rosenbrock Function");
            testFunctions.Add(new RosenbrockFunction());


            functionsComboBox.SelectedIndexChanged += RefreshFunction;
            functionsComboBox.SelectedIndex = 0;
        }

        private void RefreshFunction(object sender, EventArgs e)
        {
            int index = functionsComboBox.SelectedIndex;
            var testFunction = GetSelectedFunction();

            var surface = new ILSurface((x, y) => (float)testFunction.getResult(x, y),
                xmin: testFunction.MinX, xmax: testFunction.MaxX, ymax: testFunction.MaxY, ymin: testFunction.MinY);

            if (this.surface != null)
            {
                plotCube.Remove(this.surface);
                this.surface.Dispose();
            }

            plotCube.Add(surface);
            this.surface = surface;

            RemoveAllPoints();

            panel1.Refresh();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            RemoveAllPoints();

            int iterations = (int)iterationsNUD.Value;
            var testFunction = GetSelectedFunction();

            float x1 = (float)r.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX;
            float x2 = (float)r.NextDouble() * (testFunction.MaxY - testFunction.MinY) + testFunction.MinY;

            float[,] firstNode = { { x1, x2, (float)testFunction.getResult(x1, x2) } };

            var points = new ILPoints
            {
                Color = Color.Black,
                Positions = firstNode
            };

            plotCube.Add(points);
            panel1.Refresh();
            listOfPoints.Add(points);

            GeneratePopulation(100);

            for (int i = 1; i < iterations; i++)
            {

            }
        }

        private AbstractFunction GetSelectedFunction()
        {
            int index = functionsComboBox.SelectedIndex;
            return testFunctions[index];
        }

        private void RemoveAllPoints()
        {
            foreach (var item in listOfPoints)
            {
                if (item == null)
                    continue;
                plotCube.Remove(item);
                item.Dispose();
            }
            listOfPoints.Clear();
        }

        private float[,] GeneratePopulation(int sizeOfPopulation)
        {
            List<float[]>
            for (int i = 0; i < sizeOfPopulation; i++)
            {

            double u1 = r.NextDouble(); 
            double u2 = r.NextDouble();

            double z2 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double z1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            Console.WriteLine(z1 + " z2: " + z2);
            }
        }
    }
}
