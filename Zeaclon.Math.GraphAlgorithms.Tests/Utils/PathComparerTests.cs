using Zeaclon.Math.GraphAlgorithms.Core;
using Path = Zeaclon.Math.GraphAlgorithms.Core.Path;

namespace TestProject1.Utils;

public class PathComparerTests
{
    
    [Fact]
    public void PathComparerByCost_ShouldSortByCostThenLength()
    {
        var nodeA = new Node(1, "A");
        var nodeB = new Node(2, "B");
        var nodeC = new Node(3, "C");

        var path1 = new Path([nodeA, nodeB], 5); // cost 5, length 2
        var path2 = new Path([nodeA, nodeB, nodeC], 5); // cost 5, length 3
        var path3 = new Path([nodeB, nodeC], 3); // cost 3, length 2

        var list = new List<Path> { path1, path2, path3 };
        list.Sort(new PathComparerByCost());

        // sorted by cost first, then length
        Assert.Equal(path3, list[0]); // cost 3
        Assert.Equal(path1, list[1]); // cost 5, length 2
        Assert.Equal(path2, list[2]); // cost 5, length 3
    }

}