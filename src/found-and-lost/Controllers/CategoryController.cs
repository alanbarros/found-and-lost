using Application.Boundaries;
using Application.Boundaries.Inputs;
using Application.UseCases.UcCategory;
using Microsoft.AspNetCore.Mvc;

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

    public CategoryController(ILogger<CategoryController> logger,
     CategoryPresenter presenter,
     IAddCategoryUseCase useCaseAdd,
     IFindCategoryUseCase useCaseFind,
     IUpdateCategoryUseCase useCaseUpdate,
     IDeleteCategoryUseCase useCaseDelete,
     IListCategoryUseCase useCaseList)
    {
        _logger = logger;
        _presenter = presenter;
        _useCaseAdd = useCaseAdd;
        _useCaseFind = useCaseFind;
        _useCaseUpdate = useCaseUpdate;
        _useCaseDelete = useCaseDelete;
        _useCaseList = useCaseList;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CategoryInput categoryInput)
    {
        var request = new AddCategoryRequest(categoryInput);
        _useCaseAdd.Execute(request, _presenter);

        return _presenter.ViewModel;
    }

    [HttpGet]
    public IActionResult Read([FromQuery] string name)
    {
        var request = new FindCategoryRequest(name);
        _useCaseFind.Execute(request, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPut]
    public IActionResult Update(
        [FromQuery] Guid id,
        [FromQuery] string description)
    {
        var category = new UpdateCategoryRequest(id, description);
        _useCaseUpdate.Execute(category, _presenter);

        return _presenter.ViewModel;
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id)
    {
        var category = new DeleteCategoryRequest(id);
        _useCaseDelete.Execute(category, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPost]
    public IActionResult ListAll(
        [FromBody] PaginationInput paginationInput)
    {
        var category = new ListCategoryRequest(paginationInput);
        _useCaseList.Execute(category, _presenter);

        return _presenter.ViewModel;
    }

    [HttpPost]
    public IActionResult ListByName(
        [FromBody] PaginationInputObject<ListCategoryByNameInput> paginationInput)
    {
        var category = new ListCategoryRequest(paginationInput, paginationInput.Input.CategoryName);
        _useCaseList.Execute(category, _presenter);

        return _presenter.ViewModel;
    }
}
