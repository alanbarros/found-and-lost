using Application.Boundaries;
using Application.Boundaries.Inputs;
using Application.Repository;
using Domain.Entities;

namespace Application.UseCases.UcCategory
{
    public class CategoryUseCase : IFindCategoryUseCase, IAddCategoryUseCase
    {
        private readonly ICategoryRepository repository;

        public CategoryUseCase(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(string input, IOutputPort<Category> outputPort)
        {
            repository.Find(input)
            .Match(
                some: category => outputPort.Standard(category),
                none: () => outputPort.Fail()
                );
        }

        public void Execute(CategoryInput input, IOutputPort<Category> outputPort)
        {
            var category = new Category(Guid.NewGuid(), input.Name, input.Description);

            repository.Add(category)
                .Match(
                    some: (_) => outputPort.Standard(category),
                    none: () => outputPort.Fail()
                );
        }
    }
}