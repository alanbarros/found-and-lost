namespace Infrastructure.Data.Entities
{
    public abstract class DefaultDbEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}