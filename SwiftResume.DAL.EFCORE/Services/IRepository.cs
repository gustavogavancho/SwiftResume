﻿using System.Linq.Expressions;


namespace SwiftResume.DAL.EFCORE.Services;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> Get(int id);
    Task<IEnumerable<TEntity>> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    Task Add(TEntity entity);
    Task AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

    Task SaveAsync();
    bool HasChanges();
    void DetachAllProperties();
}
