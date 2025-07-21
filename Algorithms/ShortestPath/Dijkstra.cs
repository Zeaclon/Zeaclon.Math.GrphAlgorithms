using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Utils;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public static class Dijkstra
    {
        /// <summary>
        /// Calculates the shortest path distances from the source node to all other nodes using Dijkstra’s algorithm.
        /// </summary>
        /// <param name="graph">Adjacency Matrix representing the graph (weights). Use int.MaxValue for no edge.</param>
        /// <param name="source">Index of the source node.</param>
        /// <returns>Array of shortest distances from source to each node.</returns>
        public static Dictionary<Node, double> ShortestPaths(Graph graph, Node source)
        {
            var distances = graph.Nodes.ToDictionary(node => node, _ => double.PositiveInfinity);
            var visited = new HashSet<Node>();
            var priorityQueue = new SortedSet<Node>(
                new NodeFScoreComparer(distances)
            );

            distances[source] = 0;
            priorityQueue.Add(source);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Min!;
                priorityQueue.Remove(current);

                if (visited.Contains(current))
                    continue;

                visited.Add(current);

                foreach (var edge in graph.GetEdgesFrom(current))
                {
                    var neighbor = edge.To;
                    var alt = distances[current] + edge.Weight;

                    if (alt < distances[neighbor])
                    {
                        // SortedSet requires re-adding to update order
                        priorityQueue.Remove(neighbor);
                        distances[neighbor] = alt;
                        priorityQueue.Add(neighbor);
                    }
                }
            }

            return distances;
        }
    }
}