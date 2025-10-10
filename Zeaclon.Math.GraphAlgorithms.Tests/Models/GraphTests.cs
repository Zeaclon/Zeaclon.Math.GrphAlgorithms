using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Models
{
    public class GraphTests
    {
        [Fact]
        public void AddNode_ShouldAddNodeToGraph()
        {
            var graph = new Graph();
            var node = new Node(1, "A");

            graph.AddNode(node);

            Assert.Contains(node, graph.Nodes);
        }

        [Fact]
        public void AddEdge_ShouldAddEdgeToGraph()
        {
            var graph = new Graph();
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);

            graph.AddEdge(nodeA, nodeB, 5);

            var edge = graph.Edges.First();
            Assert.Equal(nodeA, edge.From);
            Assert.Equal(nodeB, edge.To);
            Assert.Equal(5, edge.Weight);
        }

        [Fact]
        public void GetEdgesFrom_ShouldReturnCorrectEdges()
        {
            var graph = new Graph();
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");
            var nodeC = new Node(3, "C");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);

            graph.AddEdge(nodeA, nodeB);
            graph.AddEdge(nodeA, nodeC);
            graph.AddEdge(nodeB, nodeC);

            var edgesFromA = graph.GetEdgesFrom(nodeA).ToList();

            Assert.Equal(2, edgesFromA.Count);
            Assert.All(edgesFromA, e => Assert.Equal(nodeA, e.From));
        }

        [Fact]
        public void GetNeighbors_ShouldReturnCorrectNodes()
        {
            var graph = new Graph();
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");
            var nodeC = new Node(3, "C");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);

            graph.AddEdge(nodeA, nodeB);
            graph.AddEdge(nodeA, nodeC);

            var neighbors = graph.GetNeighbors(nodeA).ToList();

            Assert.Contains(nodeB, neighbors);
            Assert.Contains(nodeC, neighbors);
            Assert.DoesNotContain(nodeA, neighbors);
        }

        [Fact]
        public void Reverse_ShouldReverseAllEdges()
        {
            var graph = new Graph();
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);

            graph.AddEdge(nodeA, nodeB, 7);

            var reversed = graph.Reverse();

            var edge = reversed.Edges.First();
            Assert.Equal(nodeB, edge.From);
            Assert.Equal(nodeA, edge.To);
            Assert.Equal(7, edge.Weight);

            // nodes should remain the same
            Assert.Contains(nodeA, reversed.Nodes);
            Assert.Contains(nodeB, reversed.Nodes);
        }
    }
}