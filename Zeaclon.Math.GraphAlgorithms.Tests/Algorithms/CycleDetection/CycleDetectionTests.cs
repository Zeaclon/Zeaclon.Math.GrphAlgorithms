using Zeaclon.Math.GraphAlgorithms.Algorithms.CycleDetection;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.CycleDetection
{
    public class CycleDetectorTests
    {
        private Node A = new Node(1, "A");
        private Node B = new Node(2, "B");
        private Node C = new Node(3, "C");
        private Node D = new Node(4, "D");

        private Graph CreateGraph(params Node[] nodes)
        {
            var g = new Graph();
            foreach (var node in nodes)
                g.AddNode(node);
            return g;
        }

        [Fact]
        public void DirectedGraph_WithCycle_ShouldDetectCycle()
        {
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A); // cycle

            Assert.True(CycleDetector.HasCycleDirected(graph));
        }

        [Fact]
        public void DirectedGraph_WithoutCycle_ShouldReturnFalse()
        {
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            Assert.False(CycleDetector.HasCycleDirected(graph));
        }

        [Fact]
        public void UndirectedGraph_WithCycle_ShouldDetectCycle()
        {
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A); // cycle

            Assert.True(CycleDetector.HasCycleUndirected(graph));
        }

        [Fact]
        public void UndirectedGraph_WithoutCycle_ShouldReturnFalse()
        {
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            Assert.False(CycleDetector.HasCycleUndirected(graph));
        }

        [Fact]
        public void UndirectedGraph_DisconnectedCycle_ShouldDetectCycle()
        {
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A); // cycle in one component
            graph.AddEdge(D, D); // single node with self-loop

            Assert.True(CycleDetector.HasCycleUndirected(graph));
        }

        [Fact]
        public void DirectedGraph_DisconnectedComponents_WithAndWithoutCycle()
        {
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(B, A); // cycle
            graph.AddEdge(C, D); // acyclic component

            Assert.True(CycleDetector.HasCycleDirected(graph));
        }
    }
}