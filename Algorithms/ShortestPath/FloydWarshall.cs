using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public static class FloydWarshall
    {
        /// <summary>
        /// Computes shortest path distances between all pairs of nodes.
        /// returns null if a negatic cycle is detected.
        /// </summary>
        public static Dictionary<Node, Dictionary<Node, double>>? AllPairsShortestPaths(Graph graph)
        {
            var nodes = graph.Nodes;
            var n = nodes.Count;
            
            // Map each node to an index for matrix access
            var nodeIndex = new Dictionary<Node, int>();
            for (var i = 0; i < n; i++)
                nodeIndex[nodes[i]] = i;
            
            // Initialize distance matrix
            var dist = new double[n, n];
            
            const double INF = double.PositiveInfinity;
            
            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                dist[i, j] = (i == j) ? 0 : INF;
            
            // Set distances from edges
            foreach (var edge in graph.Edges)
            {
                var from = nodeIndex[edge.From];
                var to = nodeIndex[edge.To];
                dist[from, to] = edge.Weight;
            }
            
            // Floyd-Warshall Core
            for (var k = 0; k < n; k++)
            {
                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < n; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }
            
            // Detect negative cycles (distance from i to i < 0
            for (var i = 0; i < n; i++)
            {
                if (dist[i, i] < 0)
                    return null; // Negative cycle detected
            }
            
            // Convert matrix back to dictionary form
            var result = new Dictionary<Node, Dictionary<Node, double>>();
            for (var i = 0; i < n; i++)
            {
                var inner = new Dictionary<Node, double>();
                for (var j = 0; j < n; j++)
                {
                    inner[nodes[j]] = dist[i, j];
                }
                result[nodes[i]] = inner;
            }
            
            return result;
        }
    }
}