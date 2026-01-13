namespace Movement.MessagingContracts.Enakl;

public sealed record PostDu2IntegionEvent
{
    public long Du1Id {get; init;}
    public string StationCode {get; init;} = null!;
    public int? WayId {get; init;}
    public string? WayName {get; init;}
    public int? ParkId {get; init;}
    public string? ParkName {get; init;}
    public DateTime MessagedTime {get; init;}
    public int Type {get; init;}
    public string? IndexPoezd {get; init;}
}