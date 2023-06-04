using Application.UseCases.UCWeatherForecast;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherForecastPresenter _presenter;
    private readonly IWeatherForecastUseCase _useCase;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
     WeatherForecastPresenter presenter,
     IWeatherForecastUseCase useCase)
    {
        _logger = logger;
        _presenter = presenter;
        _useCase = useCase;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        _useCase.Execute(_presenter);

        return _presenter.ViewModel;
    }
}
