using Shared.Exceptions;

namespace Shared.ValueObjects;

public abstract class TimeRange<T> where T : struct, IComparable<T>
{
    protected TimeRange(T? start, T? end)
    {
        if (start.HasValue && end.HasValue && end.Value.CompareTo(start.Value) < 0)
            throw new TimeRangeInvalidException();

        Start = start;
        End = end;
    }

    public T? Start { get; }
    public T? End { get; }

    // Immutable "setters"
    public TimeRange<T> WithStart(T? start) => Create(start, End);
    public TimeRange<T> WithEnd(T? end) => Create(Start, end);

    // Check if value is inside the range
    public bool Contains(T other)
    {
        bool afterStart = !Start.HasValue || other.CompareTo(Start.Value) >= 0;
        bool beforeEnd = !End.HasValue || other.CompareTo(End.Value) <= 0;
        
        return afterStart && beforeEnd;
    }

    public bool Contains(TimeRange<T> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        var thisStart = Start;
        var thisEnd = End;

        var otherStart = other.Start;
        var otherEnd = other.End;

        bool startIsInside = !thisStart.HasValue || !otherStart.HasValue || thisStart.Value.CompareTo(otherStart.Value) <= 0;
        bool endIsInside = !thisEnd.HasValue || !otherEnd.HasValue || thisEnd.Value.CompareTo(otherEnd.Value) >= 0;

        return startIsInside && endIsInside;
    }


    // Check if two ranges overlap
    public bool Overlaps(TimeRange<T> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        var thisStart = Start;
        var thisEnd = End;

        var otherStart = other.Start;
        var otherEnd = other.End;

        bool thisStartsBeforeOtherEnds = !thisEnd.HasValue || !otherStart.HasValue || thisEnd.Value.CompareTo(otherStart.Value) >= 0;
        bool thisEndsAfterOtherStarts = !thisStart.HasValue || !otherEnd.HasValue || thisStart.Value.CompareTo(otherEnd.Value) <= 0;

        return thisStartsBeforeOtherEnds && thisEndsAfterOtherStarts;
    }

    // Equality for value objects
    public override bool Equals(object? obj)
    {
        if (obj is not TimeRange<T> other) return false;
        return Nullable.Equals(Start, other.Start) && Nullable.Equals(End, other.End);
    }

    public override int GetHashCode() => HashCode.Combine(Start, End);

    // Factory for derived class
    protected abstract TimeRange<T> Create(T? start, T? end);
}