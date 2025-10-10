using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity
{
    /// <summary>
    /// Finds all bridges (critical edges) in an undirected graph.
    /// A bridge is an edge that, if removed, increases the number of connected components.
    /// </summary>
    public static class BridgeFinder
    {
        private static int time;

        public static List<(Node from, Node to)> FindBridges(Graph graph)
        {
            var visited = new HashSet<Node>();
            var discoveryTime = new Dictionary<Node, int>();
            var lowTime = new Dictionary<Node, int>();
            var bridges = new List<(Node, Node)>();

            time = 0;

            // Use DFS from each unvisited node
            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    DFS.DFSVisit(
                        node,
                        visited,
                        onStack: new HashSet<Node>(),
                        onPreVisit: null,
                        onPostVisit: null,
                        onCycleDetected: null,
                        graph: graph);
                }
            }

            // Now process edges and look for bridges
            foreach (var node in graph.Nodes)
            {
                // Using DFS on the edges to identify bridges
                DFS.DFSVisit(
                    node,
                    visited,
                    onStack: new HashSet<Node>(),
                    onPreVisit: (n) =>
                    {
                        discoveryTime[n] = time;
                        lowTime[n] = time;
                        time++;
                    },
                    onPostVisit: (n) =>
                    {
                        // Check for bridge condition after DFS visit
                        foreach (var edge in graph.GetEdgesFrom(n))
                        {
                            var neighbor = edge.To;
                            if (lowTime[neighbor] > discoveryTime[n])
                            {
                                bridges.Add((n, neighbor));  // Found a bridge
                            }
                        }
                    },
                    onCycleDetected: null,
                    graph: graph);
            }

            return bridges;
        }
    }
}