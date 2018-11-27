using CV_4_WF.Algorithms;
using CV_4_WF.Functions;
using CV_7_WF.Algorithms;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CV_4_WF
{
    public partial class Form1 : Form
    {

        ILGroup plotCube;
        ILSurface surface;
        List<ILPoints> listOfPoints;
        ILPanel panel;

        List<AbstractFunction> testFunctions;
        List<IAlgorithm> algorithms;


        Random rnd;
        public Form1()
        {
            InitializeComponent();

            testFunctions = new List<AbstractFunction>();
            algorithms = new List<IAlgorithm>();
            listOfPoints = new List<ILPoints>();
            rnd = new Random();

            plotCube = new ILPlotCube();
            var scene = new ILScene
            {
                plotCube,
            };

            panel = new ILPanel
            {
                Scene = scene,
            };

            InicializeFunctions();
            InicializeAlgorithms();
            radioButtonShowInGraph.CheckedChanged += ShowSelectedPanel;
            radioButtonShowInGraph.Select();

        }

        public void InicializeFunctions()
        {
            functionsComboBox.Items.Add("Sphere Function");
            testFunctions.Add(new SphereFunction());
            functionsComboBox.Items.Add("Rosenbrock Function");
            testFunctions.Add(new RosenbrockFunction());
            functionsComboBox.Items.Add("Ackley Function");
            testFunctions.Add(new AckleyFunction());
            functionsComboBox.Items.Add("Schwefel Function");
            testFunctions.Add(new SchwefelFunction());
            functionsComboBox.Items.Add("Rastrigin Function");
            testFunctions.Add(new RastriginFunction());




            functionsComboBox.SelectedIndexChanged += RefreshFunction;
            functionsComboBox.SelectedIndex = 0;
        }

        public void InicializeAlgorithms()
        {
            algorithmsComboBox.Items.Add("Hill Climbing");
            algorithms.Add(new HillClimbing());
            algorithmsComboBox.Items.Add("Simulated Annealing");
            algorithms.Add(new SimulatedAnnealing());
            algorithmsComboBox.Items.Add("SOMA");
            algorithms.Add(new Algorithms.SOMA());
            algorithmsComboBox.Items.Add("Particle Swarm");
            algorithms.Add(new Algorithms.ParticleSwarm());
            algorithmsComboBox.Items.Add("Differential Evolution");
            algorithms.Add(new DifferentialEvolution());
            algorithmsComboBox.Items.Add("Genetic Algorithm");
            algorithms.Add(new GeneticAlgorithm());
            algorithmsComboBox.Items.Add("Ant Colony");
            algorithms.Add(new AntColony());

            algorithmsComboBox.SelectedIndex = 0;
            algorithmsComboBox.SelectedIndexChanged += RefreshFunction;
            algorithmsComboBox.SelectedIndexChanged += EnableStratgiesComboBox;
        }

        public void InicializeStrategies()
        {
            strategiesComboBox.Items.Clear();

            strategiesComboBox.Items.Add("DE/rand/1/bin");
            strategiesComboBox.Items.Add("DE/current-to-pbest/1/bin");

            strategiesComboBox.SelectedIndex = 0;
            strategiesComboBox.SelectedIndexChanged += RefreshFunction;
        }

        private void RefreshFunction(object sender, EventArgs e)
        {
            int index = functionsComboBox.SelectedIndex;
            var testFunction = GetSelectedFunction();

            if(panel1.Controls.Count < 1)
            {
                functionsComboBox.Enabled = true;
                panel1.Controls.Add(panel);
            }
            // Remove ILNumrics panel if GA is selected
            if ((algorithmsComboBox.SelectedIndex == 5 || algorithmsComboBox.SelectedIndex == 6) && panel1.Controls.Count > 0)
            {
                panel1.Controls.Clear();
                functionsComboBox.Enabled = false;
            }

            var surface = new ILSurface((x, y) => (float)testFunction.getResult(x, y),
                xmin: testFunction.MinX, xmax: testFunction.MaxX, ymax: testFunction.MaxY, ymin: testFunction.MinY, xlen: 100, ylen: 100);

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
            // number of iterations (NUD = numericUpDown)
            int iterations = (int)iterationsNUD.Value;
            var testFunction = GetSelectedFunction();
            dataGridView1.Rows.Clear();
            bestNodeTextBox.Text = "";
            bestNodeTextBox.Refresh();

            IAlgorithm selectedAlgorithm = GetSelectedAlgorithm();
            selectedAlgorithm.SetStrategy(strategiesComboBox.SelectedIndex);
            float[,] lastNode = null;

            if (radioButtonShowInGraph.Checked == true)
            {
                lastNode = selectedAlgorithm.StartAlgorithm(testFunction, iterations, plotCube, panel1, listOfPoints, bestNodeTextBox);
            }
            else
            {
                var results = new List<float>();
                for (int i = 0; i < 30; i++)
                {
                    lastNode = selectedAlgorithm.StartAlgorithm(testFunction, iterations, plotCube, null, listOfPoints, bestNodeTextBox);
                    dataGridView1.Rows.Add(lastNode[0, 2]);
                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    results.Add(lastNode[0, 2]);
                }
                textBoxAverageValue.Text = (results.Sum() / results.Count).ToString();
            }
        }

        private AbstractFunction GetSelectedFunction()
        {
            int index = functionsComboBox.SelectedIndex;
            return testFunctions[index];
        }

        private IAlgorithm GetSelectedAlgorithm()
        {
            int index = algorithmsComboBox.SelectedIndex;
            return algorithms[index];
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

        private void ShowSelectedPanel(object sender, EventArgs e)
        {
            if (radioButtonShowInGraph.Checked == true)
            {
                panel1.BringToFront();
            }
            else
            {
                panel2.BringToFront();
            }

        }

        private void EnableStratgiesComboBox(object sender, EventArgs e)
        {
            switch (algorithmsComboBox.SelectedIndex)
            {
                case 4:
                    InicializeStrategies();
                    strategiesComboBox.Enabled = true;
                    break;
                default:
                    strategiesComboBox.Items.Clear();
                    strategiesComboBox.Enabled = false;
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (panel1.Controls.Count < 1)
            {
                panel1.Controls.Add(panel);
            }
        }
    }
}
