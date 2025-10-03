using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity
{
    public static class BridgeFinder
    {
        private static int time;

        public static List<(Node from, Node to)> FindBridges(Graph graph)
        {
            var visited = new HashSet<Node>();
            var discoveryTime = new Dictionary<Node, int>();
            var lowTime = new Dictionary<Node, int>();
            var parent = new Dictionary<Node, Node?>();
            var bridges = new List<(Node, Node)>();
            time = 0;

            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                    DFS.DFSBridge(node, graph, visited, discoveryTime, lowTime, parent, bridges, ref time);
            }

            return bridges;
        }
    }
}