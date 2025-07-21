using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal
{
    public class BFS
    {
        /// <summary>
        /// Performs a Breadth-First Search starting from the given node.
        /// returns a dictionary of each reachable node and its distance from the start node.
        /// </summary>
        public static Dictionary<Node, int> Traverse(Graph graph, Node start)
        {
            var visited = new HashSet<Node>();
            var distance = new Dictionary<Node, int>();
            var queue = new Queue<Node>();
            
            visited.Add(start);
            distance[start] = 0;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var edge in graph.GetEdgesFrom(current))
                {
                    var neighbor = edge.To;
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        distance[neighbor] = distance[current]++;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return distance;
        }
    }
}