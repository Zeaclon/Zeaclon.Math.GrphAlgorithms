using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity
{
    public static class KosarjuSCC
    {
        /// <summary>
        /// Finds strongly connected components (SCCs) in the given directed graph using Josaraju's algorithm.
        /// Returns a list of SCCs, each SCC is a list of nodes.
        /// </summary>
        public static List<List<Node>> FindSCCs(Graph graph)
        {
            var visited = new HashSet<Node>();
            var finishStack = new Stack<Node>();

            // 1st DFS pass: order nodes by finish time
            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    DFS.DFSVisit(
                        node,
                        visited,
                        onStack: new HashSet<Node>(), // empty hashset
                        onPreVisit: null,
                        onPostVisit: n => finishStack.Push(n),
                        onCycleDetected: null,
                        graph: graph);
                }
            }
            
            // 2nd pass: DFS on reversed graph
            var reversedGraph = graph.Reverse();
            visited.Clear();
            
            var sccList = new List<List<Node>>();

            while (finishStack.Count > 0)
            {
                var node = finishStack.Pop();
                if (!visited.Contains(node))
                {
                    var scc = new List<Node>();
                    DFS.DFSVisit(
                        node,
                        visited,
                        onStack: new HashSet<Node>(), // empty hashset
                        onPreVisit: n => scc.Add(n),
                        onPostVisit: null,
                        onCycleDetected: null,
                        graph: reversedGraph);
                    sccList.Add(scc);
                }
            }
            
            return sccList;
        }
    }   
}