using System.Linq.Expressions;
using AutoMapper;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Data.Repository
{
    public abstract class BaseRepository<TEntity, TDomain>
        where TEntity : Entities.DefaultDbEntity<TDomain>
        where TDomain : Domain.Entities.BaseDomain
    {
        const string NOT_FOUND_MESSAGE = "A categoria com a chave informada não foi localizada no banco de dados";

        protected DbContext Context { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }
        private readonly IMapper _mapper;

        public BaseRepository(IMapper mapper)
        {
            _mapper = mapper;
            Context = new Context.EFContext();

            DbSet = Context.Set<TEntity>();

            Option<bool> VerificarDataBase()
            {
                try
                {
                    return this.Context.Set<Category>()
                        .Any()
                        .SomeNotNull();
                }
                catch
                {
                    return Option.None<bool>();
                }
            }

            VerificarDataBase().MatchNone(() => Context.Database.Migrate());
        }

        public Option<TDomain> Find(Guid id)
        {
            var result = DbSet.Find(id);

            var mappedResult = _mapper.Map<TEntity, TDomain>(result);

            return mappedResult.SomeNotNull();
        }

        public Option<TDomain> Find(Expression<Func<TDomain, bool>> predicado)
        {
            var mapPredicado = _mapper.Map<Expression<Func<TEntity, bool>>>(predicado);

            var result = DbSet.FirstOrDefault(mapPredicado);

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

        public Option<TDomain, Exception> Update(Guid id, TDomain domain) =>
        DbSet.Find(id).SomeNotNull().Match(
            some: (dbEntity) =>
            {
                dbEntity.UpdatedAt = DateTime.UtcNow;
                dbEntity.Update(domain);
                DbSet.Update(dbEntity);

                try
                {
                    Context.SaveChanges();
                    domain = _mapper.Map<TEntity, TDomain>(dbEntity);
                }
                catch (Exception ex)
                {
                    return Option.None<TDomain, Exception>(new DbUpdateException(ex.Message));
                }

                return domain.Some<TDomain, Exception>();
            },
            () => Option.None<TDomain, Exception>(
                new KeyNotFoundException(NOT_FOUND_MESSAGE))
        );

        public Option<bool, Exception> Delete(Guid id) =>
        DbSet.Find(id).SomeNotNull()
        .Match(some: (dbEntity) =>
        {
            try
            {
                DbSet.Remove(dbEntity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Option.None<bool, Exception>(ex);
            }

            return Option.Some<bool, Exception>(true);
        }, none: () => Option.None<bool, Exception>(
            new KeyNotFoundException(NOT_FOUND_MESSAGE)));
    }
}