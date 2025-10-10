using Zeaclon.Math.GraphAlgorithms.Core;

namespace TestProject1.Models
{
    public class UnionFindTests
    {
        [Fact]
        public void Find_ShouldReturnSameNodeInitially()
        {
            var nodeA = new Node(1, "A");
            var uf = new UnionFind(new[] { nodeA });

            Assert.Equal(nodeA, uf.Find(nodeA));
        }

        [Fact]
        public void Union_ShouldMergeTwoSets()
        {
            var nodeA = new Node(1, "A");
            var nodeB = new Node(2, "B");

            var uf = new UnionFind(new[] { nodeA, nodeB });

            uf.Union(nodeA, nodeB);

            var rootA = uf.Find(nodeA);
            var rootB = uf.Find(nodeB);

            Assert.Equal(rootA, rootB);
        }

        [Fact]
        public void Union_ShouldMaintainRank()
        {
            var nodeA = new Node(1);
            var nodeB = new Node(2);
            var nodeC = new Node(3);

            var uf = new UnionFind(new[] { nodeA, nodeB, nodeC });

            uf.Union(nodeA, nodeB); // rank changes
            uf.Union(nodeB, nodeC); // merges remaining

            var root = uf.Find(nodeA);
            Assert.Equal(root, uf.Find(nodeB));
            Assert.Equal(root, uf.Find(nodeC));
        }

        [Fact]
        public void Union_SameSet_ShouldNotChangeRoots()
        {
            var nodeA = new Node(1);
            var nodeB = new Node(2);

            var uf = new UnionFind(new[] { nodeA, nodeB });

            uf.Union(nodeA, nodeB);
            var rootBefore = uf.Find(nodeA);
            uf.Union(nodeA, nodeB);
            var rootAfter = uf.Find(nodeA);

            Assert.Equal(rootBefore, rootAfter);
        }
    }
}