using Application.Boundaries.Inputs;
using Application.UseCases.UcCategory;
using Application.UseCases.UCWeatherForecast;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly CategoryPresenter _presenter;
    private readonly IAddCategoryUseCase _useCaseAdd;
    private readonly IFindCategoryUseCase _useCaseFind;

    public CategoryController(ILogger<WeatherForecastController> logger,
     CategoryPresenter presenter,
     IAddCategoryUseCase useCaseAdd,
     IFindCategoryUseCase useCaseFind)
    {
        _logger = logger;
        _presenter = presenter;
        _useCaseAdd = useCaseAdd;
        _useCaseFind = useCaseFind;
    }

    [HttpGet(Name = "GetCategory")]
    public IActionResult Get([FromQuery] string name)
    {
        _useCaseFind.Execute(name, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPost(Name = "AddCategory")]
    public IActionResult Add([FromBody] CategoryInput category)
    {
        _useCaseAdd.Execute(category, _presenter);

        return _presenter.ViewModel;
    }
}
