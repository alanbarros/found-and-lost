using Application.Boundaries;
using Domain.Entities;

namespace Application.UseCases.UcCategory;

public interface IAddCategoryUseCase : IUseCase<AddCategoryRequest, Category> { }

public interface IDeleteCategoryUseCase : IUseCase<DeleteCategoryRequest, string, Exception> { }

public interface IUpdateCategoryUseCase : IUseCase<UpdateCategoryRequest, Category, Exception> { }

public interface IFindCategoryUseCase : IUseCase<FindCategoryRequest, Category> { }

public interface IListCategoryUseCase : IUseCase<ListCategoryRequest, PaginationOutput<Category>> { }
