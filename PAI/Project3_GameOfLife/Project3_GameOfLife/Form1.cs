using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_GameOfLife
{
    public partial class Form1 : Form
    {
        private Grid _formGrid;
        private CancellationTokenSource _tokenSource;
        private bool _parallel = false;
        public Form1()
        {
            _tokenSource = new CancellationTokenSource();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formGrid = new Grid();

            /*for (int i = 0; i < 50; i++)
            {
                int x = 10 * i + 12;

                Controls.Add(new Label()
                {
                    AutoSize = true,
                    Text = i.ToString(),
                    Location = new Point(x, 0),
                    Font = new Font(Font.FontFamily, 6)
                });

                for (int j = 0; j < 50; j++)
                {
                    int y = 10 * j + 12;

                    Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Text = j.ToString(),
                        Location = new Point(0, y),
                        Font = new Font(Font.FontFamily, 6)
                    });
                }
            }*/
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_formGrid.ToBitmap(), 0,0);
            e.Graphics.Dispose();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var futureTask = Task.Factory.StartNew(x =>
            {
                while (!_tokenSource.IsCancellationRequested)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    if (_parallel)
                    {
                        _formGrid.UpdateGridParallel();
                    }
                    else
                    {
                        _formGrid.UpdateGrid();
                    }
                    sw.Stop();
                    TimeLabel.Invoke((MethodInvoker)(() =>
                        {
                            TimeLabel.Text = $"Time Elapsed: {sw.Elapsed.Milliseconds}";
                        }));
                    Graphics graphics = this.CreateGraphics();
                    graphics.Clear(this.BackColor);
                    graphics.DrawImage(_formGrid.ToBitmap(), 0, 0);
                    graphics.Dispose();
                    Thread.Sleep(30);
                }
            }, _tokenSource);

            StartButton.Hide();
            var stopButton = new Button()
            {
                Text = "Stop",
                Location = StartButton.Location,
                Size = StartButton.Size
            };

            this.Controls.Add(stopButton);
            stopButton.Click += new EventHandler(async (x, y) =>
            {
                _tokenSource.Cancel();
                stopButton.Hide();
                StartButton.Show();
                await futureTask;
                futureTask.Wait();
                _tokenSource = new CancellationTokenSource();
            });
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _formGrid = new Grid();

            Graphics graphics = this.CreateGraphics();
            graphics.Clear(this.BackColor);
            graphics.DrawImage(_formGrid.ToBitmap(), 0, 0);
            graphics.Dispose();
        }

        private void ParallelButton_Click(object sender, EventArgs e)
        {
            if (!_parallel)
            {
                _parallel = true;
                ParallelButton.Text = "Parallel: on";
            }
            else
            {
                _parallel = false;
                ParallelButton.Text = "Parallel: off";
            }

            ParallelButton.Refresh();
        }
    }
}
