using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.ShortestPath;

public class YenTests
{
    private Node A = new Node(1, "A");
    private Node B = new Node(2, "B");
    private Node C = new Node(3, "C");
    private Node D = new Node(4, "D");

    private Graph CreateSimpleGraph()
    {
        var graph = new Graph(isDirected: true);
        graph.AddEdge(A, B, 1);
        graph.AddEdge(B, C, 1);
        graph.AddEdge(A, C, 2);
        graph.AddEdge(C, D, 1);
        return graph;
    }

    private List<Node>? SimpleShortestPathFunc(Graph g, Node start, Node goal)
    {
        // Simple Dijkstra wrapper for testing
        return Dijkstra.ShortestPaths(g, start).TryGetValue(goal, out var d) && d < double.PositiveInfinity
            ? ReconstructPath(g, start, goal)
            : null;
    }

    private List<Node> ReconstructPath(Graph g, Node start, Node goal)
    {
        // Simple BFS-based reconstruction for test purposes
        var queue = new Queue<List<Node>>();
        queue.Enqueue(new List<Node> { start });
        var visited = new HashSet<Node> { start };

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var last = path.Last();
            if (last == goal) return path;

            foreach (var edge in g.GetEdgesFrom(last))
            {
                if (!visited.Contains(edge.To))
                {
                    visited.Add(edge.To);
                    var newPath = new List<Node>(path) { edge.To };
                    queue.Enqueue(newPath);
                }
            }
        }

        return new List<Node>();
    }

    [Fact]
    public void FindKShortestPaths_SinglePath_ShouldReturnOnePath()
    {
        var graph = new Graph();
        graph.AddEdge(A, B, 1);

        var paths = Yen.FindKShortestPaths(graph, A, B, 3, SimpleShortestPathFunc);

        Assert.Single(paths);
        Assert.Equal(new List<Node> { A, B }, paths[0].Nodes);
    }

    [Fact]
    public void FindKShortestPaths_MultiplePaths_ShouldReturnKPaths()
    {
        var graph = CreateSimpleGraph();

        var paths = Yen.FindKShortestPaths(graph, A, C, 2, SimpleShortestPathFunc);

        Assert.NotEmpty(paths);
        Assert.True(paths.Count <= 2);
        Assert.Contains(paths, p => p.Nodes.SequenceEqual(new List<Node> { A, C }));
        Assert.Contains(paths, p => p.Nodes.SequenceEqual(new List<Node> { A, B, C }));
    }

    [Fact]
    public void FindKShortestPaths_KGreaterThanAvailable_ShouldReturnAllPaths()
    {
        var graph = CreateSimpleGraph();

        var paths = Yen.FindKShortestPaths(graph, A, C, 10, SimpleShortestPathFunc);

        Assert.NotEmpty(paths);
        Assert.All(paths, p =>
        {
            Assert.Equal(A, p.Nodes.First());
            Assert.Equal(C, p.Nodes.Last());
        });
    }

    [Fact]
    public void FindKShortestPaths_NoPath_ShouldReturnEmptyList()
    {
        var graph = CreateSimpleGraph();
        var disconnectedNode = new Node(99, "X");
        graph.AddNode(disconnectedNode);

        var paths = Yen.FindKShortestPaths(graph, A, disconnectedNode, 3, SimpleShortestPathFunc);

        Assert.Empty(paths);
    }

    [Fact]
    public void FindKShortestPaths_PathWithIntermediateNodes_ShouldPreserveOrder()
    {
        var graph = CreateSimpleGraph();

        var paths = Yen.FindKShortestPaths(graph, A, D, 3, SimpleShortestPathFunc);

        Assert.NotEmpty(paths);
        foreach (var path in paths)
        {
            Assert.Equal(A, path.Nodes.First());
            Assert.Equal(D, path.Nodes.Last());
        }
    }
}