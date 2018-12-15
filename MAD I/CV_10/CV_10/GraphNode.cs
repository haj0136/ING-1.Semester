using System.Collections.Generic;

namespace CV_10
{
    public class GraphNode
    {
        public int Id { get; }
        public List<GraphNode> Neighbours { get; set; }
        public int Degree { get; set; }
        public float ClusteringCoeficient { get; set; }
        public double AverageDistance { get; set; }
        public string Name { get; set; }
        public NodeType Type { get; set; }

        public GraphNode(int id)
        {
            Neighbours = new List<GraphNode>();
            Id = id;
        }

        public GraphNode(GraphNode graphNode, bool withNeighbours)
        {
            if (withNeighbours)
            {
                Neighbours = new List<GraphNode>(graphNode.Neighbours);
            }
            else
            {
                Neighbours = new List<GraphNode>();
            }
            Id = graphNode.Id;
            Degree = graphNode.Degree;
            ClusteringCoeficient = graphNode.ClusteringCoeficient;
            Name = graphNode.Name;
        }

        public bool Equals(GraphNode obj)
        {
            return obj.Id == Id;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as GraphNode);
        }
        public override int GetHashCode()
        {
            return Id;
        }

        public enum NodeType
        {
            Actor,
            Movie
        }
    }
}
