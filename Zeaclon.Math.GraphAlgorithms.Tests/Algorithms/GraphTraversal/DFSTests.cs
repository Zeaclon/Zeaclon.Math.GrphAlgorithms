using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.GraphTraversal;

namespace TestProject1.Algorithms.GraphTraversal
{
    public class DFSTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        private Node E => new Node(5, "E");

        [Fact]
        public void LinearGraph_TraversalOrderCorrect()
        {
            // A - B - C
            var graph = new Graph();
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            var result = DFS.Traverse(graph, A);

            // Should visit nodes in DFS order: A -> B -> C
            Assert.Equal(3, result.Count);
            Assert.Equal(A, result[0]);
            Assert.Equal(B, result[1]);
            Assert.Equal(C, result[2]);
        }

        [Fact]
        public void BranchingGraph_PrePostVisitCallbacks()
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

            var preOrder = new List<Node>();
            var postOrder = new List<Node>();

            DFS.TraverseWithCallbacks(
                graph,
                A,
                onPreVisit: n => preOrder.Add(n),
                onPostVisit: n => postOrder.Add(n)
            );

            // Pre-order should start with root
            Assert.Equal(A, preOrder[0]);
            Assert.Contains(B, preOrder);
            Assert.Contains(C, preOrder);
            Assert.Contains(D, preOrder);

            // Post-order should end with root
            Assert.Equal(A, postOrder[^1]);
            Assert.Contains(B, postOrder);
            Assert.Contains(C, postOrder);
            Assert.Contains(D, postOrder);
        }

        [Fact]
        public void CycleDetection_CallbackInvoked()
        {
            // Simple cycle: A -> B -> C -> A
            var graph = new Graph();
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A);

            var cycles = new List<Node>();

            DFS.TraverseWithCallbacks(
                graph,
                A,
                onCycleDetected: n => cycles.Add(n)
            );

            // At least one cycle should be detected
            Assert.NotEmpty(cycles);
            Assert.Contains(A, cycles);
        }

        [Fact]
        public void SingleNodeGraph_TraversalSingleNode()
        {
            var graph = new Graph();
            var result = DFS.Traverse(graph, A);

            Assert.Single(result);
            Assert.Equal(A, result[0]);
        }
    }
}