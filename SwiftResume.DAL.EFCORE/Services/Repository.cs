using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SwiftResume.DAL.EFCORE.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SwiftResumeDbContextFactory _contextFactory;

        public Repository(SwiftResumeDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<TEntity> Get(int id)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Set<TEntity>().Where(predicate);
            }
        }

        public async Task Add(TEntity entity)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<TEntity>().AddAsync(entity);
            }
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<TEntity>().AddRangeAsync(entities);
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
