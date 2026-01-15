using Shared.Events;

namespace Shared.Entities;

public interface IEntity<TId> where TId : struct, IEquatable<TId>
{
    TId Id { get; init; }

    void ClearDomainEvents();
    IReadOnlyCollection<object> GetDomainEvents();
    void AddDomainEvent(DomainEvent @event);
    TEvent GetDomainEvent<TEvent>(Guid id) where TEvent : DomainEvent;
}