using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.MinimumSpanningTree;

namespace TestProject1.Algorithms.MinimumSpanningTree
{
    public class KruskalTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");

        [Fact]
        public void SimpleGraph_ShouldReturnCorrectMST()
        {
            // Graph:
            //    A
            //   / \
            // 1/   \3
            // B-----C
            //   2
            // D isolated will be ignored in connected assumption

            var graph = new Graph(isDirected: false);

            graph.AddNode(A);
            graph.AddNode(B);
            graph.AddNode(C);
            // D is optional since it's isolated

            graph.AddEdge(A, B, 1);
            graph.AddEdge(A, C, 3);
            graph.AddEdge(B, C, 2);


            var mst = Kruskal.MinimumSpanningTree(graph);

            // MST should have 2 edges (n-1)
            Assert.Equal(2, mst.Count);

            // MST edges should be the lowest weights without forming a cycle
            Assert.Contains(mst, e => (e.From == A && e.To == B) || (e.From == B && e.To == A));
            Assert.Contains(mst, e => (e.From == B && e.To == C) || (e.From == C && e.To == B));
        }

        [Fact]
        public void EmptyGraph_ShouldReturnEmptyMST()
        {
            var graph = new Graph(isDirected: false);
            var mst = Kruskal.MinimumSpanningTree(graph);
            Assert.Empty(mst);
        }

        [Fact]
        public void SingleNodeGraph_ShouldReturnEmptyMST()
        {
            var graph = new Graph(isDirected: false);
            graph.AddNode(A);
            var mst = Kruskal.MinimumSpanningTree(graph);
            Assert.Empty(mst);
        }
    }
}