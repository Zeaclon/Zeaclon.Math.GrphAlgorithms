namespace Zeaclon.Math.GraphAlgorithms.Algorithms
{
    public static class Dijkstra
    {
        /// <summary>
        /// Calculates the shortest path distances from the source node to all other nodes using Dijkstra’s algorithm.
        /// </summary>
        /// <param name="graph">Adjacency Matrix representing the graph (weights). Use int.MaxValue for no edge.</param>
        /// <param name="source">Index of the source node.</param>
        /// <returns>Array of shortest distances from source to each node.</returns>
        public static int[] ShortestPaths(int[,] graph, int source)
        {
            var n = graph.GetLength(0);
            var dist = new int[n];
            var visited = new bool[n];
            
            for (var i = 0; i < n; i++)
                dist[i] = int.MaxValue;
            
            dist[source] = 0;

            for (var count = 0; count < n - 1; count++)
            {
                var u = MinDistance(dist, visited);
                if (u == -1) break;
                visited[u] = true;

                for (var v = 0; v < n; v++)
                {
                    if (!visited[v] && graph[u, v] != int.MaxValue && dist[u] != int.MaxValue &&
                        dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                    }
                }
            }

            return dist;
        }

        private static int MinDistance(int[] dist, bool[] visited)
        {
            int min = int.MaxValue, minIndex = -1;

            for (var v = 0; v < dist.Length; v++)
            {
                if (!visited[v] && dist[v] <= min)
                {
                    min = dist[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

    }
}