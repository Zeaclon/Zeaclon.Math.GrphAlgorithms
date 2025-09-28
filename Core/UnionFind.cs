namespace Zeaclon.Math.GraphAlgorithms.Core
{
    // Union-Find (Disjoint Set) data structure for cycle detection
    public class UnionFind
    {
        private readonly Dictionary<Node, Node> _parent;
        private readonly Dictionary<Node, int> _rank;

        public UnionFind(IEnumerable<Node> nodes)
        {
            _parent = new Dictionary<Node, Node>();
            _rank = new Dictionary<Node, int>();
            foreach (var node in nodes)
            {
                _parent[node] = node;
                _rank[node] = 0;
            }
        }

        public Node Find(Node node)
        {
            if (!_parent[node].Equals(node))
            {
                _parent[node] = Find(_parent[node]);
            }
            return _parent[node];
        }

        public void Union(Node a, Node b)
        {
            var rootA = Find(a);
            var rootB = Find(b);
            if (rootA.Equals(rootB))
                return;
            
            // Union by rank
            if (_rank[rootA] < _rank[rootB])
            {
                _parent[rootA] = rootB;
            }
            else if (_rank[rootA] > _rank[rootB])
            {
                _parent[rootB] = rootA;
            }
            else
            {
                _parent[rootB] = rootA;
                _rank[rootA]++;
            }
        }
    }    
}