using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Utils;

namespace TestProject1.Utils;

public class PathUtilTests
{
    
    [Fact]
    public void PathUtils_CalculatePathCost_ShouldSumEdgeWeights()
    {
        var nodeA = new Node(1, "A");
        var nodeB = new Node(2, "B");
        var nodeC = new Node(3, "C");

        var graph = new Graph();
        graph.AddNode(nodeA);
        graph.AddNode(nodeB);
        graph.AddNode(nodeC);
        graph.AddEdge(nodeA, nodeB, 5);
        graph.AddEdge(nodeB, nodeC, 7);

        var path = new List<Node> { nodeA, nodeB, nodeC };
        double cost = PathUtils.CalculatePathCost(graph, path);

        Assert.Equal(12, cost);
    }

    [Fact]
    public void PathUtils_CalculatePathCost_ShouldThrowIfMissingEdge()
    {
        var nodeA = new Node(1, "A");
        var nodeB = new Node(2, "B");
        var nodeC = new Node(3, "C");

        var graph = new Graph();
        graph.AddNode(nodeA);
        graph.AddNode(nodeB);
        graph.AddNode(nodeC);
        graph.AddEdge(nodeA, nodeB, 5);
        // Missing edge B -> C

        var path = new List<Node> { nodeA, nodeB, nodeC };

        var ex = Assert.Throws<Exception>(() => PathUtils.CalculatePathCost(graph, path));
        Assert.Contains("Missing edge from", ex.Message);
    }

}