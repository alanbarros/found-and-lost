using Infrastructure.Data.Configuration;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class EFContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
                .UseNpgsql(@"Host=banco_dados;Username=postgres;Password=found&lost;Database=foundAndLost",
                    db => db.MigrationsHistoryTable("__FoundLostMigrationsHistory", "achados"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
    }
}