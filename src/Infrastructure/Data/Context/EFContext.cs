using Infrastructure.Data.Configuration;
using Infrastructure.Data.Entities;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Data.Context;

public class EFContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Uteis.GetEnvironmentVariableWithoutQuotes("CONNECTION_STRING");

        optionsBuilder
                .UseNpgsql(connectionString, 
                db => db.MigrationsHistoryTable("__FoundLostMigrationsHistory", "achados"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
    }

    public static DbContext CreateAndMigrate()
    {
        var context = new EFContext();

        Option<bool> VerificarDataBase()
        {
            try
            {
                return context.Set<Category>()
                    .Any()
                    .SomeNotNull();
            }
            catch
            {
                return Option.None<bool>();
            }
        }

        VerificarDataBase().MatchNone(() => context.Database.Migrate());

        return context;
    }
}