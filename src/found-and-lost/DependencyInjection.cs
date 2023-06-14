using Application.UseCases.UcCategory;
using found_and_lost.Controllers;
using Infrastructure.DI;

namespace found_and_lost;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAddCategoryUseCase, CategoryUseCase>();
        services.AddScoped<IFindCategoryUseCase, CategoryUseCase>();
        services.AddScoped<IUpdateCategoryUseCase, CategoryUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, CategoryUseCase>();
        services.AddScoped<IListCategoryUseCase, CategoryUseCase>();
        services.AddScoped<IReadCategoryUseCase, CategoryUseCase>();
        services.AddScoped<CategoryPresenter>();

        services.AddRepositories();
        services.ConfigureDatabase();

        services.AddMappingProfiles();

        return services;
    }
}