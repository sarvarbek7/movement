using Shared.Entities;

namespace Movement.Domain.Users.Entities;

public class Department : IEntity<int>, ISoftDeleted, IHasExternalId<int>
{
    public const string LStation = "station";
    public const string LDepartment = "department";
    public const string LCenter = "center";

    public int Id { get; init; }
    public string Name { get; private set; } = default!;
    public string Level { get; private set; } = default!;
    public string? Code { get; private set; }
    public int? WorkplaceId { get; private set; }
    public Workplace? Workplace { get; private set; }
    public int ExternalId { get; private set; }
    public bool IsDeleted { get; private set; }
}