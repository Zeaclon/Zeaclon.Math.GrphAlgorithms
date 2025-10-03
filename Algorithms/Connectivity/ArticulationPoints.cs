using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity
{
    /// <summary>
    /// Finds articulation points (cut vertices) in an undirected graph.
    /// An articulation point is a vertex which, when removed, would disconnect the graph.
    /// </summary>
    public class ArticulationPoints
    {
        private static int time;

        public static List<Node> FindArticulationPoints(Graph graph)
        {
            var visited = new HashSet<Node>();
            var discoveryTime = new Dictionary<Node, int>();
            var lowTime = new Dictionary<Node, int>();
            var parent = new Dictionary<Node, Node?>();
            var articulationPoints = new HashSet<Node>();
            int time = 0;

            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    parent[node] = null;
                    DFS.DFSArticulation(node, graph, visited, discoveryTime, lowTime, parent, articulationPoints, ref time);
                }
            }

            return articulationPoints.ToList();
        }

    }   
}