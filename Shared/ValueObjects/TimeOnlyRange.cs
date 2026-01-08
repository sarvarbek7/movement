namespace Shared.ValueObjects;

public sealed class TimeOnlyRange(TimeOnly? start, TimeOnly? end) : TimeRange<TimeOnly>(start, end)
{
    protected override TimeRange<TimeOnly> Create(TimeOnly? start,
                                                  TimeOnly? end) => new TimeOnlyRange(start, end);
}