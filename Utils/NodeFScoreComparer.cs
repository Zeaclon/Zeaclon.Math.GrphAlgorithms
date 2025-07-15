using Zeaclon.Math.GraphAlgorithms.Core;

namespace Zeaclon.Math.GraphAlgorithms.Utils
{
    public class NodeFScoreComparer(Dictionary<Node, double> fScore) : IComparer<Node>
    {
        public int Compare(Node? x, Node? y)
        {
            if (x is null || y is null) return 0;

            var compare = fScore[x].CompareTo(fScore[y]);
            if (compare == 0)
            {
                // Tie-breaker so SortedSet doesn't think equal and drops duplicates
                return x.Id.CompareTo(y.Id);
            }
            return compare;
        }
    }
}