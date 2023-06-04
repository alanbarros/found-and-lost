using Application.Boundaries.Inputs;
using Domain.Entities;

namespace Application.UseCases.UcCategory
{
    public interface IAddCategoryUseCase : IUseCase<CategoryInput, Category>
    {

    }
}