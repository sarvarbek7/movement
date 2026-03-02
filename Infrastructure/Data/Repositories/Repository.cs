using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movement.Application.Data.Repositories;
using Shared.Entities;

namespace Movement.Infrastructure.Data.Repositories;

internal class Repository<TContext, TEntity, TId>(TContext context) : IRepository<TEntity, TId>
where TEntity : class, IEntity<TId>
where TId : struct, IEquatable<TId>
where TContext : DbContext
{
    public virtual async Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(entity, cancellationToken);

        if (saveChanges)
        {
            await SaveChangesAsync(cancellationToken);
        }

        return entity;
    }

    public virtual async Task<ICollection<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await context.AddRangeAsync(entities, cancellationToken);

        if (saveChanges)
        {
            await SaveChangesAsync(cancellationToken);
        }

        return [.. entities];
    }

    public virtual void AttachEntities<T>(IEnumerable<T> entities) where T : class
    {
        foreach (T entity in entities)
        {
            AttachEntity(entity);
        }
    }

    public virtual void AttachEntity<T>(T entity) where T : class
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            context.Entry(entity).State = EntityState.Unchanged;
        }
    }

    public virtual async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public virtual async Task BeginTransactionAsync(IsolationLevel isolationLevel,
                                            CancellationToken cancellationToken = default)
    {
        await context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    public virtual async Task<bool> CheckExistsByPredicateAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                        bool ignoreGlobalQueryFilter = false,
                                                        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetAll(predicate,
                                           tracked: false,
                                           ignoreGlobalQueryFilter);

        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.CommitTransactionAsync(cancellationToken);
    }

    public virtual void DetachEntities<T>(IEnumerable<T> entities) where T : class
    {
        foreach (T entity in entities)
        {
            DetachEntity(entity);
        }
    }

    public virtual void DetachEntity<T>(T entity) where T : class
    {
        context.Entry(entity).State = EntityState.Detached;
    }

    public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                                      bool tracked = true,
                                      bool ignoreGlobalQueryFilter = false)
    {
        IQueryable<TEntity> query = context.Set<TEntity>().AsQueryable();

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (ignoreGlobalQueryFilter)
        {
            query = query.IgnoreQueryFilters();
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id,
                                             bool tracked = true,
                                             CancellationToken cancellationToken = default,
                                             ICollection<string>? navigations = null)
    {
        var query = GetAll(entity => entity.Id.Equals(id),
                           tracked,
                           false);

        if (navigations is { Count: > 0 } relations)
        {
            foreach (var rel in relations)
            {
                query = query.Include(rel);
            }
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                              bool tracked = true,
                                              bool ignoreGlobalQueryFilter = false,
                                              CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetAll(predicate,
                                           tracked,
                                           ignoreGlobalQueryFilter);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual TEntity Remove(TEntity entity, bool saveChanges = true)
    {
        context.Remove(entity);

        if (saveChanges)
        {
            context.SaveChanges();
        }

        return entity;
    }

    public virtual ICollection<TEntity> RemoveRange(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        context.RemoveRange(entities);

        if (saveChanges)
        {
            context.SaveChanges();
        }

        return entities.ToList();
    }

    public virtual async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public virtual TEntity Update(TEntity entity, bool saveChanges = true)
    {
        context.Update(entity);

        if (saveChanges)
        {
            context.SaveChanges();
        }

        return entity;
    }

    public virtual ICollection<TEntity> UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        context.UpdateRange(entities);

        if (saveChanges)
        {
            context.SaveChanges();
        }

        return entities.ToList();
    }

    public virtual void LoadReference<TEntityType, TProperty>(TEntityType entity, Expression<Func<TEntityType, TProperty?>> propertyExpression)
        where TEntityType : class
        where TProperty : class
    {
        context.Entry(entity).Reference(propertyExpression).Load();
    }

    public virtual void LoadCollection<TEntityType, TProperty>(TEntityType entity, Expression<Func<TEntityType, IEnumerable<TProperty>>> propertyExpression)
        where TEntityType : class
        where TProperty : class
    {
        context.Entry(entity).Collection(propertyExpression).Load();
    }
}