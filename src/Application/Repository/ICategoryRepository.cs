using Domain.Entities;
using Optional;

namespace Application.Repository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Option<Category> GetWithParentAndSubcategories(Guid id);
}