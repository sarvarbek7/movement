using System.Data;
using System.Linq.Expressions;
using Shared.Entities;

namespace Movement.Application.Data.Repositories;

public interface IRepository<TEntity, in TId>
where TEntity : IEntity<TId>
where TId : struct, IEquatable<TId>
{
    void AttachEntity<T>(T entity) where T : class;
    void DetachEntity<T>(T entity) where T : class;
    void DetachEntities<T>(IEnumerable<T> entities) where T : class;
    void AttachEntities<T>(IEnumerable<T> entities) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                               bool tracked = true,
                               bool ignoreGlobalQueryFilter = false);

    Task<TEntity?> GetByIdAsync(TId id,
                                bool tracked = true,
                                CancellationToken cancellationToken = default,
                                ICollection<string>? navigations = null);

    Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                       bool tracked = true,
                                       bool ignoreGlobalQueryFilter = false,
                                       CancellationToken cancellationToken = default);

    Task<bool> CheckExistsByPredicateAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                           bool ignoreGlobalQueryFilter = false,
                                           CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task<ICollection<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default);
    TEntity Update(TEntity entity, bool saveChanges = true);
    ICollection<TEntity> UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = true);
    TEntity Remove(TEntity entity, bool saveChanges = true);
    ICollection<TEntity> RemoveRange(IEnumerable<TEntity> entities, bool saveChanges = true);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    void LoadReference<TEntityType, TProperty>(TEntityType entity, Expression<Func<TEntityType, TProperty?>> propertyExpression)
        where TEntityType : class
        where TProperty : class;

    void LoadCollection<TEntityType, TProperty>(TEntityType entity, Expression<Func<TEntityType, IEnumerable<TProperty>>> propertyExpression)
        where TEntityType : class
        where TProperty : class;
}