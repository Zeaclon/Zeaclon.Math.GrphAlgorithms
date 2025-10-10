using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity;

namespace TestProject1.Algorithms.Connectivity
{
    public class KosarajuSCCTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        private Node E => new Node(5, "E");

        private Graph CreateGraph(params Node[] nodes)
        {
            var g = new Graph();
            foreach (var n in nodes) g.AddNode(n);
            return g;
        }

        [Fact]
        public void SingleNodeGraph_ShouldReturnOneSCC()
        {
            var graph = CreateGraph(A);

            var sccs = KosarjuSCC.FindSCCs(graph);

            Assert.Single(sccs);
            Assert.Contains(A, sccs[0]);
        }

        [Fact]
        public void LinearGraph_ShouldReturnEachNodeAsSCC()
        {
            // A -> B -> C
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            var sccs = KosarjuSCC.FindSCCs(graph);

            Assert.Equal(3, sccs.Count);
            Assert.Contains(sccs, scc => scc.Contains(A));
            Assert.Contains(sccs, scc => scc.Contains(B));
            Assert.Contains(sccs, scc => scc.Contains(C));
        }

        [Fact]
        public void CycleGraph_ShouldReturnOneSCC()
        {
            // A -> B -> C -> A
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A);

            var sccs = KosarjuSCC.FindSCCs(graph);

            Assert.Single(sccs);
            Assert.Contains(A, sccs[0]);
            Assert.Contains(B, sccs[0]);
            Assert.Contains(C, sccs[0]);
        }

        [Fact]
        public void DisconnectedGraph_ShouldReturnMultipleSCCs()
        {
            // A -> B, C -> D
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(C, D);

            var sccs = KosarjuSCC.FindSCCs(graph);

            Assert.Equal(4, sccs.Count); // Each node is its own SCC
        }
    }
}