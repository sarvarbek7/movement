using Shared.Entities;

namespace Movement.Domain.Users.Entities;

public class Position : IEntity<int>, ISoftDeleted, IHasExternalId<int>
{
    public int Id { get; init; }
    public string Name { get; private set; } = default!;
    public int WorkplaceId { get; private set; }
    public Workplace? Workplace { get; private set; } = default!;
    public int DepartmentId { get; private set; }
    public Department? Department { get; private set; } = default!;
    public int ExternalId { get; private set; }
    public bool IsDeleted { get; private set; }
}