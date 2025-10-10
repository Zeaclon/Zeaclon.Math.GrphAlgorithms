namespace Zeaclon.Math.GraphAlgorithms.Core
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new();
        public List<Edge> Edges { get; } = new();

        public void AddNode(Node node) => Nodes.Add(node);

        public void AddEdge(Node from, Node to, double weight = 1)
        {
            Edges.Add(new Edge(from, to, weight));
        }
        
        public IEnumerable<Edge> GetEdgesFrom(Node node) =>
            Edges.Where(e => e.From == node);
        
        public IEnumerable<Node> GetNeighbors(Node node) =>
            GetEdgesFrom(node).Select(e => e.To);

        public Graph Reverse()
        {
            var reversed = new Graph();
            
            // add all nodes first
            foreach (var node in Nodes)
                reversed.AddNode(node);
            
            // reverse the edges
            foreach (var edge in Edges)
                reversed.AddEdge(edge.To, edge.From, edge.Weight);
            
            return reversed;
        }
    }
}