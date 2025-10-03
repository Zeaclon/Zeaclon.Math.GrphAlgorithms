using Zeaclon.Math.GraphAlgorithms.Algorithms.CycleDetection;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Topological
{
    public class TopologicalSort
    {
        public static List<Node>? TopoDFS(Graph graph)
        {
            if (CycleDetector.HasCycleDirected(graph))
                return null; // cycle detected, no topo order possible
            
            // proceed with DFS-based topo sort (without cycle detection, since already checked)
            var visited = new HashSet<Node>();
            var result = new List<Node>();

            void DFSVisit(Node node)
            {
                if (visited.Contains(node)) return;
                visited.Add(node);
                foreach (var edge in graph.GetEdgesFrom(node))
                    DFSVisit(edge.To);
                result.Add(node);
            }

            foreach (var node in graph.Nodes)
                DFSVisit(node);

            result.Reverse();
            return result;
        }

        public static List<Node>? TopoKahn(Graph graph)
        {
            if (CycleDetector.HasCycleDirected(graph))
                return null; // Early cycle detection

            var inDegree = new Dictionary<Node, int>();
            var result = new List<Node>();
            var queue = new Queue<Node>();

            foreach (var node in graph.Nodes)
                inDegree[node] = 0;

            foreach (var node in graph.Nodes)
            {
                foreach (var neighbor in graph.GetNeighbors(node))
                {
                    if (inDegree.ContainsKey(neighbor))
                        inDegree[neighbor]++;
                }
            }

            foreach (var (node, degree) in inDegree)
            {
                if (degree == 0)
                    queue.Enqueue(node);
            }

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

            return result.Count == graph.Nodes.Count ? result : null;
        }
    }       
}