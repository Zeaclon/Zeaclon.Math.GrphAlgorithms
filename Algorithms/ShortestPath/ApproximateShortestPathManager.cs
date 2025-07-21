using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public class ApproximateShortestPathManager
    {
        private Graph _graph;
        private List<Node> _landmarks;
        private Dictionary<Node, Dictionary<Node, double>> _distFromLandmark;
        private Dictionary<Node, Dictionary<Node, double>> _distToLandmark;

        public ApproximateShortestPathManager(Graph graph)
        {
            _graph = graph;
            _landmarks = new List<Node>();
            _distFromLandmark = new();
            _distToLandmark = new();
        }

        public void SetLandmarks(List<Node> landmarks)
        {
            _landmarks = landmarks;
            Preprocess();
        }

        private void Preprocess()
        {
            _distFromLandmark.Clear();
            _distToLandmark.Clear();
            foreach (var lm in _landmarks)
            {
                _distFromLandmark[lm] = Dijkstra.ShortestPaths(_graph, lm);
                _distToLandmark[lm] = Dijkstra.ShortestPaths(_graph.Reverse(), lm);
            }
        }

        public double AltHeuristic(Node n, Node goal)
        {
            double maxEstimate = 0;
            foreach (var landmark in _landmarks)
            {
                double distLT = _distFromLandmark[landmark].GetValueOrDefault(goal, double.PositiveInfinity);
                double distLN = _distFromLandmark[landmark].GetValueOrDefault(n, double.PositiveInfinity);
                double distTL = _distToLandmark[landmark].GetValueOrDefault(goal, double.PositiveInfinity);
                double distNL = _distToLandmark[landmark].GetValueOrDefault(n, double.PositiveInfinity);
                
                double estimate = System.Math.Max(System.Math.Abs(distLT - distLN), System.Math.Abs(distNL - distTL));
                if (estimate > maxEstimate)
                    maxEstimate = estimate;
            }
            return maxEstimate;        
        }

        public List<Node>? FindPathAlt(Node start, Node goal)
        {
            return AStar.FindPath(_graph, start, goal, n => AltHeuristic(n, goal));
        }

    }
}