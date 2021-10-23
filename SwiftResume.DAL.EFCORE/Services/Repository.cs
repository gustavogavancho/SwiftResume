using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SwiftResume.DAL.EFCORE.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SwiftResumeDbContextFactory _contextFactory;

        public Repository(SwiftResumeDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TEntity Get(int id)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Set<TEntity>().Where(predicate);
            }
        }

        public void Add(TEntity entity)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                context.Set<TEntity>().Add(entity);
            }
        }

        public void ADdRange(IEnumerable<TEntity> entities)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                context.Set<TEntity>().AddRange(entities);
            }
        }

        public void Remove(TEntity entity)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                context.Set<TEntity>().Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                context.Set<TEntity>().RemoveRange(entities);
            }
        }
    }
}
