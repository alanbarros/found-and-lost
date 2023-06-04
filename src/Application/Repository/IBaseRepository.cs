using Optional;

namespace Application.Repository
{
    public interface IBaseRepository<TDomain> where TDomain : Domain.Entities.BaseDomain
    {
        Option<TDomain> Find(params object[] keyValues);
        Option<int> Add(TDomain domain);
        Option<int> AddRange(IEnumerable<TDomain> domain);
    }
}