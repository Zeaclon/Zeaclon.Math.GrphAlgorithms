using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity;

namespace TestProject1.Algorithms.Connectivity
{
    public class BridgeFinderTests
    {
        private Graph CreateGraph(params Node[] nodes)
        {
            var graph = new Graph();
            foreach (var node in nodes)
                graph.AddNode(node);
            return graph;
        }

        private Node A = new Node(1, "A");
        private Node B = new Node(2, "B");
        private Node C = new Node(3, "C");
        private Node D = new Node(4, "D");

        [Fact]
        public void LinearGraph_AllEdgesAreBridges()
        {
            // A - B - C
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            var bridges = BridgeFinder.FindBridges(graph);

            Assert.Equal(2, bridges.Count);
            Assert.Contains((A, B), bridges);
            Assert.Contains((B, C), bridges);
        }

        [Fact]
        public void CycleGraph_NoBridges()
        {
            // A - B - C - A
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A);

            var bridges = BridgeFinder.FindBridges(graph);

            Assert.Empty(bridges);
        }

        [Fact]
        public void SingleBridgeGraph_ShouldDetectBridge()
        {
            // A - B   C
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);

            var bridges = BridgeFinder.FindBridges(graph);

            Assert.Single(bridges);
            Assert.Contains((A, B), bridges);
        }

        [Fact]
        public void DisconnectedGraph_MultipleComponents()
        {
            // Component 1: A - B
            // Component 2: C - D
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(C, D);

            var bridges = BridgeFinder.FindBridges(graph);

            Assert.Equal(2, bridges.Count);
            Assert.Contains((A, B), bridges);
            Assert.Contains((C, D), bridges);
        }
    }
}