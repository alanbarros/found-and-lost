using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : BaseConfiguration<Category, Domain.Entities.Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", SCHEMA_NAME);

            builder.HasKey(p => p.Id);

            builder.HasAlternateKey(p => p.Name);

            builder.Property(p => p.ParentId).IsRequired(false);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}