using Zeaclon.Math.GraphAlgorithms.Utils;

namespace Zeaclon.Math.GraphAlgorithms.Core
{
    public class Path
    {
        public List<Node> Nodes { get; }
        public double Cost { get; }

        public Path(IEnumerable<Node> nodes, double cost)
        {
            Nodes = nodes.ToList();
            Cost = cost;
        }

        public Path(List<Node> nodes, Graph graph)
        {
            Nodes = nodes;
            Cost = PathUtils.CalculatePathCost(graph, nodes);
        }

        public int Length => Nodes.Count;

        public override string ToString()
        {
            return $"Path (Cost: {Cost}, Length: {Length}): {string.Join(" -> ", Nodes.Select(n => n.Id))}";
        }

        public Path Clone()
        {
            return new Path(new List<Node>(Nodes), Cost);
        }

        public bool Equals(Path other)
        {
            if (other == null || Nodes.Count != other.Nodes.Count) return false;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (!Nodes[i].Equals(other.Nodes[i])) return false;
            }
            return true;
        }
    }
}