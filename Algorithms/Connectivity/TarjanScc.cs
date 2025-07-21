using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Algorithms.Connectivity
{
    public static class TarjanSCC
    {
        public static List<List<Node>> FindStronglyConnectedComponents(Graph graph)
        {
            var index = 0;
            var stack = new Stack<Node>();
            var indices = new Dictionary<Node, int>();
            var lowLinks = new Dictionary<Node, int>();
            var onStack = new HashSet<Node>();
            var sccs = new List<List<Node>>();

            void StrongConnect(Node node)
            {
                indices[node] = index;
                lowLinks[node] = index;
                index++;
                stack.Push(node);
                onStack.Add(node);

                foreach (var edge in graph.GetEdgesFrom(node))
                {
                    var neighbor = edge.To;

                    if (!indices.ContainsKey(neighbor))
                    {
                        // Neighbor has not been visited yet
                        StrongConnect(neighbor);
                        lowLinks[node] = System.Math.Min(lowLinks[node], lowLinks[neighbor]);
                    }
                    else if (onStack.Contains(neighbor))
                    {
                        // Neighbor is in the current SCC stack
                        lowLinks[node] = System.Math.Min(lowLinks[node], indices[neighbor]);
                    }
                }

                // If node is a root node, pop the stack and generate an SCC
                if (lowLinks[node] == indices[node])
                {
                    var scc = new List<Node>();
                    Node w;
                    do
                    {
                        w = stack.Pop();
                        onStack.Remove(w);
                        scc.Add(w);
                    } while (w != node);

                    sccs.Add(scc);
                }
            }

            foreach (var node in graph.Nodes)
            {
                if (!indices.ContainsKey(node))
                {
                    StrongConnect(node);
                }
            }

            return sccs;
        }
    }
}