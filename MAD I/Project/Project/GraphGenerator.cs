using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class GraphGenerator
    {
        private readonly int _m0;
        private readonly int _m;
        private readonly int _numberOfNodes;
        private readonly Random _rnd;

        /// <param name="m0">Number of fundamental nodes.</param>
        /// <param name="m">Number of edges for new created nodes.</param>
        /// <param name="numberOfNodes">Number of nodes.</param>
        public GraphGenerator(int m0, int m, int numberOfNodes)
        {
            _m0 = m0;
            _m = m;
            _numberOfNodes = numberOfNodes;
            _rnd = new Random();
        }

        public Graph Generate()
        {
            var graph = new Graph(_m0);

            // Base graph (complete graph)
            for (int i = 0; i < graph.NodeList.Count; i++)
            {
                for (int j = 0; j < graph.NodeList.Count; j++)
                {
                    if (i == j)
                        continue;
                    graph.NodeList[i].Neighbors.Add(graph.NodeList[j]);
                }
            }

            var nodes = new List<int>();
            foreach (var node in graph.NodeList)
            {
                node.Degree = node.Neighbors.Count;
                for (int i = 0; i < node.Degree; i++)
                {
                    nodes.Add(node.Id);
                }
            }

            // Adding new nodes

            for (int i = 0; i < _numberOfNodes; i++)
            {
                var node = new GraphNode(_m0 + 1 + i);
                var usedNodesIds = new List<int>();
                for (int j = 0; j < _m; j++)
                {
                    int randomIndex = _rnd.Next(nodes.Count - 1);
                    while (usedNodesIds.Contains(nodes[randomIndex]))
                    {
                        randomIndex = _rnd.Next(nodes.Count - 1);
                    }

                    GraphNode newNeighbor = graph.NodeList.Find(x => x.Id == nodes[randomIndex]);
                    node.Neighbors.Add(newNeighbor);
                    newNeighbor.Neighbors.Add(node);
                    usedNodesIds.Add(nodes[randomIndex]);
                }

                for (int j = 0; j < _m; j++)
                {
                    nodes.Add(node.Id);
                    nodes.Add(node.Neighbors[j].Id);
                }

                graph.NodeList.Add(node);
            }

            return graph;
        }
    }
}
