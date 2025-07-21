using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.ShortestPath
{
    public static class Alt
    {
        private static ApproximateShortestPathManager? _instance;

        public static void Initialize(Graph graph, List<Node> landmarks)
        {
            _instance = new ApproximateShortestPathManager(graph);
            _instance.SetLandmarks(landmarks);
        }

        public static List<Node>? FindPath(Graph graph, Node start, Node goal)
        {
            if (_instance == null)
                throw new InvalidOperationException("Alt not initialized. Call Initialize() first.");
            return _instance.FindPathAlt(start, goal);
        }
    }
}
