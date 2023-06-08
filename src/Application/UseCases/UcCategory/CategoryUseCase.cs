using Application.Boundaries;
using Application.Boundaries.Inputs;
using Application.Repository;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.UcCategory
{
    public class CategoryUseCase :
        IFindCategoryUseCase,
        IAddCategoryUseCase,
        IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryUseCase(ICategoryRepository repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Execute(string input, IOutputPort<Category> outputPort)
        {
            _repository.Find(c => c.Name.Contains(input))
            .Match(
                some: category => outputPort.Standard(category),
                none: () => outputPort.Fail()
                );
        }

        public void Execute(CategoryInput input, IOutputPort<Category> outputPort)
        {
            var category = _mapper.Map<CategoryInput, Category>(input);

            _repository.Add(category)
                .Match(
                    some: (_) => outputPort.Standard(category),
                    none: () => outputPort.Fail()
                );
        }

        public void Execute(Category input, IOutputPort<Category> outputPort) =>
            _repository.Update(input.Id, input).Match(
                some: (category) => outputPort.Standard(category),
                none: () => outputPort.Fail());
    }
}