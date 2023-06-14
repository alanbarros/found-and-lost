using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Data.Entities
{
    public abstract class DefaultDbEntity<TDomain> where TDomain : BaseDomain
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public abstract void Update(TDomain domain);

        public abstract TDomain MapDomain(ResolutionContext context);
    }
}