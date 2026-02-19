using System.Text.Json.Serialization;

namespace Movement.Application.Network.Apis.VirtualOffice.Models;

public record User
{
    public int Id { get; init; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; init; } = default!;

    [JsonPropertyName("last_name")]
    public string LastName { get; init; } = default!;

    [JsonPropertyName("middle_name")]
    public string? MiddleName { get; init; }

    public string? Phone { get; init; }
    public string? Image { get; init; }
    public string Pinfl { get; init; } = default!;
    public Position[] Positions { get; init; } = [];
    public Role[] Roles { get; init; } = [];
}