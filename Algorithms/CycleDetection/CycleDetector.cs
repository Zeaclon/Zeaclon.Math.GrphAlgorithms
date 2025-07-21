using System.Collections.Generic;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.CycleDetection
{
    public static class CycleDetector
    {
        /// <summary>
        /// Detects cycles in a directed graph using DFS and recursion stack.
        /// Returns true if a cycle exists.
        /// </summary>
        public static bool HasCycleDirected(Graph graph)
        {
            var visited = new HashSet<Node>();
            var onStack = new HashSet<Node>();

            bool Dfs(Node node)
            {
                if (onStack.Contains(node)) return true;
                if (visited.Contains(node)) return false;

                visited.Add(node);
                onStack.Add(node);

                foreach (var edge in graph.GetEdgesFrom(node))
                {
                    if (Dfs(edge.To)) return true;
                }

                onStack.Remove(node);
                return false;
            }

            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    if (Dfs(node)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Detects cycles in an undirected graph using DFS.
        /// Returns true if a cycle exists.
        /// </summary>
        public static bool HasCycleUndirected(Graph graph)
        {
            var visited = new HashSet<Node>();

            bool Dfs(Node node, Node? parent)
            {
                visited.Add(node);

                foreach (var edge in graph.GetEdgesFrom(node))
                {
                    var neighbor = edge.To;
                    if (!visited.Contains(neighbor))
                    {
                        if (Dfs(neighbor, node)) return true;
                    }
                    else if (neighbor != parent)
                    {
                        // Visited neighbor that is not parent => cycle
                        return true;
                    }
                }

                return false;
            }

            foreach (var node in graph.Nodes)
            {
                if (!visited.Contains(node))
                {
                    if (Dfs(node, null)) return true;
                }
            }
            return false;
        }
    }
}
