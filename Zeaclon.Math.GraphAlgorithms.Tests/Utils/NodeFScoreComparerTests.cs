using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Utils;

namespace TestProject1.Utils
{
    public class NodeFScoreComparerTests
    {
        [Fact]
        public void NodeFScoreComparer_ShouldSortByFScore()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");
            var nodeC = new Node(3, "C");

            var fScore = new Dictionary<Node, double>
            {
                [nodeA] = 10,
                [nodeB] = 5,
                [nodeC] = 10
            };

            var comparer = new NodeFScoreComparer(fScore);
            var list = new List<Node> { nodeA, nodeB, nodeC };
            list.Sort(comparer);

            // nodeB has smallest fScore
            Assert.Equal(nodeB, list[0]);

            // nodeA and nodeC tie on fScore, tie-breaker by Id
            Assert.Equal(nodeA, list[1]);
            Assert.Equal(nodeC, list[2]);
        }

        [Fact]
        public void NodeFScoreComparer_ShouldReturnZeroForNullNodes()
        {
            var fScore = new Dictionary<Node, double>();
            var comparer = new NodeFScoreComparer(fScore);

            int result = comparer.Compare(null, null);
            Assert.Equal(0, result);
        }
    }
}
