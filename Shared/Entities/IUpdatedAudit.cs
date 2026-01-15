namespace Shared.Entities;

public interface IUpdatedAudit<TAudit, TAuditId>
    where TAudit : class, IEntity<TAuditId>
    where TAuditId : struct, IEquatable<TAuditId>
{
    TAuditId? UpdatedById { get; }
    TAudit? UpdatedBy { get; set; }
    DateTimeOffset? UpdatedAt { get; }
}