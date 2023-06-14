using System.ComponentModel.DataAnnotations;
using Application.Boundaries;
using Application.Boundaries.Inputs;
using Application.UseCases.UcCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace found_and_lost.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly CategoryPresenter _presenter;
    private readonly IAddCategoryUseCase _useCaseAdd;
    private readonly IFindCategoryUseCase _useCaseFind;
    private readonly IUpdateCategoryUseCase _useCaseUpdate;
    private readonly IDeleteCategoryUseCase _useCaseDelete;
    private readonly IListCategoryUseCase _useCaseList;
    private readonly IReadCategoryUseCase _useCaseRead;

    public CategoryController(ILogger<CategoryController> logger,
     CategoryPresenter presenter,
     IAddCategoryUseCase useCaseAdd,
     IFindCategoryUseCase useCaseFind,
     IUpdateCategoryUseCase useCaseUpdate,
     IDeleteCategoryUseCase useCaseDelete,
     IListCategoryUseCase useCaseList,
     IReadCategoryUseCase useCaseRead)
    {
        _logger = logger;
        _presenter = presenter;
        _useCaseAdd = useCaseAdd;
        _useCaseFind = useCaseFind;
        _useCaseUpdate = useCaseUpdate;
        _useCaseDelete = useCaseDelete;
        _useCaseList = useCaseList;
        _useCaseRead = useCaseRead;
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 400, type: typeof(ValidationProblemDetails))]
    [ProducesResponseType(statusCode: 200, type: typeof(Category))]
    public IActionResult Create([FromBody] CategoryInput categoryInput)
    {
        var request = new AddCategoryRequest(categoryInput);
        _useCaseAdd.Execute(request, _presenter);

        return _presenter.ViewModel;
    }

    /// <summary>
    /// Buscar por categorias através no nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("{categoryId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Category))]
    [ProducesResponseType(statusCode: 404)]
    public IActionResult Read(Guid categoryId)
    {
        var request = new ReadCategoryRequest(categoryId);
        _useCaseRead.Execute(request, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPut("{categoryId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Category))]
    [ProducesResponseType(statusCode: 404)]
    [ProducesResponseType(statusCode: 400, type: typeof(ValidationProblemDetails))]
    public IActionResult Update(
        Guid categoryId,
        [FromForm]
        string description)
    {
        var category = new UpdateCategoryRequest(categoryId, description);
        _useCaseUpdate.Execute(category, _presenter);

        return _presenter.ViewModel;
    }

    [HttpDelete("{categoryId}")]
    [ProducesResponseType(statusCode: 404)]
    [ProducesResponseType(statusCode: 200)]
    public IActionResult Delete(Guid categoryId)
    {
        var category = new DeleteCategoryRequest(categoryId);
        _useCaseDelete.Execute(category, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Category>))]
    [ProducesResponseType(statusCode: 400, type: typeof(ValidationProblemDetails))]
    public IActionResult ListAll(
        [FromBody] PaginationInput paginationInput)
    {
        var category = new ListCategoryRequest(paginationInput);
        _useCaseList.Execute(category, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Category>))]
    [ProducesResponseType(statusCode: 400, type: typeof(ValidationProblemDetails))]
    public IActionResult ListByName(
        [FromBody] PaginationInputObject<ListCategoryByNameInput> paginationInput)
    {
        var category = new ListCategoryRequest(paginationInput, paginationInput.Input.CategoryName);
        _useCaseList.Execute(category, _presenter);

        return _presenter.ViewModel;
    }
}
