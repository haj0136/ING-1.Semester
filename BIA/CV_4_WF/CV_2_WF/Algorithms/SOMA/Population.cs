using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.SOMA
{
    public class Population
    {
        public List<Node> Nodes { get; set; }

        public Population()
        {
            Nodes = new List<Node>();
        }

        public float[,] To2dArray()
        {
            var vectorList = new List<float[]>();
            foreach (var node in Nodes)
            {
                vectorList.Add(node.ToFloatArray());
            }
            return HelpTools.CreateRectangularArray(vectorList);
        }

        public float[,] GetNextPositions()
        {
            var vectorList = new List<float[]>();
            foreach (var node in Nodes)
            {
                vectorList.Add(node.GetNextPosition());
            }
            return HelpTools.CreateRectangularArray(vectorList);
        }
    }
}
