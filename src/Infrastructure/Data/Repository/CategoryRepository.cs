using AutoMapper;

namespace Infrastructure.Data.Repository
{
    public class CategoryRepository :
        BaseRepository<Data.Entities.Category, Domain.Entities.Category>,
        Application.Repository.ICategoryRepository
    {
        public CategoryRepository(IMapper mapper) : base(mapper)
        {

        }
    }
}