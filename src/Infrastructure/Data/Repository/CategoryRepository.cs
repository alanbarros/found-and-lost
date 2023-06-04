using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Repository
{
    public class CategoryRepository : BaseRepository<Data.Entities.Category, Domain.Entities.Category>,
        Application.Repository.ICategoryRepository
    {
        public CategoryRepository(IMapper mapper, IConfiguration configuration) : base(mapper, configuration)
        {

        }
    }
}