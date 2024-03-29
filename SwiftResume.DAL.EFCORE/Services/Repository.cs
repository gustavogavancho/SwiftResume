﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SwiftResume.DAL.EFCORE.Services;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly SwiftResumeDbContext _context;

    public Repository(SwiftResumeDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> Get(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Where(predicate);
    }

    public async Task Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public async Task SaveAsync()
    {
        //TODO: Implement fail safe logic
        try
        {
            var check = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            var check = ex;
        }
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

    public void DetachAllProperties()
    {
        _context.ChangeTracker.Clear();
    }
}
