using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms
{
    
    public static class Johnson
    {
        public static Dictionary<(Node, Node), double>? AllPairsShortestPaths(Graph graph)
        {
            // Create super node
            var superNode = new Node(-1, "SuperNode");
            var augmentedGraph = new Graph();
            foreach (var node in graph.Nodes)
                augmentedGraph.AddNode(node);
            augmentedGraph.AddNode(superNode);
            foreach (var node in graph.Nodes)
                augmentedGraph.AddEdge(superNode, node, 0);

            foreach (var edge in graph.Edges)
                augmentedGraph.AddEdge(edge.From, edge.To, edge.Weight);
            
            // Bellman-Ford from super node
            var h = BellmanFord.ShortestPaths(augmentedGraph, superNode);
            if (h is null) return null; // Negative cycle Detected
            
            // Reweight edges
            var reweightedGraph = new Graph();
            foreach (var node in graph.Nodes)
                reweightedGraph.AddNode(node);
            foreach (var edge in graph.Edges)
            {
                var newWeight = edge.Weight + h[edge.From] - h[edge.To];
                reweightedGraph.AddEdge(edge.From, edge.To, newWeight);
            }
            
            // For each node, run Dijkstra on weighted graph
            var allPairsDistances = new Dictionary<(Node, Node), double>();

            foreach (var u in graph.Nodes)
            {
                var dist = Dijkstra.ShortestPaths(reweightedGraph, u);
                foreach (var v in dist.Keys)
                {
                    // Adjust back to original weights
                    allPairsDistances[(u, v)] = dist[v] + h[v] - h[u];
                }
            }

            return allPairsDistances;
        }
    }
}
