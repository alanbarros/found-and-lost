using Application.Boundaries;
using Application.Repository;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.UcCategory
{
    public class CategoryUseCase :
        IFindCategoryUseCase,
        IAddCategoryUseCase,
        IUpdateCategoryUseCase,
        IDeleteCategoryUseCase,
        IListCategoryUseCase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryUseCase(ICategoryRepository repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Execute(FindCategoryRequest request, IOutputPort<Category> outputPort) =>
            _repository.Find(c => c.Name.Contains(request.CategoryName))
            .Match(some: category => outputPort.Standard(category),
            none: () => outputPort.NotFound());

        public void Execute(AddCategoryRequest request, IOutputPort<Category> outputPort) =>
            _repository.Add(request.Category)
            .Match(some: (_) => outputPort.Standard(request.Category),
            none: () => outputPort.Fail());

        public void Execute(UpdateCategoryRequest request, IOutputPort<Category, Exception> outputPort) =>
            _repository.Update(request.IdCategory, request.Category)
            .Match(some: (category) => outputPort.Standard(category),
            none: (ex) => outputPort.Fail(ex));

        public void Execute(DeleteCategoryRequest request, IOutputPort<string, Exception> outputPort) =>
            _repository.Delete(request.CategoryId)
            .Match(some: (_) => outputPort.Standard("Removido com sucesso"),
            none: (ex) => outputPort.Fail(ex));

        public void Execute(ListCategoryRequest input, IOutputPort<PaginationOutput<Category>> outputPort)
        {
            input.MaybeCategoryName.Match(
                some: (categoryName) =>
                {
                    var items = _repository.List(c => c.Name.Contains(categoryName),
                        input.Input);

                    outputPort.Standard(items);
                },
                none: () =>
                {
                    var items = _repository.List(c => c.Name != null,
                        input.Input);

                    outputPort.Standard(items);
                }
            );
        }
    }
}