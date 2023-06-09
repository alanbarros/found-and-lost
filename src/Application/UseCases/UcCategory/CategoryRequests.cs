using Application.Boundaries.Inputs;
using Domain.Entities;

namespace Application.UseCases.UcCategory;

public class UpdateCategoryRequest : BaseRequest
{
    public string Description { get; set; }
    public Guid IdCategory { get; set; }

    public UpdateCategoryRequest(Guid idCategory, string description)
    {
        this.IdCategory = idCategory;
        this.Description = description;
    }

    public Domain.Entities.Category Category =>
        new Domain.Entities.Category(IdCategory, string.Empty, Description);
}

public class AddCategoryRequest : BaseRequest
{
    public AddCategoryRequest(CategoryInput categoryInput)
    {
        this.Category = new Category(Guid.NewGuid(),
        categoryInput.Name ?? string.Empty,
        categoryInput.Description ?? string.Empty);
    }

    public Category Category { get; }
}

public class FindCategoryRequest : BaseRequest
{
    public FindCategoryRequest(string categoryName)
    {
        this.CategoryName = categoryName;

    }

    public string CategoryName { get; set; }
}

public class DeleteCategoryRequest : BaseRequest
{
    public Guid CategoryId { get; }
    public DeleteCategoryRequest(Guid categoryId)
    {
        this.CategoryId = categoryId;

    }
}