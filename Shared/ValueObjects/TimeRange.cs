using Shared.Exceptions;

namespace Shared.ValueObjects;

public abstract class TimeRange<T> where T : struct, IComparable<T>
{
    protected TimeRange(T? start, T? end)
    {
        // Allow nulls (incomplete), but if both exist they must be ordered.
        if (start.HasValue && end.HasValue && end.Value.CompareTo(start.Value) < 0)
            throw new TimeRangeInvalidException();

        Start = start;
        End = end;
    }

    public T? Start { get; }
    public T? End { get; }

    /// <summary>
    /// True only when both Start and End are set.
    /// </summary>
    public bool IsComplete => Start.HasValue && End.HasValue;

    // Immutable "setters"
    public TimeRange<T> WithStart(T? start) => Create(start, End);
    public TimeRange<T> WithEnd(T? end) => Create(Start, end);

    // -----------------------------
    // Try Methods (null = incomplete)
    // -----------------------------

    /// <summary>
    /// Tries to check whether value is inside the range.
    /// Half-open: [Start, End)
    /// Returns false if the range is incomplete.
    /// </summary>
    public bool TryContains(T value, out bool result)
    {
        if (!Start.HasValue || !End.HasValue)
        {
            result = default;
            return false;
        }

        result = value.CompareTo(Start.Value) >= 0
              && value.CompareTo(End.Value) < 0;

        return true;
    }

    /// <summary>
    /// Tries to check whether this range fully contains another range.
    /// Half-open semantics.
    /// Returns false if either range is incomplete.
    /// </summary>
    public bool TryContains(TimeRange<T> other, out bool result)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (!Start.HasValue || !End.HasValue || !other.Start.HasValue || !other.End.HasValue)
        {
            result = default;
            return false;
        }

        // this.Start <= other.Start && other.End <= this.End
        result = Start.Value.CompareTo(other.Start.Value) <= 0
              && other.End.Value.CompareTo(End.Value) <= 0;

        return true;
    }

    /// <summary>
    /// Tries to check whether two ranges overlap.
    /// Half-open: [Start, End) overlaps iff intersection length > 0.
    /// Returns false if either range is incomplete.
    /// </summary>
    public bool TryOverlaps(TimeRange<T> other, out bool result)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (!Start.HasValue || !End.HasValue || !other.Start.HasValue || !other.End.HasValue)
        {
            result = default;
            return false;
        }

        // Overlap iff this.Start < other.End AND other.Start < this.End
        result = Start.Value.CompareTo(other.End.Value) < 0
              && other.Start.Value.CompareTo(End.Value) < 0;

        return true;
    }

    // -----------------------------
    // Optional strict wrappers
    // -----------------------------

    public bool Contains(T value)
        => TryContains(value, out var r) ? r
         : throw new TimeRangeInvalidException(); // or IncompleteTimeRangeException

    public bool Contains(TimeRange<T> other)
        => TryContains(other, out var r) ? r
         : throw new TimeRangeInvalidException();

    public bool Overlaps(TimeRange<T> other)
        => TryOverlaps(other, out var r) ? r
         : throw new TimeRangeInvalidException();

    // Equality for value objects
    public override bool Equals(object? obj)
        => obj is TimeRange<T> other
        && Nullable.Equals(Start, other.Start)
        && Nullable.Equals(End, other.End);

    public override int GetHashCode() => HashCode.Combine(Start, End);

    // Factory for derived class
    protected abstract TimeRange<T> Create(T? start, T? end);
}
