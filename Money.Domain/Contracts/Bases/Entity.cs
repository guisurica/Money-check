namespace Money.Domain.Contracts.Bases
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Indexes { get; set; }

        public Entity()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString("N");
            }

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Indexes = new List<string>();
        }
    }
}
