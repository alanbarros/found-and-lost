using System.Linq.Expressions;
using Optional;

namespace Application.Repository
{
    public interface IBaseRepository<TDomain> where TDomain : Domain.Entities.BaseDomain
    {
        Option<TDomain> Find(Guid id);
        Option<TDomain> Find(Expression<Func<TDomain, bool>> predicado);
        Option<int> Add(TDomain domain);
        Option<int> AddRange(IEnumerable<TDomain> domain);
    }
}