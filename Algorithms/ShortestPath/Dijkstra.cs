using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Utils;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public class Dijkstra
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

        public static (double Distance, List<Node> Path) BidirectionalShortestPath(Graph graph, Node source, Node target)
        {
            if (source == target)
                return (0, new List<Node> { source });

            var reverseGraph = graph.Reverse();

            var distFromSource = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var distFromTarget = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);

            var prevFromSource = new Dictionary<Node, Node?>();
            var prevFromTarget = new Dictionary<Node, Node?>();

            var visitedFromSource = new HashSet<Node>();
            var visitedFromTarget = new HashSet<Node>();

            var pqSource = new SortedSet<Node>(new NodeFScoreComparer(distFromSource));
            var pqTarget = new SortedSet<Node>(new NodeFScoreComparer(distFromTarget));

            distFromSource[source] = 0;
            distFromTarget[target] = 0;
            prevFromSource[source] = null;
            prevFromTarget[target] = null;

            pqSource.Add(source);
            pqTarget.Add(target);

            var bestPathLength = double.PositiveInfinity;
            Node? meetingNode = null;

            while (pqSource.Count > 0 && pqTarget.Count > 0)
            {
                // Forward step
                var currentF = pqSource.Min!;
                pqSource.Remove(currentF);
                visitedFromSource.Add(currentF);

                foreach (var edge in graph.GetEdgesFrom(currentF))
                {
                    var neighbor = edge.To;
                    var alt = distFromSource[currentF] + edge.Weight;

                    if (alt < distFromSource[neighbor])
                    {
                        pqSource.Remove(neighbor);
                        distFromSource[neighbor] = alt;
                        prevFromSource[neighbor] = currentF;
                        pqSource.Add(neighbor);
                    }

                    if (visitedFromTarget.Contains(neighbor))
                    {
                        var pathLength = distFromSource[neighbor] + distFromTarget[neighbor];
                        if (pathLength < bestPathLength)
                        {
                            bestPathLength = pathLength;
                            meetingNode = neighbor;
                        }
                    }
                }

                // Backward step
                var currentB = pqTarget.Min!;
                pqTarget.Remove(currentB);
                visitedFromTarget.Add(currentB);

                foreach (var edge in reverseGraph.GetEdgesFrom(currentB))
                {
                    var neighbor = edge.To;
                    double alt = distFromTarget[currentB] + edge.Weight;

                    if (alt < distFromTarget[neighbor])
                    {
                        pqTarget.Remove(neighbor);
                        distFromTarget[neighbor] = alt;
                        prevFromTarget[neighbor] = currentB;
                        pqTarget.Add(neighbor);
                    }

                    if (visitedFromSource.Contains(neighbor))
                    {
                        double pathLength = distFromSource[neighbor] + distFromTarget[neighbor];
                        if (pathLength < bestPathLength)
                        {
                            bestPathLength = pathLength;
                            meetingNode = neighbor;
                        }
                    }
                }

                // Early termination
                var minF = pqSource.FirstOrDefault();
                var minB = pqTarget.FirstOrDefault();
                if (minF != null && minB != null &&
                    distFromSource[minF] + distFromTarget[minB] >= bestPathLength)
                {
                    break;
                }
            }

            if (meetingNode == null)
                return (double.PositiveInfinity, new List<Node>());

            // Reconstruct path from source to meeting node
            var path = new List<Node>();
            for (var node = meetingNode; node != null; node = prevFromSource.GetValueOrDefault(node))
                path.Insert(0, node);

            // Reconstruct path from meeting node to target
            var tail = new List<Node>();
            for (var node = prevFromTarget.GetValueOrDefault(meetingNode); node != null; node = prevFromTarget.GetValueOrDefault(node))
                tail.Add(node);

            path.AddRange(tail);
            return (bestPathLength, path);
        }
    }
}