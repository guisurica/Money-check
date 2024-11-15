namespace Money.Domain.Contracts.Bases
{
    public abstract class DTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
