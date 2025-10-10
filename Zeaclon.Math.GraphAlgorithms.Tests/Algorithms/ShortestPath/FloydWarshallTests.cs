using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.ShortestPath;

public class FloydWarshallTests
{
    private Node A = new Node(1, "A");
    private Node B = new Node(2, "B");
    private Node C = new Node(3, "C");
    private Node D = new Node(4, "D");

    private Graph CreateSimpleGraph()
    {
        var graph = new Graph(isDirected: true);
        graph.AddEdge(A, B, 1);
        graph.AddEdge(B, C, 2);
        graph.AddEdge(A, C, 4);
        return graph;
    }

    [Fact]
    public void AllPairsShortestPaths_SingleNodeGraph_ShouldReturnZeroDistance()
    {
        var graph = new Graph();
        graph.AddNode(A);

        var result = FloydWarshall.AllPairsShortestPaths(graph);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(0, result[A][A]);
    }

    [Fact]
    public void AllPairsShortestPaths_SimpleGraph_ShouldReturnCorrectDistances()
    {
        var graph = CreateSimpleGraph();
        var result = FloydWarshall.AllPairsShortestPaths(graph);

        Assert.NotNull(result);
        Assert.Equal(1, result[A][B]);
        Assert.Equal(3, result[A][C]); // Shortest path A->B->C
        Assert.Equal(double.PositiveInfinity, result[B][A]); // no path
    }

    [Fact]
    public void AllPairsShortestPaths_DisconnectedGraph_ShouldReturnInfinityForUnreachable()
    {
        var graph = CreateSimpleGraph();
        graph.AddNode(D); // D is disconnected

        var result = FloydWarshall.AllPairsShortestPaths(graph);

        Assert.NotNull(result);
        Assert.Equal(double.PositiveInfinity, result[A][D]);
        Assert.Equal(double.PositiveInfinity, result[D][A]);
    }

    [Fact]
    public void AllPairsShortestPaths_NegativeCycle_ShouldReturnNull()
    {
        var graph = new Graph();
        graph.AddEdge(A, B, 1);
        graph.AddEdge(B, C, -2);
        graph.AddEdge(C, A, -2); // negative cycle

        var result = FloydWarshall.AllPairsShortestPaths(graph);

        Assert.Null(result);
    }

    [Fact]
    public void AllPairsShortestPaths_PositiveAndNegativeWeights_ShouldComputeCorrectly()
    {
        var graph = new Graph();
        graph.AddEdge(A, B, 3);
        graph.AddEdge(B, C, -1);
        graph.AddEdge(A, C, 10);

        var result = FloydWarshall.AllPairsShortestPaths(graph);

        Assert.NotNull(result);
        Assert.Equal(3, result[A][B]);
        Assert.Equal(2, result[A][C]); // via B: 3 + (-1)
        Assert.Equal(-1, result[B][C]);
    }
}