namespace Shared.ValueObjects;

public sealed class DateOnlyRange(DateOnly? start, DateOnly? end) : TimeRange<DateOnly>(start, end)
{
    protected override TimeRange<DateOnly> Create(DateOnly? start,
                                                  DateOnly? end) => new DateOnlyRange(start, end);
}