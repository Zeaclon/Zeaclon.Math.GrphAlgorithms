using Zeaclon.Math.GraphAlgorithms.Core;

public class PathComparerByCost : IComparer<Zeaclon.Math.GraphAlgorithms.Core.Path>
{
    public int Compare(Zeaclon.Math.GraphAlgorithms.Core.Path? x, Zeaclon.Math.GraphAlgorithms.Core.Path? y)
    {
        if (x == null || y == null)
            throw new ArgumentNullException();

        int comp = x.Cost.CompareTo(y.Cost);
        return comp != 0 ? comp : x.Length.CompareTo(y.Length);
    }
}