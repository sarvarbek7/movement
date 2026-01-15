using Shared.Entities;

namespace Movement.Domain.Users.Entities;

public class User : IEntity<int>, ISoftDeleted, IHasExternalId<int>
{
    public int Id { get; init; }
    public int ExternalId { get; private set; }
    public bool IsDeleted { get; private set; }
}