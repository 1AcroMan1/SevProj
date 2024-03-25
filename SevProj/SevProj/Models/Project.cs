namespace SevProj.Models
{
    public class Project
    {
        public string Id {  get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string DirectionId { get; private set; }
        public Project(string id, string name, string description, string directionId)
        {
            Id = id;
            Name = name;
            Description = description;
            DirectionId = directionId;
        }
    }
}
