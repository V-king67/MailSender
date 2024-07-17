namespace ConsoleTests.Data.Entities
{
    abstract public class Entity
    {
        public int Id { get; set; }
    }

    abstract public class NamedEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
