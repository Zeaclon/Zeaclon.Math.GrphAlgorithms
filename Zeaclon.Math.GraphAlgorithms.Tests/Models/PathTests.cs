using Zeaclon.Math.GraphAlgorithms.Core;
using Path = Zeaclon.Math.GraphAlgorithms.Core.Path;

namespace TestProject1.Models
{
    public class PathTests
    {
        [Fact]
        public void Constructor_WithExplicitCost_ShouldSetProperties()
        {
            var nodes = new List<Node>
            {
                new Node(1, "A"),
                new Node(2, "B")
            };
            var path = new Path(nodes, 5);

            Assert.Equal(2, path.Length);
            Assert.Equal(5, path.Cost);
            Assert.Equal(nodes, path.Nodes);
        }

        [Fact]
        public void Constructor_WithGraph_ShouldCalculateCost()
        {
            var graph = new Graph();
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");
            var nodeC = new Node(3, "C");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddEdge(nodeA, nodeB, 3);
            graph.AddEdge(nodeB, nodeC, 4);

            var pathNodes = new List<Node> { nodeA, nodeB, nodeC };
            var path = new Path(pathNodes, graph);

            Assert.Equal(7, path.Cost); // 3 + 4
            Assert.Equal(3, path.Length);
        }

        [Fact]
        public void ToString_ShouldContainNodeIdsAndCost()
        {
            var nodes = new List<Node>
            {
                new Node(1),
                new Node(2)
            };
            var path = new Path(nodes, 10);

            var str = path.ToString();
            Assert.Contains("Cost: 10", str);
            Assert.Contains("1 -> 2", str);
        }
    }
}