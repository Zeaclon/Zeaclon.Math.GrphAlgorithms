using System;
using System.Collections.Generic;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal
{
    public static class DFS
    {
        /// <summary>
        /// General DFS traversal from a start node.
        /// Calls pre- and post-visit callbacks if provided.
        /// </summary>
        public static void TraverseWithCallbacks(
            Graph graph,
            Node start,
            Action<Node>? onPreVisit = null,
            Action<Node>? onPostVisit = null,
            Action<Node>? onCycleDetected = null)
        {
            var visited = new HashSet<Node>();
            var onStack = new HashSet<Node>();

            DFSVisit(start, visited, onStack, onPreVisit, onPostVisit, onCycleDetected, graph);
        }
        
        public static List<Node> Traverse(Graph graph, Node start)
        {
            var visited = new HashSet<Node>();
            var result = new List<Node>();
            DFS.DFSVisit(start, visited, new HashSet<Node>(), 
                onPreVisit: n => result.Add(n), 
                onPostVisit: null,
                onCycleDetected: null,
                graph: graph);
            return result;
        }


        /// <summary>
        /// Flexible DFS visit with cycle detection and callbacks.
        /// </summary>
        public static void DFSVisit(
            Node node,
            HashSet<Node> visited,
            HashSet<Node> onStack,
            Action<Node>? onPreVisit,
            Action<Node>? onPostVisit,
            Action<Node>? onCycleDetected,
            Graph graph)
        {
            if (onStack.Contains(node))
            {
                onCycleDetected?.Invoke(node);
                return;
            }

            if (visited.Contains(node)) return;

            visited.Add(node);
            onStack.Add(node);

            onPreVisit?.Invoke(node);

            foreach (var edge in graph.GetEdgesFrom(node))
                DFSVisit(edge.To, visited, onStack, onPreVisit, onPostVisit, onCycleDetected, graph);

            onStack.Remove(node);
            onPostVisit?.Invoke(node);
        }
    }
}