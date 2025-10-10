using System.Security.Cryptography.X509Certificates;
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
            var parent = new Dictionary<Node, Node?>(); // Keep track of DFS tree structure
            var articulationPoints = new HashSet<Node>();
            time = 0;
            
            // Apply DFS and find articulation points
            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    DFS.DFSVisit(
                        node,
                        visited,
                        onStack: new HashSet<Node>(),
                        onPreVisit: (n) =>
                        {
                            discoveryTime[n] = time;
                            lowTime[n] = time;
                            parent[n] = null; // Root node has no parent
                            time++;
                        },
                        onPostVisit: (n) =>
                        {
                            int children = 0;
                            bool isArticulation = false;

                            foreach (var edge in graph.GetEdgesFrom(n))
                            {
                                var neighbor = edge.To;
                                if (!visited.Contains(neighbor))
                                {
                                    children++;
                                    parent[neighbor] = n;

                                    // Perform DFS on the neighbor
                                    DFS.DFSVisit(
                                        neighbor,
                                        visited,
                                        onStack: new HashSet<Node>(),
                                        onPreVisit: null,
                                        onPostVisit: (v) =>
                                        {
                                            // After visiting, check for articulation point condition
                                            lowTime[n] = System.Math.Min(lowTime[n], lowTime[neighbor]);
                                            if (parent[n] == null && children > 1)
                                                isArticulation = true; // Root condition
                                            if (parent[n] != null && lowTime[neighbor] >= discoveryTime[n])
                                                isArticulation = true;
                                        },
                                        onCycleDetected: null,
                                        graph: graph);
                                }
                                else if (neighbor != parent[n])
                                {
                                    // Back edge found
                                    lowTime[n] = System.Math.Min(lowTime[n], discoveryTime[neighbor]);
                                }
                            }

                            if (isArticulation)
                            {
                                articulationPoints.Add(n);
                            }
                        },
                        onCycleDetected: null,
                        graph: graph);
                }
            }
            return new List<Node>(articulationPoints);
        }
    }   
}