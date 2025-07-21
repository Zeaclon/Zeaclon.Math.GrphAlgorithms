using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Topological
{
    public class TopologicalSort
    {
        public static List<Node>? TopoDFS(Graph graph)
        {
            var visited = new HashSet<Node>();
            var onStack = new HashSet<Node>();
            var result = new List<Node>();
            bool cycleDetected = false;

            void OnCycle(Node _) => cycleDetected = true;

            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    DFS.DFSVisit(node, visited, onStack, 
                        onPreVisit: null, 
                        onPostVisit: n => result.Add(n),
                        onCycleDetected: OnCycle,
                        graph: graph);

                    if (cycleDetected)
                        return null; // Cycle detected, no valid topo sort
                }
            }

            result.Reverse();
            return result;
        }

        public static List<Node>? TopoKahn(Graph graph)
        {
            var inDegree = new Dictionary<Node, int>();
            var result = new List<Node>();
            var queue = new Queue<Node>();
            
            // Initialize in-degree of all nodes to 0
            foreach (var node in graph.Nodes)
                inDegree[node] = 0;
            
            // Count actual in-degrees from edge
            foreach (var node in graph.Nodes)
            {
                foreach (var neighbor in graph.GetNeighbors(node))
                {
                    if (inDegree.ContainsKey(neighbor))
                        inDegree[neighbor]++;
                }
            }

            // Enqueue all nodes with in-degree 0
            foreach (var (node, degree) in inDegree)
            {
                if (degree == 0)
                    queue.Enqueue(node);
            }
            
            // Process the queue
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                        queue.Enqueue(neighbor);
                }
            }
            
            // check if all nodes were processed
            return result.Count == graph.Nodes.Count ? result : null; // null = cycle detected
        }
    }       
}