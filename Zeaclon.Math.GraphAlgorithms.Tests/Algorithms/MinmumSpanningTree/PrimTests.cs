using Zeaclon.Math.GraphAlgorithms.Algorithms.MinimumSpanningTree;
using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Algorithms.MinimumSpanningTree
{
    public class PrimTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        
        [Fact]
        public void SimpleGraph_ShouldReturnCorrectMST()
        {
            var graph = new Graph(isDirected: false);
            graph.AddEdge(A, B, 1);
            graph.AddEdge(A, C, 3);
            graph.AddEdge(B, C, 2);

            var mst = Prim.MinimumSpanningTree(graph);

            Assert.Equal(2, mst.Count);
            Assert.Contains(mst, e => (e.From == A && e.To == B) || (e.From == B && e.To == A));
            Assert.Contains(mst, e => (e.From == B && e.To == C) || (e.From == C && e.To == B));
        }

    
    } 
}
