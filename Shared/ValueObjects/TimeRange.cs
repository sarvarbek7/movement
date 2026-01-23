using Shared.Exceptions;

namespace Shared.ValueObjects;

public abstract class TimeRange<T> where T : struct, IComparable<T>
{
    protected TimeRange(T? start, T? end)
    {
        // Allow nulls (incomplete), but if both exist they must be ordered.
        if (start.HasValue && end.HasValue && end.Value.CompareTo(start.Value) <= 0)
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

    public bool TryContains(T value, out bool result)
    {
        result = false;

        // If complete, it's decidable.
        if (IsComplete)
        {
            var s = Start!.Value;
            var e = End!.Value;

            // [s, e): s <= value < e
            result = Comparer<T>.Default.Compare(value, s) >= 0
                  && Comparer<T>.Default.Compare(value, e) < 0;
            return true;
        }

        // If only Start exists:
        if (Start.HasValue)
        {
            // If value < Start => definitely false. Otherwise unknown (End missing).
            if (Comparer<T>.Default.Compare(value, Start.Value) < 0)
            {
                result = false;
                return true;
            }

            return false; // could be in or out depending on End
        }

        // If only End exists:
        if (End.HasValue)
        {
            // If value >= End => definitely false. Otherwise unknown (Start missing).
            if (Comparer<T>.Default.Compare(value, End.Value) >= 0)
            {
                result = false;
                return true;
            }

            return false; // could be in or out depending on Start
        }

        // No bounds => unknown.
        return false;
    }


    public bool TryContains(TimeRange<T> other, out bool result)
    {
        ArgumentNullException.ThrowIfNull(other);
        result = false;

        static int Cmp(T a, T b) => Comparer<T>.Default.Compare(a, b);

        // Determinable TRUE/FALSE when both are complete.
        // A contains B iff A.Start <= B.Start AND B.End <= A.End (half-open containment).
        if (IsComplete && other.IsComplete)
        {
            var aS = Start!.Value;
            var aE = End!.Value;
            var bS = other.Start!.Value;
            var bE = other.End!.Value;

            result = Cmp(aS, bS) <= 0 && Cmp(bE, aE) <= 0;
            return true;
        }

        // Determinable FALSE cases (proof by contradiction), even with incompleteness.

        // If B.Start is known and it's before A.Start -> cannot be contained.
        if (Start.HasValue && other.Start.HasValue && Cmp(other.Start.Value, Start.Value) < 0)
            return true;

        // If B.End is known and it's after A.End -> cannot be contained.
        if (End.HasValue && other.End.HasValue && Cmp(other.End.Value, End.Value) > 0)
            return true;

        // If B.End <= A.Start, then B.Start < B.End <= A.Start => B.Start < A.Start -> cannot be contained.
        // (Relies on non-empty intervals: Start < End.)
        if (Start.HasValue && other.End.HasValue && Cmp(other.End.Value, Start.Value) <= 0)
            return true;

        // If B.Start >= A.End, then B.End > B.Start >= A.End => B.End > A.End -> cannot be contained.
        // (Relies on non-empty intervals: Start < End.)
        if (End.HasValue && other.Start.HasValue && Cmp(other.Start.Value, End.Value) >= 0)
            return true;

        // Otherwise: not enough information to decide.
        return false;
    }


    public bool TryOverlaps(TimeRange<T> other, out bool result)
    {
        ArgumentNullException.ThrowIfNull(other);
        result = false;

        static int Cmp(T a, T b) => Comparer<T>.Default.Compare(a, b);

        bool InHalfOpen(T x, T s, T e) => Cmp(x, s) >= 0 && Cmp(x, e) < 0;   // [s, e)
        bool InOpenClosed(T x, T s, T e) => Cmp(x, s) > 0 && Cmp(x, e) <= 0; // (s, e]

        // 1) If both complete, it's fully decidable.
        if (IsComplete && other.IsComplete)
        {
            var aS = Start!.Value;
            var aE = End!.Value;
            var bS = other.Start!.Value;
            var bE = other.End!.Value;

            // Overlap iff aS < bE AND bS < aE  (half-open)
            result = Cmp(aS, bE) < 0 && Cmp(bS, aE) < 0;
            return true;
        }

        // 2) Certain "no overlap" from known separation (doesn't require completeness).
        // If other starts at/after this ends => cannot overlap.
        if (End.HasValue && other.Start.HasValue && Cmp(other.Start.Value, End.Value) >= 0)
            return true;

        // If this starts at/after other ends => cannot overlap.
        if (Start.HasValue && other.End.HasValue && Cmp(Start.Value, other.End.Value) >= 0)
            return true;

        // 3) Certain "overlap" is only provable when one range is complete and we know
        //    a boundary of the other lies inside it.
        if (IsComplete)
        {
            var aS = Start!.Value;
            var aE = End!.Value;

            // other.Start inside [aS, aE) => overlap guaranteed (under End > Start assumption)
            if (other.Start.HasValue && InHalfOpen(other.Start.Value, aS, aE))
            {
                result = true;
                return true;
            }

            // other.End inside (aS, aE] => overlap guaranteed (under End > Start assumption)
            if (other.End.HasValue && InOpenClosed(other.End.Value, aS, aE))
            {
                result = true;
                return true;
            }
        }

        if (other.IsComplete)
        {
            var bS = other.Start!.Value;
            var bE = other.End!.Value;

            if (Start.HasValue && InHalfOpen(Start.Value, bS, bE))
            {
                result = true;
                return true;
            }

            if (End.HasValue && InOpenClosed(End.Value, bS, bE))
            {
                result = true;
                return true;
            }
        }

        // 4) Otherwise: not enough info.
        return false;
    }

    // Equality for value objects
    public override bool Equals(object? obj)
        => obj is TimeRange<T> other
        && Nullable.Equals(Start, other.Start)
        && Nullable.Equals(End, other.End);

    public override int GetHashCode() => HashCode.Combine(Start, End);

    // Factory for derived class
    protected abstract TimeRange<T> Create(T? start, T? end);
}
