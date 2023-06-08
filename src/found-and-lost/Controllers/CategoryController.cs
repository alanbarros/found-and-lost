using Application.Boundaries.Inputs;
using Application.UseCases.UcCategory;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Entities;

namespace found_and_lost.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly CategoryPresenter _presenter;
    private readonly IAddCategoryUseCase _useCaseAdd;
    private readonly IFindCategoryUseCase _useCaseFind;
    private readonly IUpdateCategoryUseCase _useCaseUpdate;
    private readonly IMapper _mapper;

    public CategoryController(ILogger<WeatherForecastController> logger,
     CategoryPresenter presenter,
     IAddCategoryUseCase useCaseAdd,
     IFindCategoryUseCase useCaseFind,
     IUpdateCategoryUseCase useCaseUpdate,
     IMapper mapper)
    {
        _logger = logger;
        _presenter = presenter;
        _useCaseAdd = useCaseAdd;
        _useCaseFind = useCaseFind;
        _useCaseUpdate = useCaseUpdate;
        _mapper = mapper;
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

    [HttpPut(Name = "UpdateCategory")]
    public IActionResult Update(
        [FromHeader] Guid id,
        [FromBody] CategoryInput input)
    {
        var category = new Category(id, input.Name, input.Description);
        _useCaseUpdate.Execute(category, _presenter);

        return _presenter.ViewModel;
    }
}
