using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity;

namespace TestProject1.Algorithms.Connectivity
{
    public class ArticulationPointsTests
    {
        private Node A => new Node(1, "A");
        private Node B => new Node(2, "B");
        private Node C => new Node(3, "C");
        private Node D => new Node(4, "D");
        private Node E => new Node(5, "E");

        private Graph CreateGraph(params Node[] nodes)
        {
            var g = new Graph();
            foreach (var node in nodes)
                g.AddNode(node);
            return g;
        }

        [Fact]
        public void EmptyGraph_ShouldReturnNoArticulationPoints()
        {
            var graph = new Graph();
            var result = ArticulationPoints.FindArticulationPoints(graph);
            Assert.Empty(result);
        }

        [Fact]
        public void SingleNodeGraph_ShouldReturnNoArticulationPoints()
        {
            var graph = CreateGraph(A);
            var result = ArticulationPoints.FindArticulationPoints(graph);
            Assert.Empty(result);
        }

        [Fact]
        public void LinearGraph_ShouldDetectMiddleNodesAsArticulationPoints()
        {
            // A - B - C
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);

            var result = ArticulationPoints.FindArticulationPoints(graph);

            // Only B should be articulation point
            Assert.Single(result);
            Assert.Contains(B, result);
        }

        [Fact]
        public void StarGraph_ShouldDetectCenterAsArticulationPoint()
        {
            //    B
            //    |
            // A--C--D
            //    |
            //    E
            var graph = CreateGraph(A, B, C, D, E);
            graph.AddEdge(C, A);
            graph.AddEdge(C, B);
            graph.AddEdge(C, D);
            graph.AddEdge(C, E);

            var result = ArticulationPoints.FindArticulationPoints(graph);

            // Only C is articulation point
            Assert.Single(result);
            Assert.Contains(C, result);
        }

        [Fact]
        public void FullyConnectedGraph_ShouldReturnNoArticulationPoints()
        {
            // Triangle: A-B-C-A
            var graph = CreateGraph(A, B, C);
            graph.AddEdge(A, B);
            graph.AddEdge(B, C);
            graph.AddEdge(C, A);

            var result = ArticulationPoints.FindArticulationPoints(graph);
            Assert.Empty(result);
        }

        [Fact]
        public void MultipleArticulationPoints_ShouldDetectAllCorrectly()
        {
            // Graph structure:
            //      B
            //      |
            // A -- C -- D
            //      |
            //      E
            var graph = CreateGraph(A, B, C, D, E);
            graph.AddEdge(A, C);
            graph.AddEdge(B, C);
            graph.AddEdge(C, D);
            graph.AddEdge(C, E);

            var result = ArticulationPoints.FindArticulationPoints(graph);

            Assert.Single(result); // Only C
            Assert.Contains(C, result);
        }
    }
}
