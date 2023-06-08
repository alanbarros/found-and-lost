using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public abstract class BaseConfiguration<TEntity, TDomain> :
        IEntityTypeConfiguration<TEntity>
        where TDomain : Domain.Entities.BaseDomain
        where TEntity : DefaultDbEntity<TDomain>
    {
        protected const string SCHEMA_NAME = "achados";
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UpdatedAt).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
        }
    }
}