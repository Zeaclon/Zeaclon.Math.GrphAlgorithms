using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;

namespace TestProject1.Algorithms.ShortestPath
{
    public class BellmanFordTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");

        private Graph CreateSimpleGraph()
        {
            var graph = new Graph();
            graph.AddEdge(A, B, 1);
            graph.AddEdge(B, C, 2);
            graph.AddEdge(A, C, 4);
            graph.AddEdge(C, D, 1);
            return graph;
        }

        [Fact]
        public void ShortestPaths_SimpleGraph_ShouldReturnCorrectDistances()
        {
            var graph = CreateSimpleGraph();
            var distances = BellmanFord.ShortestPaths(graph, A);

            Assert.NotNull(distances);
            Assert.Equal(0, distances[A]);
            Assert.Equal(1, distances[B]);
            Assert.Equal(3, distances[C]); // via A->B->C
            Assert.Equal(4, distances[D]); // via A->B->C->D
        }

        [Fact]
        public void ShortestPaths_DisconnectedGraph_ShouldReturnInfinityForUnreachable()
        {
            var graph = new Graph();
            graph.AddNode(A);
            graph.AddNode(B);
            graph.AddNode(C); // C is disconnected
            graph.AddEdge(A, B, 5);

            var distances = BellmanFord.ShortestPaths(graph, A);

            Assert.NotNull(distances);
            Assert.Equal(0, distances[A]);
            Assert.Equal(5, distances[B]);
            Assert.Equal(double.PositiveInfinity, distances[C]);
        }

        [Fact]
        public void ShortestPaths_NegativeEdge_ShouldHandleCorrectly()
        {
            var graph = new Graph();
            graph.AddEdge(A, B, 4);
            graph.AddEdge(B, C, -2);
            graph.AddEdge(A, C, 5);

            var distances = BellmanFord.ShortestPaths(graph, A);

            Assert.NotNull(distances);
            Assert.Equal(0, distances[A]);
            Assert.Equal(4, distances[B]);
            Assert.Equal(2, distances[C]); // A->B->C gives 4 + (-2) = 2
        }

        [Fact]
        public void ShortestPaths_NegativeCycle_ShouldReturnNull()
        {
            var graph = new Graph();
            graph.AddEdge(A, B, 1);
            graph.AddEdge(B, C, -2);
            graph.AddEdge(C, A, -2); // Negative cycle: A->B->C->A total weight = -3

            var distances = BellmanFord.ShortestPaths(graph, A);
            Assert.Null(distances);
        }

        [Fact]
        public void ShortestPaths_SingleNodeGraph_ShouldReturnZeroDistance()
        {
            var graph = new Graph();
            graph.AddNode(A);

            var distances = BellmanFord.ShortestPaths(graph, A);

            Assert.NotNull(distances);
            Assert.Single(distances);
            Assert.Equal(0, distances[A]);
        }

        [Fact]
        public void ShortestPaths_EmptyGraph_ShouldReturnEmptyDictionary()
        {
            var graph = new Graph();
            var distances = BellmanFord.ShortestPaths(graph, A); // A not in graph

            Assert.NotNull(distances);
            Assert.Empty(distances);
        }
    }
}