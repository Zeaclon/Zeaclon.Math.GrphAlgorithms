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
    }
}