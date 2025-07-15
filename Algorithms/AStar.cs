using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Utils;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms
{
    public static class AStar
    {
        public static List<Node>? FindPath(Graph graph, Node start, Node goal, Func<Node, double> heuristic)
        {
            var fScore = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var openSet = new SortedSet<Node>(new NodeFScoreComparer(fScore));
            var cameFrom = new Dictionary<Node, Node?>();
            var gScore = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);

            gScore[start] = 0;
            fScore[start] = heuristic(start);
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                var current = openSet.Min!;
                if (current == goal)
                    return ReconstructPath(cameFrom, current);

                openSet.Remove(current);

                foreach (var edge in graph.GetEdgesFrom(current))
                {
                    var neighbor = edge.To;
                    var tentativeGScore = gScore[current] + edge.Weight;

                    if (tentativeGScore >= gScore[neighbor]) continue;
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + heuristic(neighbor);

                    openSet.Remove(neighbor); // This is safe even if neighbor isn’t in the set
                    openSet.Add(neighbor);
                }
            }
            return null;
        }

        private static List<Node> ReconstructPath(Dictionary<Node, Node?> cameFrom, Node current)
        {
            var path = new List<Node> { current };
            while (cameFrom.TryGetValue(current, out var prev) && prev != null)
            {
                current = prev;
                path.Insert(0, current);
            }
            return path;
        }

    }
}