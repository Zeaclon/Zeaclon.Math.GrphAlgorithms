using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Extensions;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Flow
{
    public static class FordFulkerson
    {
        public static double MaxFlow(Graph graph, Node source, Node sink)
        {
            var residualGraph = graph.Clone();

            double maxFlow = 0;

            while (TryFindAugmentingPath(residualGraph, source, sink, out var path, out double pathFlow))
            {
                maxFlow += pathFlow;

                for (int i = 0; i < path.Count - 1; i++)
                {
                    var u = path[i];
                    var v = path[i + 1];

                    // Use extension methods here:
                    residualGraph.UpdateEdgeCapacity(u, v, residualGraph.GetEdgeCapacity(u, v) - pathFlow);
                    residualGraph.UpdateEdgeCapacity(v, u, residualGraph.GetEdgeCapacity(v, u) + pathFlow);
                }
            }

            return maxFlow;
        }

        private static bool TryFindAugmentingPath(Graph residualGraph, Node source, Node sink,
            out List<Node> path, out double pathFlow)
        {
            var queue = new Queue<Node>();
            var parents = new Dictionary<Node, Node?>();
            var visited = new HashSet<Node>();

            queue.Enqueue(source);
            visited.Add(source);
            parents[source] = null;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == sink)
                    break;

                foreach (var edge in residualGraph.GetEdgesFrom(current))
                {
                    if (edge.Weight > 0 && !visited.Contains(edge.To))
                    {
                        visited.Add(edge.To);
                        parents[edge.To] = current;
                        queue.Enqueue(edge.To);
                    }
                }
            }

            if (!visited.Contains(sink))
            {
                path = new List<Node>();
                pathFlow = 0;
                return false;
            }

            path = new List<Node>();
            for (var node = sink; node is not null; node = parents[node]!)
                path.Insert(0, node);

            pathFlow = double.PositiveInfinity;
            for (int i = 0; i < path.Count - 1; i++)
            {
                var u = path[i];
                var v = path[i + 1];
                var capacity = residualGraph.GetEdgeCapacity(u, v);
                if (capacity < pathFlow)
                    pathFlow = capacity;
            }

            return true;
        }
    }
}