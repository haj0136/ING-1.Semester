using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_3
{
    public class Graph
    {
        public List<GraphNode> NodeList {get;}

        public double AverageDegree { get; set; }

        public Graph(int nodes)
        {
            NodeList = new List<GraphNode>();
            for (int i = 0; i < nodes; i++)
            {
                NodeList.Add(new GraphNode());
            }
        }
    }
}
