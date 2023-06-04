using Infrastructure.Data.Configuration;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Optional;

namespace Infrastructure.Data.Context;

public class EFContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public EFContext()
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        ConfigureDb(connectionString);
    }

    public EFContext(string connectionString) => ConfigureDb(connectionString);

    private void ConfigureDb(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<EFContext>()
            .UseNpgsql(connectionString, db => db.MigrationsHistoryTable("__FoundLostMigrationsHistory", "achados"));

        base.OnConfiguring(contextOptions);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
    }
}