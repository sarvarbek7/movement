using System.Text.Json.Serialization;

namespace Movement.Application.Network.Apis.VirtualOffice.Models;

public record Department
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Level { get; init; } = default!;
    public string Code { get; init; } = default!;

    public Workplace? Workplace { get; init; }
}