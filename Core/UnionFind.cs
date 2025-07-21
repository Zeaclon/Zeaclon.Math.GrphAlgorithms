namespace Zeaclon.Math.GraphAlgorithms.Core
{
    // Union-Find (Disjoint Set) data structure for cycle detection
    public class UnionFind
    {
        private readonly Dictionary<Node, Node> parent;
        private readonly Dictionary<Node, int> rank;

        public UnionFind(IEnumerable<Node> nodes)
        {
            parent = new Dictionary<Node, Node>();
            rank = new Dictionary<Node, int>();
            foreach (var node in nodes)
            {
                parent[node] = node;
                rank[node] = 0;
            }
        }

        public Node Find(Node node)
        {
            if (!parent[node].Equals(node))
            {
                parent[node] = Find(parent[node]);
            }
            return parent[node];
        }

        public void Union(Node a, Node b)
        {
            var rootA = Find(a);
            var rootB = Find(b);
            if (rootA.Equals(rootB))
                return;
            
            // Union by rank
            if (rank[rootA] < rank[rootB])
            {
                parent[rootA] = rootB;
            }
            else if (rank[rootA] > rank[rootB])
            {
                parent[rootB] = rootA;
            }
            else
            {
                parent[rootB] = rootA;
                rank[rootA]++;
            }
        }
    }    
}