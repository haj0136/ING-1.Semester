using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_5
{
    public class GraphNode
    {
        public int Id { get; }
        public List<GraphNode> Neighbours { get; set; }
        public int Degree { get; set; }
        public float ClusteringCoeficient { get; set; }

        public GraphNode(int id)
        {
            Neighbours = new List<GraphNode>();
            Id = id;
        }
    }
}
