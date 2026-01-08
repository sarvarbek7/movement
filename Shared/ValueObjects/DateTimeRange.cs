namespace Shared.ValueObjects;

public sealed class DateTimeRange(DateTime? start, DateTime? end) : TimeRange<DateTime>(start, end)
{
    protected override TimeRange<DateTime> Create(DateTime? start,
                                                  DateTime? end) => new DateTimeRange(start, end);
}