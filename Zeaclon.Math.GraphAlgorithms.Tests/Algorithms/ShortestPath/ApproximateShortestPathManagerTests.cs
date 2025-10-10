using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath;

namespace TestProject1.Algorithms.ShortestPath
{
    public class ApproximateShortestPathManagerTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");

        private Graph CreateSimpleGraph()
        {
            var graph = new Graph();
            graph.AddEdge(A, B, 1);
            graph.AddEdge(B, C, 2);
            graph.AddEdge(A, C, 4);
            graph.AddEdge(C, D, 1);
            return graph;
        }

        [Fact]
        public void Initialize_ShouldSetLandmarksAndPreprocess()
        {
            var graph = new Graph(isDirected: false);
            graph.AddEdge(A, B, 1);
            graph.AddEdge(B, C, 2);
            graph.AddEdge(A, C, 4);
            graph.AddEdge(C, D, 1);
            var manager = new ApproximateShortestPathManager(graph);
            manager.SetLandmarks(new List<Node> { A, D });

            // Check that heuristic for a node is not infinity
            double h = manager.AltHeuristic(B, D);
            Assert.True(h >= 0 && !double.IsPositiveInfinity(h));
        }

        [Fact]
        public void AltHeuristic_ShouldBeZeroForSameNode()
        {
            var graph = CreateSimpleGraph();
            var manager = new ApproximateShortestPathManager(graph);
            manager.SetLandmarks(new List<Node> { A, D });

            double h = manager.AltHeuristic(B, B);
            Assert.Equal(0, h);
        }

        [Fact]
        public void FindPathAlt_ShouldReturnValidPath()
        {
            var graph = CreateSimpleGraph();
            var manager = new ApproximateShortestPathManager(graph);
            manager.SetLandmarks(new List<Node> { A, D });

            var path = manager.FindPathAlt(A, D);
            Assert.NotNull(path);
            Assert.Equal(A, path[0]);
            Assert.Equal(D, path[^1]);

            // Path should be valid sequence of edges
            for (int i = 0; i < path.Count - 1; i++)
            {
                Assert.Contains(graph.GetEdgesFrom(path[i]), e => e.To == path[i + 1]);
            }
        }

        [Fact]
        public void FindPathAlt_ShouldReturnNullIfNoPathExists()
        {
            var graph = new Graph();
            graph.AddNode(A);
            graph.AddNode(B);

            var manager = new ApproximateShortestPathManager(graph);
            manager.SetLandmarks(new List<Node> { A });

            var path = manager.FindPathAlt(A, B);
            Assert.Null(path);
        }
    }
}
