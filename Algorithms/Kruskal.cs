using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms
{
    public static class Kruskal
    {
        /// <summary>
        /// Computes a Minimum Spanning Tree (MST) of the given undirected graph using Kruskal's algorithm.
        /// returns a list of edges in the MST
        /// Assumes graph is connected and undirected
        /// </summary>
        public static List<Edge> MinimumSpanningTree(Graph graph)
        {
            var mstEdges = new List<Edge>();
            var nodes = graph.Nodes;
            if (nodes.Count == 0)
                return mstEdges;
            
            // sort edges by weight
            var sortedEdges = new List<Edge>(graph.Edges);
            sortedEdges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            var uf = new UnionFind(nodes);

            foreach (var edge in sortedEdges)
            {
                if (uf.Find(edge.From) != uf.Find(edge.To))
                {
                    uf.Union(edge.From, edge.To);
                    mstEdges.Add(edge);
                }
            }

            return mstEdges;
        } 
    }   
}