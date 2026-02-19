using System.Text.Json.Serialization;

namespace Movement.Application.Network.Apis.VirtualOffice.Models;

public record Workplace
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
   
   [JsonPropertyName("parent_id")]
    public int? ParentId { get; init; }
}