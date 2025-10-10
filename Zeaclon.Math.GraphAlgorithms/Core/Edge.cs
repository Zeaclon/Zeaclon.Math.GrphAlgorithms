namespace Zeaclon.Math.GraphAlgorithms.Core
{
    public class Edge(Node from, Node to, double weight = 1)
    {
        public Node From { get; } = from;
        public Node To { get; } = to;
        public double Weight { get; } = weight;
    }
}