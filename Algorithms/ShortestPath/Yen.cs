using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Extensions;
using Path = Zeaclon.Math.GraphAlgorithms.Core.Path;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public static class Yen
    {
        public static List<Path> FindKShortestPaths(
            Graph graph,
            Node start,
            Node goal,
            int k,
            Func<Graph, Node, Node, List<Node>?> shortestPathFunc)
        {
            var kPaths = new List<Path>();
            var candidates = new SortedSet<Path>(new PathComparerByCost());
            
            var firstPathNodes = shortestPathFunc(graph, start, goal);
            if (firstPathNodes is null)
                return kPaths;
            
            kPaths.Add(new Path(firstPathNodes, graph));

            for (int i = 1; i < k; i++)
            {
                var previousPath = kPaths[i - 1];
                var previousNodes = previousPath.Nodes;
                
                for (int j = 0; j < previousNodes.Count - 1; j++)
                {
                    var spurNode = previousNodes[j];
                    var rootPathNodes = previousNodes.Take(j + 1).ToList();

                    var graphCopy = graph.Clone();

                    // Remove edges that would create duplicates
                    foreach (var path in kPaths)
                    {
                        var pathNodes = path.Nodes;
                        if (pathNodes.Count > j && rootPathNodes.SequenceEqual(pathNodes.Take(j + 1)))
                        {
                            graphCopy.RemoveEdge(pathNodes[j], pathNodes[j + 1]);
                        }
                    }

                    // Remove nodes in rootPath except spurNode
                    foreach (var node in rootPathNodes.Take(rootPathNodes.Count - 1))
                    {
                        graphCopy.RemoveNode(node);
                    }

                    var spurPathNodes = shortestPathFunc(graphCopy, spurNode, goal);

                    if (spurPathNodes is not null && spurPathNodes.Count > 0)
                    {
                        var totalPathNodes = new List<Node>(rootPathNodes);
                        totalPathNodes.AddRange(spurPathNodes.Skip(1));

                        var candidatePath = new Path(totalPathNodes, graph);
                        candidates.Add(candidatePath);
                    }
                }

                if (candidates.Count == 0)
                    break;

                var bestCandidate = candidates.Min!;
                candidates.Remove(bestCandidate);
                kPaths.Add(bestCandidate);
            }

            return kPaths;
        }
    }
}