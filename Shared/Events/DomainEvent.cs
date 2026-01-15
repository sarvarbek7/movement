namespace Shared.Events;

public abstract class DomainEvent : IEquatable<DomainEvent>
{
    public DomainEvent()
    {
        Id = Guid.CreateVersion7();
        OccurredAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTimeOffset OccurredAt { get; init; }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}, OccurredAt={OccurredAt}]";
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DomainEvent other)
        {
            return false;
        }

        return Equals(other);
    }

    public bool Equals(DomainEvent? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id.Equals(other.Id);
    }
}