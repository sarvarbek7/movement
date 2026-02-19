using System.Text.Json.Serialization;

namespace Movement.Application.Network.Apis.VirtualOffice.Models;

public record Position
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Workplace Workplace { get; set; } = default!;
    public Department? Department { get; set; }
}