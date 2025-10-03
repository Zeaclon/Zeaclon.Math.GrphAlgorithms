using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public static class BellmanFord
    {
        /// <summary>
        /// Returns the shortest distance from the source to all nodes using Bellman-Ford
        /// Returns null if a negative-weight cycle is detected.
        /// </summary>
        public static Dictionary<Node, double>? ShortestPaths(Graph graph, Node source)
        {
            if (!graph.Nodes.Contains(source))
                return new Dictionary<Node, double>();
            
            var distances = graph.Nodes.ToDictionary(n => n, n => double.PositiveInfinity);
            distances[source] = 0;

            for (var i = 0; i < graph.Nodes.Count - 1; i++)
            {
                foreach (var edge in graph.Edges)
                {
                    if (distances[edge.From] + edge.Weight < distances[edge.To])
                    {
                        distances[edge.To] = distances[edge.From] + edge.Weight;
                    }
                }
            }

            // detect negative cycles
            foreach (var edge in graph.Edges)
            {
                if (distances[edge.From] + edge.Weight < distances[edge.To])
                {
                    return null;
                }
            }
            
            return distances;
        }
    }
}