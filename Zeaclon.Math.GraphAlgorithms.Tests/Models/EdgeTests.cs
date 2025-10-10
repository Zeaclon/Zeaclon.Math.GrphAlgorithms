using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Models
{
    public class EdgeTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var edge = new Edge(nodeA, nodeB, 5);

            Assert.Equal(nodeA, edge.From);
            Assert.Equal(nodeB, edge.To);
            Assert.Equal(5, edge.Weight);
        }

        [Fact]
        public void DefaultWeight_ShouldBeOne()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var edge = new Edge(nodeA, nodeB);

            Assert.Equal(1, edge.Weight);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValues()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var edge1 = new Edge(nodeA, nodeB, 10);
            var edge2 = new Edge(nodeA, nodeB, 10);

            Assert.Equal(edge1, edge2);
            Assert.True(edge1 == edge2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValues()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var edge1 = new Edge(nodeA, nodeB, 10);
            var edge2 = new Edge(nodeA, nodeB, 20);

            Assert.NotEqual(edge1, edge2);
            Assert.False(edge1 == edge2);
        }

        [Fact]
        public void ToString_ShouldContainProperties()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var edge = new Edge(nodeA, nodeB, 3);

            var str = edge.ToString();

            Assert.Contains("From = Node { Id = 1, Name = A }", str);
            Assert.Contains("To = Node { Id = 2, Name = B }", str);
            Assert.Contains("Weight = 3", str);
        }
    }
}