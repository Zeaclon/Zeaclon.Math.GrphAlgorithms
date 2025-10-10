using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;

namespace TestProject1.Algorithms.ShortestPath
{
    public class AStarTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        private Node E => new Node(5, "E");

        private Graph CreateSimpleGraph(bool isDirected = false)
        {
            var graph = new Graph(isDirected);
            graph.AddEdge(A, B, 1);
            graph.AddEdge(B, C, 2);
            graph.AddEdge(A, C, 4);
            graph.AddEdge(C, D, 1);
            return graph;
        }

        private double ZeroHeuristic(Node n) => 0;

        [Fact]
        public void FindPath_SimpleGraph_ShouldReturnCorrectPath()
        {
            var graph = CreateSimpleGraph();
            var path = AStar.FindPath(graph, A, D, ZeroHeuristic);

            Assert.NotNull(path);
            Assert.Equal(new List<Node> { A, B, C, D }, path);
        }

        [Fact]
        public void FindPath_NoPath_ShouldReturnNull()
        {
            var graph = CreateSimpleGraph();
            var isolated = new Node(6, "F");
            graph.AddNode(isolated);

            var path = AStar.FindPath(graph, A, isolated, ZeroHeuristic);
            Assert.Null(path);
        }

        [Fact]
        public void FindPath_SingleNodeGraph_ShouldReturnStartNode()
        {
            var graph = new Graph();
            graph.AddNode(A);

            var path = AStar.FindPath(graph, A, A, ZeroHeuristic);
            Assert.NotNull(path);
            Assert.Single(path);
            Assert.Equal(A, path[0]);
        }

        [Fact]
        public void FindBidirectionalPath_SimpleGraph_ShouldReturnCorrectPath()
        {
            var graph = CreateSimpleGraph();
            var path = AStar.FindBidirectionalPath(graph, A, D, ZeroHeuristic);

            Assert.NotNull(path);
            Assert.Equal(new List<Node> { A, B, C, D }, path);
        }

        [Fact]
        public void FindBidirectionalPath_NoPath_ShouldReturnNull()
        {
            var graph = CreateSimpleGraph();
            var isolated = new Node(6, "F");
            graph.AddNode(isolated);

            var path = AStar.FindBidirectionalPath(graph, A, isolated, ZeroHeuristic);
            Assert.Null(path);
        }

        [Fact]
        public void FindBidirectionalPath_SingleNodeGraph_ShouldReturnStartNode()
        {
            var graph = new Graph();
            graph.AddNode(A);

            var path = AStar.FindBidirectionalPath(graph, A, A, ZeroHeuristic);
            Assert.NotNull(path);
            Assert.Single(path);
            Assert.Equal(A, path[0]);
        }
    }
}
