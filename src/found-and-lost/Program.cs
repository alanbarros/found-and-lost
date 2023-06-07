using Application.UseCases.UcCategory;
using Application.UseCases.UCWeatherForecast;
using found_and_lost.Controllers;
using Infrastructure.DI;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IWeatherForecastUseCase, WeatherForecastUseCase>();
builder.Services.AddScoped<WeatherForecastPresenter>();

builder.Services.AddScoped<IAddCategoryUseCase, CategoryUseCase>();
builder.Services.AddScoped<IFindCategoryUseCase, CategoryUseCase>();
builder.Services.AddScoped<CategoryPresenter>();

//builder.Services.AddMappingProfiles();
builder.Services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
builder.Services.AddRepositories();
builder.Services.ConfigureDatabase();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.MapControllers();

app.Run();
