namespace Shared.ValueObjects;

public sealed class DateTimeOffsetRange(DateTimeOffset? start, DateTimeOffset? end) : TimeRange<DateTimeOffset>(start, end)
{
    protected override TimeRange<DateTimeOffset> Create(DateTimeOffset? start,
                                                        DateTimeOffset? end) => new DateTimeOffsetRange(start, end);
}