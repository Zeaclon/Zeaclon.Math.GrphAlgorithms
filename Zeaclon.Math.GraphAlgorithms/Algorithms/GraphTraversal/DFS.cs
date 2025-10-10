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
            DFSVisit(start, visited, new HashSet<Node>(), 
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
        
        public static void DFSArticulation(
            Node node,
            Graph graph,
            HashSet<Node> visited,
            Dictionary<Node, int> discoveryTime,
            Dictionary<Node, int> lowTime,
            Dictionary<Node, Node?> parent,
            HashSet<Node> articulationPoints,
            ref int time)
        {
            visited.Add(node);
            discoveryTime[node] = lowTime[node] = time++;
            int children = 0;

            foreach (var edge in graph.GetEdgesFrom(node))
            {
                var neighbor = edge.To;

                if (!visited.Contains(neighbor))
                {
                    children++;
                    parent[neighbor] = node;
                    DFSArticulation(neighbor, graph, visited, discoveryTime, lowTime, parent, articulationPoints, ref time);

                    lowTime[node] = System.Math.Min(lowTime[node], lowTime[neighbor]);

                    // Articulation conditions
                    if (parent[node] is not null && lowTime[neighbor] >= discoveryTime[node])
                        articulationPoints.Add(node);

                    if (parent[node] is null && children > 1)
                        articulationPoints.Add(node); // Root with multiple children
                }
                else if (neighbor != parent.GetValueOrDefault(node))
                {
                    // Back edge
                    lowTime[node] = System.Math.Min(lowTime[node], discoveryTime[neighbor]);
                }
            }
        }
        
        public static void DFSBridge(
            Node node,
            Graph graph,
            HashSet<Node> visited,
            Dictionary<Node, int> discoveryTime,
            Dictionary<Node, int> lowTime,
            Dictionary<Node, Node?> parent,
            List<(Node, Node)> bridges,
            ref int time)
        {
            visited.Add(node);
            discoveryTime[node] = time;
            lowTime[node] = time;
            time++;

            foreach (var edge in graph.GetEdgesFrom(node))
            {
                var neighbor = edge.To;

                if (!visited.Contains(neighbor))
                {
                    parent[neighbor] = node;
                    DFSBridge(neighbor, graph, visited, discoveryTime, lowTime, parent, bridges, ref time);

                    // Update low time for the parent
                    lowTime[node] = System.Math.Min(lowTime[node], lowTime[neighbor]);

                    // Bridge condition
                    if (lowTime[neighbor] > discoveryTime[node])
                        bridges.Add((node, neighbor));
                }
                else if (neighbor != parent.GetValueOrDefault(node))
                {
                    // Back edge found
                    lowTime[node] = System.Math.Min(lowTime[node], discoveryTime[neighbor]);
                }
            }
        }

    }
}