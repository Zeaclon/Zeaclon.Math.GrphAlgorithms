namespace Zeaclon.Math.GraphAlgorithms.Core
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new();
        public List<Edge> Edges { get; } = new();
        public bool IsDirected { get; }

        public Graph(bool isDirected = true)
        {
            IsDirected = isDirected;
        }

        public void AddNode(Node node) => Nodes.Add(node);
        public void AddEdge(Node from, Node to, double weight = 1)
        {
            if (!Nodes.Contains(from)) Nodes.Add(from);
            if (!Nodes.Contains(to)) Nodes.Add(to);

            Edges.Add(new Edge(from, to, weight));
            if (!IsDirected)
                Edges.Add(new Edge(to, from, weight));
        }

        public IEnumerable<Edge> GetEdgesFrom(Node node) =>
            Edges.Where(e => e.From == node);

        public IEnumerable<Node> GetNeighbors(Node node) =>
            GetEdgesFrom(node).Select(e => e.To);

        public Graph Reverse()
        {
            if (!IsDirected) return this; // reversing undirected graph is pointless

            var reversed = new Graph(isDirected: true);
            foreach (var node in Nodes)
                reversed.AddNode(node);
            foreach (var edge in Edges)
                reversed.AddEdge(edge.To, edge.From, edge.Weight);

            return reversed;
        }
    }
}