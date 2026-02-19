namespace Movement.Application.Network.Apis.VirtualOffice.Models;

public record Role
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
}