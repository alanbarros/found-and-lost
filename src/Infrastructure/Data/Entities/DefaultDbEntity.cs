using Domain.Entities;

namespace Infrastructure.Data.Entities
{
    public abstract class DefaultDbEntity<TDomain> where TDomain : BaseDomain
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public abstract void Update(TDomain domain);
    }
}