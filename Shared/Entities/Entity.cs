using Shared.Events;

namespace Shared.Entities;

public abstract class Entity<TId, TAudit, TAuditId> : IEntity<TId>,
                                                      IHasDomainEvent,
                                                      ISoftDeleted,
                                                      ICreatedAudit<TAudit, TAuditId>,
                                                      IUpdatedAudit<TAudit, TAuditId>,
                                                      IDeletedAudit<TAudit, TAuditId>
    where TId : struct, IEquatable<TId>
    where TAudit : class, IEntity<TAuditId>
    where TAuditId : struct, IEquatable<TAuditId>
{
    private readonly List<DomainEvent> _domainEvents = [];

    public TId Id { get; init; }
    public bool IsDeleted { get; set; }
    public TAuditId? CreatedById { get; init; }
    public TAudit? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; init; }

    public TAuditId? UpdatedById {get; private set; }

    public TAudit? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedAt {get; private set; }

    public TAuditId? DeletedById {get; private set; }

    public TAudit? DeletedBy { get; set; }

    public DateTimeOffset? DeletedAt {get; private set; }

    public void AddDomainEvent(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void AddDomainEvents(IEnumerable<DomainEvent> events)
    {
        _domainEvents.AddRange(events);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public TEvent GetDomainEvent<TEvent>(Guid id) where TEvent : DomainEvent
    {
        return (TEvent)_domainEvents.First(e => e.Id == id);
    }

    public IReadOnlyCollection<object> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }

    public void RemoveDomainEvent(DomainEvent @event)
    {
        _domainEvents.Remove(@event);
    }

    public void RemoveDomainEvents(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
        {
            _domainEvents.Remove(@event);
        }
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }

    public void SoftDelete(TAuditId deletedById, TAudit? deletedBy = null)
    {
        IsDeleted = true;
        DeletedById = deletedById;
        DeletedBy = deletedBy;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}