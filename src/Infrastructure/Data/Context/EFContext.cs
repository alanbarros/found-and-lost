using Infrastructure.Data.Configuration;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class EFContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
                .UseNpgsql(
            "Host=db;Username=postgres;Password=found&lost;Database=foundAndLost",
            //Environment.GetEnvironmentVariable("CONNECTION_STRING"),
                    db => db.MigrationsHistoryTable("__FoundLostMigrationsHistory", "achados"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
    }
}