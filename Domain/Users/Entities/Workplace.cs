using Shared.Entities;

namespace Movement.Domain.Users.Entities;

public class Workplace : IEntity<int>, ISoftDeleted, IHasExternalId<int>
{
    public int Id { get; init; }
    public string Name { get; private set; } = default!;
    public int? ParentId { get; private set; }
    public Workplace? Parent { get; private set; }
    public int ExternalId { get; private set; }
    public bool IsDeleted { get; private set; }
    public List<Department> Departments { get; private set; } = [];
}