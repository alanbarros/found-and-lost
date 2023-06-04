using AutoMapper;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Data.Repository
{
    public abstract class BaseRepository<TEntity, TDomain>
        where TEntity : Entities.DefaultDbEntity
        where TDomain : Domain.Entities.BaseDomain
    {
        protected DbContext Context { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }
        private readonly IMapper _mapper;

        public BaseRepository(IMapper mapper)
        {
            _mapper = mapper;
            Context = new Context.EFContext();

            DbSet = Context.Set<TEntity>();

            this.Context.Set<Category>()
                .FirstOrDefault()
                .SomeNotNull()
                .MatchNone(() => Context.Database.Migrate());
        }

        public Option<TDomain> Find(params object[] keyValues)
        {
            var result = DbSet.Find(keyValues);

            var mappedResult = _mapper.Map<TEntity, TDomain>(result);

            return mappedResult.SomeNotNull();
        }

        public Option<int> Add(TDomain domain)
        {
            var entity = _mapper.Map<TDomain, TEntity>(domain);

            DbSet.Add(entity);

            var result = Context.SaveChanges();

            return result.SomeWhen(x => x > 0);
        }

        public Option<int> AddRange(IEnumerable<TDomain> domain)
        {
            var entities = _mapper.Map<IEnumerable<TDomain>, IEnumerable<TEntity>>(domain);

            DbSet.AddRange(entities);

            var result = Context.SaveChanges();

            return result.SomeWhen(x => x > 0);
        }
    }
}