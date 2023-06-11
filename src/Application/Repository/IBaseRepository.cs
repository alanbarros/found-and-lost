using System.Linq.Expressions;
using Application.Boundaries;
using Optional;

namespace Application.Repository
{
    public interface IBaseRepository<TDomain> where TDomain : Domain.Entities.BaseDomain
    {
        Option<TDomain> Find(Guid id);
        Option<TDomain> Find(Expression<Func<TDomain, bool>> predicado);
        Option<int> Add(TDomain domain);
        Option<int> AddRange(IEnumerable<TDomain> domain);

        Option<TDomain, Exception> Update(Guid id, TDomain domain);

        Option<bool, Exception> Delete(Guid id);

        PaginationOutput<TDomain> List(Expression<Func<TDomain, bool>> predicado, PaginationInput pagination);
    }
}