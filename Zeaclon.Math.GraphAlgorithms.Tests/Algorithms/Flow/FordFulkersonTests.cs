using Zeaclon.Math.GraphAlgorithms.Algorithms.Flow;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.Flow
{
    public class FordFulkersonTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        private Node E => new Node(5, "E");

        [Fact]
        public void SimpleGraph_MaxFlow_Correct()
        {
            // A -> B -> C
            var graph = new Graph();
            graph.AddEdge(A, B, 3);
            graph.AddEdge(B, C, 2);

            double maxFlow = FordFulkerson.MaxFlow(graph, A, C);

            Assert.Equal(2, maxFlow); // Bottleneck is 2
        }

        [Fact]
        public void MultiplePaths_MaxFlow_Correct()
        {
            //      A
            //    /   \
            //   B     C
            //    \   /
            //      D
            var graph = new Graph();
            graph.AddEdge(A, B, 3);
            graph.AddEdge(A, C, 2);
            graph.AddEdge(B, D, 2);
            graph.AddEdge(C, D, 3);

            double maxFlow = FordFulkerson.MaxFlow(graph, A, D);

            Assert.Equal(4, maxFlow); // Max flow: A->B->D=2, A->C->D=2
        }

        [Fact]
        public void GraphWithNoPath_MaxFlowZero()
        {
            var graph = new Graph();
            graph.AddEdge(A, B, 3);
            graph.AddEdge(C, D, 5);

            double maxFlow = FordFulkerson.MaxFlow(graph, A, D);

            Assert.Equal(0, maxFlow); // No path from A to D
        }

        [Fact]
        public void GraphWithMultipleEdges_MaxFlowCorrect()
        {
            // A -> B (10)
            // A -> C (5)
            // B -> C (15)
            // B -> D (10)
            // C -> D (10)
            var graph = new Graph();
            graph.AddEdge(A, B, 10);
            graph.AddEdge(A, C, 5);
            graph.AddEdge(B, C, 15);
            graph.AddEdge(B, D, 10);
            graph.AddEdge(C, D, 10);

            double maxFlow = FordFulkerson.MaxFlow(graph, A, D);

            Assert.Equal(15, maxFlow); // Max flow from A to D
        }
    }
}