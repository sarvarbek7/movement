namespace Movement.IntegrationsGateway.Requests;

public record PostDu2Integration(long Du1Id,
                                 string StationCode,
                                 int? WayId,
                                 string? WayName,
                                 int? ParkId,
                                 string? ParkName,
                                 DateTime MessagedTime,
                                 int Type,
                                 string? IndexPoezd = null);