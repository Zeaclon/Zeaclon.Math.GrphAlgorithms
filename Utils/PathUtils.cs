using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Utils;

public class PathUtils
{
    public static double CalculatePathCost(Graph graph, List<Node> path)
    {
        double cost = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            var edge = graph.Edges.FirstOrDefault(e => e.From == path[i] && e.To == path[i + 1]);
            if (edge == null)
                throw new Exception($"Missing edge from {path[i]} to {path[i+1]}.");
            cost += edge.Weight;
        }
        return cost;
    }
}