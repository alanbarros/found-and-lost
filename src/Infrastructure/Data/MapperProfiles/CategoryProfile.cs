using AutoMapper;
using Optional;

namespace Infrastructure.Data.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Domain.Entities.Category, Data.Entities.Category>()
                .ForMember(a => a.ParentId, src => src.MapFrom(a =>
                a.Parent.SomeNotNull()
                    .Match((p) => p.Id,
                        () => a.Id)))
                .ForMember(a => a.CreatedAt, src => src.MapFrom(a => DateTime.UtcNow))
                .ForMember(a => a.UpdatedAt, src => src.MapFrom(a => DateTime.UtcNow));

            CreateMap<Data.Entities.Category, Domain.Entities.Category>()
                .ConstructUsing((src, map) => src.MapDomain(map));

            CreateMap<Application.Boundaries.Inputs.CategoryInput, Domain.Entities.Category>()
                .ConstructUsing((input, map) => new Domain.Entities.Category(Guid.NewGuid(), input.Name, input.Description));
        }
    }
}