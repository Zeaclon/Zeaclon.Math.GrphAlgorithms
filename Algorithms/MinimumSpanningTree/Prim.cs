using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.MinimumSpanningTree
{
    public static class Prim
    {
        public static List<Edge> MinimumSpanningTree(Graph graph)
        {
            if (graph.IsDirected)
                throw new InvalidOperationException("Prim's algorithm requires an undirected graph.");
            
            var mstEdges = new List<Edge>();
            var nodes = graph.Nodes;
            if (nodes.Count == 0)
                return mstEdges;

            // Build adjacency map: Node -> List of connected edges
            var adjacency = new Dictionary<Node, List<Edge>>();
            foreach (var node in nodes)
                adjacency[node] = new List<Edge>();

            foreach (var edge in graph.Edges)
            {
                adjacency[edge.From].Add(edge);
                adjacency[edge.To].Add(edge); // undirected graph: add edge for both ends
            }

            var visited = new HashSet<Node>();
            var edgeQueue = new SortedSet<(double weight, Edge edge)>(Comparer<(double, Edge)>.Create(
                (a, b) =>
                {
                    var cmp = a.Item1.CompareTo(b.Item1);
                    if (cmp == 0)
                        cmp = a.Item2.From.Id.CompareTo(b.Item2.From.Id);
                    if (cmp == 0)
                        cmp = a.Item2.To.Id.CompareTo(b.Item2.To.Id);
                    return cmp;
                }));

            var start = nodes[0];
            visited.Add(start);

            // Add edges connected to start node to the queue
            foreach (var edge in adjacency[start])
            {
                edgeQueue.Add((edge.Weight, edge));
            }

            while (edgeQueue.Count > 0 && visited.Count < nodes.Count)
            {
                var (weight, edge) = edgeQueue.Min;
                edgeQueue.Remove(edgeQueue.Min);

                // Determine which node is new (not visited)
                var nextNode = !visited.Contains(edge.From) ? edge.From : edge.To;
                if (visited.Contains(nextNode))
                    continue;

                visited.Add(nextNode);
                mstEdges.Add(edge);

                // Add edges connected to the newly visited node
                foreach (var e in adjacency[nextNode])
                {
                    var neighbor = e.From == nextNode ? e.To : e.From;
                    if (!visited.Contains(neighbor))
                    {
                        edgeQueue.Add((e.Weight, e));
                    }
                }
            }

            return mstEdges;
        }
    }
}