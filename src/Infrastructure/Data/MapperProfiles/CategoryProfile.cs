using AutoMapper;

namespace Infrastructure.Data.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Domain.Entities.Category, Data.Entities.Category>()
                .ForMember(a => a.CreatedAt, src => src.MapFrom(a => DateTime.Now))
                .ForMember(a => a.UpdatedAt, src => src.MapFrom(a => DateTime.Now));

            CreateMap<Data.Entities.Category, Domain.Entities.Category>()
                .ConstructUsing((src, map) => new Domain.Entities.Category(src.Id, src.Name, src.Description));
        }
    }
}