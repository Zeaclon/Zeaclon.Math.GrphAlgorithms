using Zeaclon.Math.GraphAlgorithms.Utils;

namespace Zeaclon.Math.GraphAlgorithms.Core
{
    public record Path
    {
        public IReadOnlyList<Node> Nodes { get; }
        public double Cost { get; }

        public Path(IEnumerable<Node> nodes, double cost)
        {
            Nodes = nodes.ToList();
            Cost = cost;
        }

        public Path(IEnumerable<Node> nodes, Graph graph)
        {
            var nodeList = nodes.ToList();
            Nodes = nodeList.AsReadOnly();
            Cost = PathUtils.CalculatePathCost(graph, nodeList);
        }

        public int Length => Nodes.Count;

        public override string ToString()
        {
            return $"Path (Cost: {Cost}, Length: {Length}): {string.Join(" -> ", Nodes.Select(n => n.Id))}";
        }
    }
}