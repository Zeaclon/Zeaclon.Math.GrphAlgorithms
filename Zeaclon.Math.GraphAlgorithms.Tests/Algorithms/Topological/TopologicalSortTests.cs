using Zeaclon.Math.GraphAlgorithms.Algorithms.Topological;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.Topological;

public class TopologicalSortTests
{
    private Node A = new Node(1, "A");
    private Node B = new Node(2, "B");
    private Node C = new Node(3, "C");
    private Node D = new Node(4, "D");

    private Graph CreateSimpleDAG()
    {
        var g = new Graph();
        g.AddNode(A);
        g.AddNode(B);
        g.AddNode(C);
        g.AddNode(D);

        g.AddEdge(A, B, 1);
        g.AddEdge(A, C, 1);
        g.AddEdge(B, D, 1);
        g.AddEdge(C, D, 1);

        return g;
    }

    private Graph CreateGraphWithCycle()
    {
        var g = CreateSimpleDAG();
        g.AddEdge(D, A, 1); // introduces cycle
        return g;
    }

    [Fact]
    public void TopoDFS_EmptyGraph_ShouldReturnEmptyList()
    {
        var g = new Graph();
        var result = TopologicalSort.TopoDFS(g);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void TopoDFS_SingleNode_ShouldReturnNode()
    {
        var g = new Graph();
        g.AddNode(A);
        var result = TopologicalSort.TopoDFS(g);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(A, result[0]);
    }

    [Fact]
    public void TopoDFS_DAG_ShouldReturnValidTopologicalOrder()
    {
        var g = CreateSimpleDAG();
        var result = TopologicalSort.TopoDFS(g);
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);

        // Check ordering constraints
        Assert.True(result.IndexOf(A) < result.IndexOf(B));
        Assert.True(result.IndexOf(A) < result.IndexOf(C));
        Assert.True(result.IndexOf(B) < result.IndexOf(D));
        Assert.True(result.IndexOf(C) < result.IndexOf(D));
    }

    [Fact]
    public void TopoDFS_Cycle_ShouldReturnNull()
    {
        var g = CreateGraphWithCycle();
        var result = TopologicalSort.TopoDFS(g);
        Assert.Null(result);
    }

    [Fact]
    public void TopoKahn_EmptyGraph_ShouldReturnEmptyList()
    {
        var g = new Graph();
        var result = TopologicalSort.TopoKahn(g);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void TopoKahn_SingleNode_ShouldReturnNode()
    {
        var g = new Graph();
        g.AddNode(A);
        var result = TopologicalSort.TopoKahn(g);
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(A, result[0]);
    }

    [Fact]
    public void TopoKahn_DAG_ShouldReturnValidTopologicalOrder()
    {
        var g = CreateSimpleDAG();
        var result = TopologicalSort.TopoKahn(g);
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);

        // Check ordering constraints
        Assert.True(result.IndexOf(A) < result.IndexOf(B));
        Assert.True(result.IndexOf(A) < result.IndexOf(C));
        Assert.True(result.IndexOf(B) < result.IndexOf(D));
        Assert.True(result.IndexOf(C) < result.IndexOf(D));
    }

    [Fact]
    public void TopoKahn_Cycle_ShouldReturnNull()
    {
        var g = CreateGraphWithCycle();
        var result = TopologicalSort.TopoKahn(g);
        Assert.Null(result);
    }
}