using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Extensions;

namespace TestProject1.Extensions
{
    public class GraphExtensionsTests
    {
        [Fact]
        public void Clone_ShouldCreateDeepCopy()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddEdge(nodeA, nodeB, 5);

            var clone = graph.Clone();

            // Check nodes and edges are the same
            Assert.Equal(graph.Nodes.Count, clone.Nodes.Count);
            Assert.Equal(graph.Edges.Count, clone.Edges.Count);

            // Mutate clone and ensure original stays unchanged
            clone.AddNode(new Node(3, "C"));
            clone.AddEdge(nodeB, nodeA, 10);

            Assert.Equal(2, graph.Nodes.Count);
            Assert.Equal(1, graph.Edges.Count);
        }

        [Fact]
        public void RemoveNode_ShouldRemoveNodeAndConnectedEdges()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");
            var nodeC = new Node(3, "C");

            var graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddEdge(nodeA, nodeB, 5);
            graph.AddEdge(nodeB, nodeC, 7);

            graph.RemoveNode(nodeB);

            Assert.DoesNotContain(nodeB, graph.Nodes);
            Assert.DoesNotContain(graph.Edges, e => e.From == nodeB || e.To == nodeB);
            Assert.Equal(2, graph.Nodes.Count);
            Assert.Empty(graph.Edges);
        }

        [Fact]
        public void RemoveEdge_ShouldRemoveOnlySpecifiedEdge()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");
            var nodeC = new Node(3, "C");

            var graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddEdge(nodeA, nodeB, 5);
            graph.AddEdge(nodeB, nodeC, 7);

            graph.RemoveEdge(nodeA, nodeB);

            Assert.Empty(graph.Edges.Where(e => e.From == nodeA && e.To == nodeB));
            Assert.Single(graph.Edges);
        }

        [Fact]
        public void GetEdgeCapacity_ShouldReturnCorrectValue()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddEdge(nodeA, nodeB, 8);

            Assert.Equal(8, graph.GetEdgeCapacity(nodeA, nodeB));
            Assert.Equal(0, graph.GetEdgeCapacity(nodeB, nodeA)); // missing edge
        }

        [Fact]
        public void UpdateEdgeCapacity_ShouldUpdateOrAddEdge()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);

            // Add new edge
            graph.UpdateEdgeCapacity(nodeA, nodeB, 10);
            Assert.Single(graph.Edges);
            Assert.Equal(10, graph.GetEdgeCapacity(nodeA, nodeB));

            // Update existing ed
        }
    }
}