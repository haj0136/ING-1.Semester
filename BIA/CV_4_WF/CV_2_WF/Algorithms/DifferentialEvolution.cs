using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CV_4_WF.Functions;
using ILNumerics.Drawing;

namespace CV_4_WF.Algorithms
{
    class DifferentialEvolution : IAlgorithm
    {
        private readonly double CR;
        private readonly double F;
        private int strategyIndex;
        

        public DifferentialEvolution()
        {
            CR = 0.9;
            F = 0.8;
        }

        public float[,] StartAlgorithm(AbstractFunction testFunction, int iterations, ILGroup plotCube, Panel panel1, List<ILPoints> listOfPoints, TextBox outPutTextBox)
        {

            return null;
        }

        public void SetStrategy(int strategyIndex)
        {
            this.strategyIndex = strategyIndex;
        }
    }
}
