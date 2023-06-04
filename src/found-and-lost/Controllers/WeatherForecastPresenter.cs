using Application.Boundaries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace found_and_lost.Controllers
{
    public class WeatherForecastPresenter : IOutputPort<List<WeatherForecast>>
    {
        public IActionResult ViewModel { get; private set; } = new BadRequestResult();

        public void Fail()
        {
            ViewModel = new BadRequestResult();
        }

        public void Standard(List<WeatherForecast> output)
        {
            ViewModel = new OkObjectResult(output);
        }
    }
}