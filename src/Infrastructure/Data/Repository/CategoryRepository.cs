using AutoMapper;
using Optional;

namespace Infrastructure.Data.Repository;

public class CategoryRepository :
        BaseRepository<Data.Entities.Category, Domain.Entities.Category>,
        Application.Repository.ICategoryRepository
{
    public CategoryRepository(IMapper mapper) : base(mapper)
    {

    }

    public Option<Domain.Entities.Category> GetWithParentAndSubcategories(Guid id)
    {
        var maybeCategory = DbSet.Where(x => x.Id == id).Join(DbSet,
            c => c.ParentId,
            p => p.Id,
            (category, parent) => new { category, parent })
            .FirstOrDefault()
            .SomeNotNull()
            .Map<Data.Entities.Category>(a =>
            {
                a.category.Parent = a.parent;
                return a.category;
            }).Else(DbSet.Where(x => x.Id == id).FirstOrDefault().SomeNotNull());

        return maybeCategory.Map((cat) =>
        {
            cat.SubCategories = DbSet
                .Where(x => x.ParentId == cat.Id)
                .ToList();

            return _mapper.Map<Entities.Category, Domain.Entities.Category>(cat);
        });
    }
}