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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<,>).Assembly);
    }

    public static DbContext CreateAndMigrate()
    {
        var context = new EFContext();

        var migrations = new List<string>()
        {
            "20230604000039_CreateDb",
            "20230608224443_CategorySelfReferency"
        };

        Option<bool> VerificarDataBase()
        {
            try
            {
                var lastMigration = context.Database
                .SqlQuery<string>($"select \"MigrationId\" FROM achados.\"__FoundLostMigrationsHistory\"")
                .ToList();

                return lastMigration.Contains(migrations.Last()).SomeWhen(exists => exists == true);
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