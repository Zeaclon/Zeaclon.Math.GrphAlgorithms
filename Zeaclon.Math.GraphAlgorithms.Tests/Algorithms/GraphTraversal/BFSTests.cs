using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;

namespace TestProject1.Algorithms.GraphTraversal
{
    public class BFSTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        private Node E => new Node(5, "E");

        [Fact]
        public void LinearGraph_DistanceCorrect()
        {
            // A - B - C
            var graph = new Graph();
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            var distances = BFS.Traverse(graph, A);

            Assert.Equal(0, distances[A]);
            Assert.Equal(1, distances[B]);
            Assert.Equal(2, distances[C]);
        }

        [Fact]
        public void BranchingGraph_DistanceCorrect()
        {
            //      A
            //    /   \
            //   B     C
            //    \   /
            //      D
            var graph = new Graph();
            graph.AddEdge(A, B);
            graph.AddEdge(A, C);
            graph.AddEdge(B, D);
            graph.AddEdge(C, D);

            var distances = BFS.Traverse(graph, A);

            Assert.Equal(0, distances[A]);
            Assert.Equal(1, distances[B]);
            Assert.Equal(1, distances[C]);
            Assert.Equal(2, distances[D]);
        }

        [Fact]
        public void DisconnectedGraph_OnlyReachableNodes()
        {
            // A - B   C - D
            var graph = new Graph();
            graph.AddEdge(A, B);
            graph.AddEdge(C, D);

            var distances = BFS.Traverse(graph, A);

            Assert.Equal(2, distances.Count);
            Assert.Contains(A, distances.Keys);
            Assert.Contains(B, distances.Keys);
            Assert.DoesNotContain(C, distances.Keys);
            Assert.DoesNotContain(D, distances.Keys);
        }

        [Fact]
        public void SingleNodeGraph_DistanceZero()
        {
            var graph = new Graph();
            var distances = BFS.Traverse(graph, A);

            Assert.Single(distances);
            Assert.Equal(0, distances[A]);
        }
    }
}