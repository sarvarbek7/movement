namespace Shared.Entities;

public interface IDeletedAudit<TAudit, TAuditId> : ISoftDeleted
    where TAudit : IEntity<TAuditId>
    where TAuditId : struct, IEquatable<TAuditId>
{
    TAuditId? DeletedById { get; }
    TAudit? DeletedBy { get; set; }
    DateTimeOffset? DeletedAt { get; }
}