using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;

namespace TestProject1.Algorithms.ShortestPath
{
    public class AltTests
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
        public void FindPath_WithoutInitialization_ShouldThrow()
        {
            Alt.Reset();
            var graph = CreateSimpleGraph();
            Assert.Throws<InvalidOperationException>(() => Alt.FindPath(graph, A, D));
        }

        [Fact]
        public void FindPath_ShouldReturnCorrectPath()
        {
            var graph = CreateSimpleGraph();
            Alt.Initialize(graph, new List<Node> { A, C });

            var path = Alt.FindPath(graph, A, D);

            Assert.NotNull(path);
            Assert.Equal(new List<Node> { A, B, C, D }, path);
        }

        [Fact]
        public void FindPath_StartEqualsGoal_ShouldReturnSingleNode()
        {
            var graph = CreateSimpleGraph();
            Alt.Initialize(graph, new List<Node> { A });

            var path = Alt.FindPath(graph, B, B);

            Assert.NotNull(path);
            Assert.Single(path);
            Assert.Equal(B, path[0]);
        }

        [Fact]
        public void FindPath_UnreachableNode_ShouldReturnNull()
        {
            var graph = CreateSimpleGraph();
            graph.AddNode(new Node(5, "E")); // isolated node
            Alt.Initialize(graph, new List<Node> { A, C });

            var path = Alt.FindPath(graph, A, new Node(5, "E"));

            Assert.Null(path);
        }
    }
}