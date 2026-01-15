using Shared.Entities;

namespace Movement.Domain.Users.Entities;

public class Role : IEntity<int>, ISoftDeleted, IHasExternalId<int>
{
    public int Id { get; init; }
    public string Name { get; private set; } = default!;
    public int ExternalId { get; private set; }
    public bool IsDeleted { get; private set; }
}