using Movement.MessagingContracts.Enakl;

namespace Movement.IntegrationsGateway.Requests;

public record PostDu2Integration(long Du1Id,
                                 string StationCode,
                                 int? WayId,
                                 string? WayName,
                                 int? ParkId,
                                 string? ParkName,
                                 DateTime MessagedTime,
                                 int Type,
                                 string? IndexPoezd = null)
{
    public PostDu2IntegionEvent ToEvent()
    {
        return new PostDu2IntegionEvent
        {
            Du1Id = Du1Id,
            StationCode = StationCode,
            WayId = WayId,
            WayName = WayName,
            ParkId = ParkId,
            ParkName = ParkName,
            MessagedTime = MessagedTime,
            Type = Type,
            IndexPoezd = IndexPoezd
        };
    }
}