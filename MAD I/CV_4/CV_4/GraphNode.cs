using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4
{
    public class GraphNode
    {
        public List<GraphNode> Neighbours { get; set; }

        public int Degree { get; set; }

        public GraphNode()
        {
            Neighbours = new List<GraphNode>();
        }
    }
}
