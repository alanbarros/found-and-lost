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
        IListCategoryUseCase,
        IReadCategoryUseCase,
        IUpdateParentCategoryUseCase
    {
        private readonly ICategoryRepository _repository;

        public CategoryUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Execute(FindCategoryRequest request, IOutputPort<Category> outputPort) =>
            _repository.Find(c => c.Name.Contains(request.CategoryName))
            .Match(some: category =>
            {
                category.Parent?.ClearParentAndSubcategories();

                outputPort.Standard(category);
            },
            none: () => outputPort.NotFound());

        public void Execute(AddCategoryRequest request, IOutputPort<Category> outputPort) =>
            _repository.Add(request.Category)
            .Match(some: (_) => outputPort.Standard(request.Category),
            none: () => outputPort.Fail());

        public void Execute(UpdateCategoryRequest request, IOutputPort<Category, Exception> outputPort) =>
            _repository.Update(request.IdCategory, request.Category)
            .Match(some: (category) =>
            {
                category.Parent?.ClearParentAndSubcategories();

                outputPort.Standard(category);
            },
            none: (ex) => outputPort.Fail(ex));

        public void Execute(DeleteCategoryRequest request, IOutputPort<string, Exception> outputPort) =>
            _repository.Delete(request.CategoryId)
            .Match(some: (_) => outputPort.Standard("Removido com sucesso"),
            none: (ex) => outputPort.Fail(ex));

        public void Execute(ListCategoryRequest input, IOutputPort<PaginationOutput<Category>> outputPort) =>
        input.MaybeCategoryName.Match(
            some: (categoryName) =>
            {
                var items = _repository.List(c => c.Name.Contains(categoryName),
                    input.Input);

                items.Items.ForEach(item =>
                {
                    item.Parent?.ClearParentAndSubcategories();
                    item.ClearSubcategoriesParentsAndSubCategories();
                });

                outputPort.Standard(items);
            },
            none: () =>
            {
                var items = _repository.List(c => c.Name != null,
                    input.Input);

                items.Items.ForEach(item =>
                {
                    item.Parent?.ClearParentAndSubcategories();
                    item.ClearSubcategoriesParentsAndSubCategories();
                });

                outputPort.Standard(items);
            }
        );

        public void Execute(ReadCategoryRequest input, IOutputPort<Category> outputPort) =>
            _repository.GetWithParentAndSubcategories(input.CategoryId).Match(
                some: (category) =>
                {
                    category.Parent?.ClearParentAndSubcategories();

                    category.ClearSubcategoriesParentsAndSubCategories();

                    outputPort.Standard(category);
                },
                none: () => outputPort.NotFound()
            );

        public void Execute(UpdateParentCategoryRequest input, IOutputPort<Category, Exception> outputPort) =>
            _repository.Find(input.CategoryId).Match(
                some: (category) =>
                {
                    _repository.Find(input.ParentId).Match(
                        some: (parent) =>
                        {
                            category.Parent = parent;

                            _repository.Update(input.CategoryId, category)
                                .Match(
                                    some: (updatedCategory) =>
                                    {
                                        updatedCategory.Parent?.ClearParentAndSubcategories();
                                        updatedCategory.ClearSubcategoriesParentsAndSubCategories();

                                        outputPort.Standard(updatedCategory);
                                    },
                                    none: (ex) => outputPort.Fail(ex));
                        },
                        none: () => outputPort.NotFound()
                    );
                },
                none: () => outputPort.NotFound());
    }
}