using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity;

namespace TestProject1.Algorithms.Connectivity
{
    public class TransitiveClosureTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");

        private Graph CreateGraph(params Node[] nodes) 
        {
            var g = new Graph();
            foreach (var n in nodes) g.AddNode(n);
            return g;
        }

        [Fact]
        public void SingleNodeGraph_ShouldReachItself()
        {
            var graph = CreateGraph(A);

            var dfsClosure = TransitiveClosure.DFSReachability(graph);
            var fwClosure = TransitiveClosure.FloydWarshallReachability(graph);

            Assert.Single(dfsClosure[A]);
            Assert.Contains(A, dfsClosure[A]);

            Assert.Single(fwClosure[A]);
            Assert.Contains(A, fwClosure[A]);
        }

        [Fact]
        public void LinearGraph_ShouldReachAllDownstreamNodes()
        {
            // A -> B -> C -> D
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, D);

            var dfsClosure = TransitiveClosure.DFSReachability(graph);
            var fwClosure = TransitiveClosure.FloydWarshallReachability(graph);

            Assert.Equal(new HashSet<Node> { A, B, C, D }, dfsClosure[A]);
            Assert.Equal(new HashSet<Node> { B, C, D }, dfsClosure[B]);
            Assert.Equal(new HashSet<Node> { C, D }, dfsClosure[C]);
            Assert.Equal(new HashSet<Node> { D }, dfsClosure[D]);

            // Compare DFS vs Floyd-Warshall
            foreach (var node in graph.Nodes)
            {
                Assert.Equal(dfsClosure[node], fwClosure[node]);
            }
        }

        [Fact]
        public void DisconnectedGraph_ShouldOnlyReachSelfOrComponent()
        {
            // A -> B, C -> D
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(C, D);

            var dfsClosure = TransitiveClosure.DFSReachability(graph);
            var fwClosure = TransitiveClosure.FloydWarshallReachability(graph);

            Assert.Equal(new HashSet<Node> { A, B }, dfsClosure[A]);
            Assert.Equal(new HashSet<Node> { B }, dfsClosure[B]);
            Assert.Equal(new HashSet<Node> { C, D }, dfsClosure[C]);
            Assert.Equal(new HashSet<Node> { D }, dfsClosure[D]);

            foreach (var node in graph.Nodes)
            {
                Assert.Equal(dfsClosure[node], fwClosure[node]);
            }
        }

        [Fact]
        public void CycleGraph_ShouldReachAllNodesInCycle()
        {
            // A -> B -> C -> A
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A);

            var dfsClosure = TransitiveClosure.DFSReachability(graph);
            var fwClosure = TransitiveClosure.FloydWarshallReachability(graph);

            var cycleNodes = new HashSet<Node> { A, B, C };
            foreach (var node in graph.Nodes)
            {
                Assert.Equal(cycleNodes, dfsClosure[node]);
                Assert.Equal(dfsClosure[node], fwClosure[node]);
            }
        }

        [Fact]
        public void ComplexGraph_ShouldMatchDFSAndFloydWarshall()
        {
            // Graph: A->B, B->C, C->A, B->D, D->C
            var graph = CreateGraph(A, B, C, D);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A);
            graph.AddEdge(B, D);
            graph.AddEdge(D, C);

            var dfsClosure = TransitiveClosure.DFSReachability(graph);
            var fwClosure = TransitiveClosure.FloydWarshallReachability(graph);

            foreach (var node in graph.Nodes)
            {
                Assert.Equal(dfsClosure[node], fwClosure[node]);
            }
        }
    }
}