using Zeaclon.Math.GraphAlgorithms.Core;
using Zeaclon.Math.GraphAlgorithms.Utils;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public static class AStar
    {
        public static List<Node>? FindPath(Graph graph, Node start, Node goal, Func<Node, double> heuristic)
        {
            var fScore = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var openSet = new SortedSet<Node>(new NodeFScoreComparer(fScore));
            var cameFrom = new Dictionary<Node, Node?>();
            var gScore = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);

            gScore[start] = 0;
            fScore[start] = heuristic(start);
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                var current = openSet.Min!;
                if (current == goal)
                    return ReconstructPath(cameFrom, current);

                openSet.Remove(current);

                foreach (var edge in graph.GetEdgesFrom(current))
                {
                    var neighbor = edge.To;
                    var tentativeGScore = gScore[current] + edge.Weight;

                    if (tentativeGScore >= gScore[neighbor]) continue;
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + heuristic(neighbor);

                    openSet.Remove(neighbor); // This is safe even if neighbor isn’t in the set
                    openSet.Add(neighbor);
                }
            }
            return null;
        }

        public static List<Node>? FindBidirectionalPath(Graph graph, Node start, Node goal,
            Func<Node, double> heuristic)
        {
            if (start == goal)
                return [start];
            
            var forwardG = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var backwardG = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            
            var forwardF = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var backwardF = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            
            var forwardCameFrom = new Dictionary<Node, Node?>();
            var backwardCameFrom = new Dictionary<Node, Node?>();
            
            var forwardOpenSet = new SortedSet<Node>(new NodeFScoreComparer(forwardF));
            var backwardOpenSet = new SortedSet<Node>(new NodeFScoreComparer(backwardF));
            
            forwardG[start] = 0;
            forwardF[start] = heuristic(start);
            forwardOpenSet.Add(start);
            
            backwardG[goal] = 0;
            backwardF[goal] = heuristic(goal);
            backwardOpenSet.Add(goal);
            
            var visitedFromBoth = new HashSet<Node>();
            Node? meetingPoint = null;
            double bestPathLength = double.PositiveInfinity;
            var reverseGraph = graph.Reverse();

            while (forwardOpenSet.Count > 0 && backwardOpenSet.Count > 0)
            {
                Node current = forwardOpenSet.Min!;
                forwardOpenSet.Remove(current);

                foreach (var edge in graph.GetEdgesFrom(current))
                {
                    Node neighbor = edge.To;
                    double tentativeG = forwardG[current] + edge.Weight;

                    if (tentativeG < forwardG[neighbor])
                    {
                        forwardCameFrom[neighbor] = current;
                        forwardG[neighbor] = tentativeG;
                        forwardF[neighbor] = tentativeG + heuristic(neighbor);
                        forwardOpenSet.Remove(neighbor);
                        forwardOpenSet.Add(neighbor);
                    }

                    if (backwardG[neighbor] < double.PositiveInfinity)
                    {
                        double pathLength = forwardG[neighbor] + backwardG[neighbor];
                        if (pathLength < bestPathLength)
                        {
                            bestPathLength = pathLength;
                            meetingPoint = neighbor;
                        }
                    }
                }
                
                current = backwardOpenSet.Min!;
                backwardOpenSet.Remove(current);
                
                foreach (var edge in reverseGraph.GetEdgesFrom(current))
                {
                    Node neighbor = edge.To;
                    double tentativeG = backwardG[current] + edge.Weight;

                    if (tentativeG < backwardG[neighbor])
                    {
                        backwardCameFrom[neighbor] = current;
                        backwardG[neighbor] = tentativeG;
                        backwardF[neighbor] = tentativeG + heuristic(neighbor);
                        backwardOpenSet.Remove(neighbor);
                        backwardOpenSet.Add(neighbor);
                    }

                    if (forwardG[neighbor] < double.PositiveInfinity)
                    {
                        double pathLength = forwardG[neighbor] + backwardG[neighbor];
                        if (pathLength < bestPathLength)
                        {
                            bestPathLength = pathLength;
                            meetingPoint = neighbor;
                        }
                    }
                }
            }
            
            return meetingPoint is null ? null : ReconstructBidirectionalPath(forwardCameFrom, backwardCameFrom, meetingPoint);
        }

        private static List<Node> ReconstructBidirectionalPath(Dictionary<Node, Node?> forward,
            Dictionary<Node, Node?> backward, Node? meet)
        {
            var path = new List<Node>();
            var curr = meet;
            
            // reconstruct forward path
            while (forward.TryGetValue(curr, out var prev) && prev is not null)
            {
                path.Insert(0, prev);
                curr = prev;
            }

            path.Add(meet);
            curr = meet;
            
            // reconstruct backward path
            while (backward.TryGetValue(curr, out var next) && next is not null)
            {
                path.Add(next);
                curr = next;
            }

            return path;
        }

        private static List<Node> ReconstructPath(Dictionary<Node, Node?> cameFrom, Node current)
        {
            var path = new List<Node> { current };
            while (cameFrom.TryGetValue(current, out var prev) && prev is not null)
            {
                current = prev;
                path.Insert(0, current);
            }
            return path;
        }

    }
}