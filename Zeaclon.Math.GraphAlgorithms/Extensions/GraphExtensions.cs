using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Extensions
{
    public static class GraphExtensions
    {
        public static Graph Clone(this Graph graph)
        {
            var clone = new Graph();

            // Add nodes (assuming Node is a reference type and immutable or handled accordingly)
            foreach (var node in graph.Nodes)
                clone.AddNode(node);

            // Add edges
            foreach (var edge in graph.Edges)
                clone.AddEdge(edge.From, edge.To, edge.Weight);

            return clone;
        }

        public static void RemoveEdge(this Graph graph, Node from, Node to)
        {
            var edge = graph.Edges.FirstOrDefault(e => e.From == from && e.To == to);
            if (edge != null)
                graph.Edges.Remove(edge);
        }

        public static void RemoveNode(this Graph graph, Node node)
        {
            // Remove edges connected to this node
            graph.Edges.RemoveAll(e => e.From == node || e.To == node);

            // Remove the node itself
            graph.Nodes.Remove(node);
        }
        
        public static double GetEdgeCapacity(this Graph graph, Node from, Node to)
        {
            var edge = graph.Edges.FirstOrDefault(e => e.From == from && e.To == to);
            return edge?.Weight ?? 0;
        }

        public static void UpdateEdgeCapacity(this Graph graph, Node from, Node to, double newCapacity)
        {
            var edge = graph.Edges.FirstOrDefault(e => e.From == from && e.To == to);
            if (edge != null)
            {
                graph.Edges.Remove(edge);
                if (newCapacity > 0)
                    graph.AddEdge(from, to, newCapacity);
            }
            else if (newCapacity > 0)
            {
                graph.AddEdge(from, to, newCapacity);
            }
        }
    }
}