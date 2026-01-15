namespace Shared.Entities;

public interface ICreatedAudit<TAudit, TAuditId>
    where TAudit : class, IEntity<TAuditId>
    where TAuditId : struct, IEquatable<TAuditId>
{
    TAuditId? CreatedById { get; init; }
    TAudit? CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; init; }
}