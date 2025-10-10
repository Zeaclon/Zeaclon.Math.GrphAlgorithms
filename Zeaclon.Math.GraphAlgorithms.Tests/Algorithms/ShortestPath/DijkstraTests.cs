using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.ShortestPath;

public class DijkstraTests
{
    private Node A = new Node(1, "A");
    private Node B = new Node(2, "B");
    private Node C = new Node(3, "C");
    private Node D = new Node(4, "D");

    private Graph CreateSimpleGraph()
    {
        var graph = new Graph(isDirected: false);
        graph.AddEdge(A, B, 1);
        graph.AddEdge(B, C, 2);
        graph.AddEdge(A, C, 4);
        return graph;
    }

    [Fact]
    public void ShortestPaths_SingleNodeGraph_ShouldReturnZeroDistance()
    {
        var graph = new Graph();
        graph.AddNode(A);

        var distances = Dijkstra.ShortestPaths(graph, A);

        Assert.Single(distances);
        Assert.Equal(0, distances[A]);
    }

    [Fact]
    public void ShortestPaths_SimpleGraph_ShouldReturnCorrectDistances()
    {
        var graph = CreateSimpleGraph();
        var distances = Dijkstra.ShortestPaths(graph, A);

        Assert.Equal(3, distances[C]); // Shortest path A->B->C = 1 + 2 = 3
        Assert.Equal(1, distances[B]);
        Assert.Equal(0, distances[A]);
    }

    [Fact]
    public void ShortestPaths_DisconnectedGraph_ShouldReturnInfinityForUnreachableNodes()
    {
        var graph = CreateSimpleGraph();
        graph.AddNode(D); // D is disconnected

        var distances = Dijkstra.ShortestPaths(graph, A);

        Assert.Equal(double.PositiveInfinity, distances[D]);
    }

    [Fact]
    public void BidirectionalShortestPath_SingleNode_ShouldReturnZero()
    {
        var graph = new Graph();
        graph.AddNode(A);

        var (dist, path) = Dijkstra.BidirectionalShortestPath(graph, A, A);

        Assert.Equal(0, dist);
        Assert.Single(path);
        Assert.Equal(A, path[0]);
    }

    [Fact]
    public void BidirectionalShortestPath_SimpleGraph_ShouldReturnCorrectPathAndDistance()
    {
        var graph = CreateSimpleGraph();
        var (dist, path) = Dijkstra.BidirectionalShortestPath(graph, A, C);

        Assert.Equal(3, dist);
        Assert.Equal(new List<Node> { A, B, C }, path);
    }

    [Fact]
    public void BidirectionalShortestPath_DisconnectedGraph_ShouldReturnInfinityAndEmptyPath()
    {
        var graph = CreateSimpleGraph();
        graph.AddNode(D);

        var (dist, path) = Dijkstra.BidirectionalShortestPath(graph, A, D);

        Assert.Equal(double.PositiveInfinity, dist);
        Assert.Empty(path);
    }
}