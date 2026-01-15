namespace Shared.Entities;

public interface ISoftDeleted
{
    bool IsDeleted { get; }
}