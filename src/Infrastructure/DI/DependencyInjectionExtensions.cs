using Application.Repository;
using AutoMapper;
using Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            var profiles = typeof(DependencyInjectionExtensions).Assembly.GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x));

            foreach (var profile in profiles)
            {
                cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        });

        services.AddSingleton<IMapper>(configuration.CreateMapper());

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}