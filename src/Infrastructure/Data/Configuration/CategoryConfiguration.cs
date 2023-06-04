using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", SCHEMA_NAME);

            builder.HasKey(p => p.Id);

            builder.HasAlternateKey(p => p.Name);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}