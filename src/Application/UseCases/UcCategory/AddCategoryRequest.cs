using Application.Boundaries.Inputs;
using Domain.Entities;

namespace Application.UseCases.UcCategory
{
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
}