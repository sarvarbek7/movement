using Shared.Events;

namespace Shared.Entities;

public interface IHasDomainEvent
{
    void ClearDomainEvents();
    IReadOnlyCollection<object> GetDomainEvents();
    void AddDomainEvent(DomainEvent @event);
    void AddDomainEvents(IEnumerable<DomainEvent> events);
    void RemoveDomainEvent(DomainEvent @event);
    void RemoveDomainEvents(IEnumerable<DomainEvent> events);
    TEvent GetDomainEvent<TEvent>(Guid id) where TEvent : DomainEvent;
}