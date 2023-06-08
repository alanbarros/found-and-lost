using Domain.Entities;

namespace Application.UseCases.UcCategory
{
    public interface IUpdateCategoryUseCase : IUseCase<UpdateCategoryRequest, Category, Exception>
    {

    }
}