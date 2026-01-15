namespace Shared.Entities;

public interface ISoftDeleted
{
    bool IsDeleted { get; set; }

    public void SoftDelete();

    public void Restore();
}