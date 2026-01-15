namespace Shared.Events;

public abstract record DomainEvent
{
    public DomainEvent()
    {
        Id = Guid.CreateVersion7();
        OccurredAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTimeOffset OccurredAt { get; init; }
}