using Shared.Events;

namespace Shared.Entities;

public interface IEntity<TId> where TId : struct, IEquatable<TId>
{
    TId Id { get; init; }
}