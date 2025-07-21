using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;
using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity
{
    public static class TransitiveClosure
    {
        public static Dictionary<Node, HashSet<Node>> DFSReachability(Graph graph)
        {
            var transitiveClosure = new Dictionary<Node, HashSet<Node>>();

            foreach (var node in graph.Nodes)
            {
                var reachableNodes = new HashSet<Node>();
                DFS.DFSVisit(
                    node,
                    visited: new HashSet<Node>(),
                    onStack: new HashSet<Node>(), // for cycle detection (if needed)
                    onPreVisit: n => reachableNodes.Add(n),
                    onPostVisit: null,
                    onCycleDetected: null,
                    graph: graph);
                
                transitiveClosure[node] = reachableNodes;
            }
            
            return transitiveClosure;
        }
        

        public static Dictionary<Node, HashSet<Node>> FloydWarshallReachability(Graph graph)
        {
            var reachability = FloydWarshall.AllPairsShortestPaths(graph);
    
            var result = new Dictionary<Node, HashSet<Node>>();

            foreach (var node in graph.Nodes)
            {
                var reachableNodes = new HashSet<Node>();
                foreach (var targetNode in graph.Nodes)
                {
                    if (reachability[node][targetNode] != double.PositiveInfinity)
                        reachableNodes.Add(targetNode);
                }
                result[node] = reachableNodes;
            }

            return result;
        }
    }   
}