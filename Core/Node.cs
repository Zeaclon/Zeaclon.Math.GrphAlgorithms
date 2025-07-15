namespace Zeaclon.Math.GraphAlgorithms.Core
{
    public record Node(int id, string? name = null)
    {
        public int Id { get; } = id;
        public string? Name { get; set; } = name;
    }
}