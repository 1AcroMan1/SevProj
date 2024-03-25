namespace SevProj.Models
{
    public class Direction
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Direction(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
