using Shared.Exceptions;
using Shared.ValueObjects;

namespace Movement.UnitTests.SharedTests;

public class TimeRangeTests
{
    // -------------------------
    // Helpers
    // -------------------------
    private static TimeOnly? P(string? s) => s is null ? null : TimeOnly.Parse(s);
    private static TimeOnlyRange R(string? start, string? end) => new(P(start), P(end));
    private static TimeOnly V(string s) => TimeOnly.Parse(s);

    // -------------------------
    // Constructor / Invariants
    // -------------------------

    [Theory]
    [InlineData("09:00", "08:00")]
    [InlineData("12:30", "12:30")]
    public void TimeRangeShouldThrowExceptionForInvalidRange(string start, string end)
    {
        var startTime = TimeOnly.Parse(start);
        var endTime = TimeOnly.Parse(end);

        Assert.Throws<TimeRangeInvalidException>(() => new TimeOnlyRange(startTime, endTime));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("09:00", null)]
    [InlineData(null, "15:00")]
    [InlineData("09:00", "15:00")]
    public void TimeRange_AllowsNulls_AndValidOrdering(string? start, string? end)
    {
        var ex = Record.Exception(() => R(start, end));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("09:00", "15:00", true)]
    [InlineData("09:00", null, false)]
    [InlineData(null, "15:00", false)]
    [InlineData(null, null, false)]
    public void IsComplete_ReflectsWhetherBothBoundsExist(string? start, string? end, bool expected)
    {
        var r = R(start, end);
        Assert.Equal(expected, r.IsComplete);
    }

    // -------------------------
    // WithStart / WithEnd
    // -------------------------

    [Fact]
    public void WithStart_And_WithEnd_ReturnNewInstances_AndDoNotMutateOriginal()
    {
        var original = R("09:00", "15:00");

        var withStart = original.WithStart(V("10:00"));
        var withEnd = original.WithEnd(V("16:00"));

        // original unchanged
        Assert.Equal(V("09:00"), original.Start);
        Assert.Equal(V("15:00"), original.End);

        // new values applied
        Assert.IsType<TimeOnlyRange>(withStart);
        Assert.Equal(V("10:00"), withStart.Start);
        Assert.Equal(V("15:00"), withStart.End);

        Assert.IsType<TimeOnlyRange>(withEnd);
        Assert.Equal(V("09:00"), withEnd.Start);
        Assert.Equal(V("16:00"), withEnd.End);
    }

    [Fact]
    public void WithStart_ShouldThrow_WhenItBreaksOrdering()
    {
        var r = R("09:00", "15:00");
        Assert.Throws<TimeRangeInvalidException>(() => r.WithStart(V("15:00"))); // start == end
        Assert.Throws<TimeRangeInvalidException>(() => r.WithStart(V("16:00"))); // start > end
    }

    [Fact]
    public void WithEnd_ShouldThrow_WhenItBreaksOrdering()
    {
        var r = R("09:00", "15:00");
        Assert.Throws<TimeRangeInvalidException>(() => r.WithEnd(V("09:00"))); // end == start
        Assert.Throws<TimeRangeInvalidException>(() => r.WithEnd(V("08:00"))); // end < start
    }

    // -------------------------
    // Equality / Hash
    // -------------------------

    [Fact]
    public void Equality_UsesStartAndEnd_AsValueObject()
    {
        var a = R("09:00", "15:00");
        var b = R("09:00", "15:00");
        var c = R("10:00", "15:00");
        var d = R("09:00", null);

        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());

        Assert.False(a.Equals(c));
        Assert.False(a.Equals(d));
        Assert.False(a.Equals(null));
    }

    // =========================================================
    // ii) TryContains(T value, out bool result)
    // =========================================================

    [Theory]
    // Complete: inside / boundaries / outside
    [InlineData("09:00", "15:00", "10:00", true,  true)]
    [InlineData("09:00", "15:00", "09:00", true,  true)]  // start inclusive
    [InlineData("09:00", "15:00", "14:59", true,  true)]
    [InlineData("09:00", "15:00", "15:00", true,  false)] // end exclusive
    [InlineData("09:00", "15:00", "08:59", true,  false)]
    [InlineData("09:00", "15:00", "18:00", true,  false)]

    // Start-only: can only prove false when value < start
    [InlineData("09:00", null,    "08:00", true,  false)]
    [InlineData("09:00", null,    "09:00", false, false)]
    [InlineData("09:00", null,    "12:00", false, false)]

    // End-only: can only prove false when value >= end
    [InlineData(null,    "15:00", "18:00", true,  false)]
    [InlineData(null,    "15:00", "14:00", false, false)]
    [InlineData(null,    "15:00", "09:00", false, false)]

    // Unknown bounds
    [InlineData(null,    null,    "12:00", false, false)]
    public void TryContains_Value_CoversAllDeterminableAndUnknownCases(
        string? start, string? end, string value,
        bool expectedDecidable, bool expectedResult)
    {
        var range = R(start, end);

        var decided = range.TryContains(V(value), out var result);

        Assert.Equal(expectedDecidable, decided);
        Assert.Equal(expectedResult, result);
    }

    // =========================================================
    // iii) TryContains(TimeRange<T> other, out bool result)
    // =========================================================

    [Theory]
    // A complete, B complete: true
    [InlineData("09:00", "15:00", "10:00", "12:00", true,  true)]
    [InlineData("09:00", "15:00", "09:00", "15:00", true,  true)]
    [InlineData("09:00", "15:00", "10:00", "15:00", true,  true)]
    [InlineData("09:00", "15:00", "09:00", "14:00", true,  true)]

    // A complete, B complete: false
    [InlineData("09:00", "15:00", "08:00", "12:00", true,  false)] // starts before A
    [InlineData("09:00", "15:00", "10:00", "16:00", true,  false)] // ends after A
    [InlineData("09:00", "15:00", "06:00", "09:00", true,  false)] // ends at A.Start => not contained
    [InlineData("09:00", "15:00", "15:00", "18:00", true,  false)] // starts at A.End => not contained

    // A complete, B incomplete: determinable false by contradiction
    [InlineData("09:00", "15:00", "08:00", null,    true,  false)] // B.Start < A.Start
    [InlineData("09:00", "15:00", null,    "18:00", true,  false)] // B.End > A.End
    [InlineData("09:00", "15:00", null,    "09:00", true,  false)] // B.End <= A.Start
    [InlineData("09:00", "15:00", "15:00", null,    true,  false)] // B.Start >= A.End

    // A complete, B incomplete: unknown
    [InlineData("09:00", "15:00", "10:00", null,    false, false)]
    [InlineData("09:00", "15:00", null,    "12:00", false, false)]
    [InlineData("09:00", "15:00", null,    null,    false, false)]

    // A incomplete, still sometimes determinable false (your proof branches)
    [InlineData("09:00", null,    "08:00", "10:00", true,  false)] // A.Start known, B.Start < A.Start
    [InlineData(null,    "15:00", "10:00", "16:00", true,  false)] // A.End known, B.End > A.End

    // A incomplete: unknown
    [InlineData("09:00", null,    "10:00", "12:00", false, false)]
    [InlineData(null,    "15:00", "10:00", "12:00", false, false)]
    [InlineData(null,    null,    "10:00", "12:00", false, false)]
    public void TryContains_Range_CoversAllDeterminableAndUnknownCases(
        string? aStart, string? aEnd,
        string? bStart, string? bEnd,
        bool expectedDecidable, bool expectedResult)
    {
        var a = R(aStart, aEnd);
        var b = R(bStart, bEnd);

        var decided = a.TryContains(b, out var result);

        Assert.Equal(expectedDecidable, decided);
        Assert.Equal(expectedResult, result);
    }

    // =========================================================
    // i) TryOverlaps(TimeRange<T> other, out bool result)
    // =========================================================

    [Theory]
    // A complete, B complete: overlap true/false including half-open touch points
    [InlineData("09:00", "15:00", "10:00", "14:00", true,  true)]
    [InlineData("09:00", "15:00", "06:00", "10:00", true,  true)]
    [InlineData("09:00", "15:00", "14:00", "18:00", true,  true)]
    [InlineData("09:00", "15:00", "06:00", "09:00", true,  false)] // touches at A.Start
    [InlineData("09:00", "15:00", "15:00", "18:00", true,  false)] // touches at A.End

    // A complete, B start-only
    [InlineData("09:00", "15:00", "06:00", null,    false, false)] // unknown
    [InlineData("09:00", "15:00", "10:00", null,    true,  true)]  // start inside => guaranteed overlap
    [InlineData("09:00", "15:00", "09:00", null,    true,  true)]  // start == A.Start
    [InlineData("09:00", "15:00", "15:00", null,    true,  false)] // start == A.End => no overlap
    [InlineData("09:00", "15:00", "17:00", null,    true,  false)] // start after A.End => no overlap

    // A complete, B end-only
    [InlineData("09:00", "15:00", null,    "18:00", false, false)] // unknown
    [InlineData("09:00", "15:00", null,    "10:00", true,  true)]  // end inside => guaranteed overlap
    [InlineData("09:00", "15:00", null,    "09:00", true,  false)] // end == A.Start => no overlap
    [InlineData("09:00", "15:00", null,    "15:00", true,  true)]  // end == A.End => overlap

    // Both unknown
    [InlineData("09:00", "15:00", null,    null,    false, false)]

    // Branches where OTHER is complete and THIS is incomplete (proving overlap)
    [InlineData("10:00", null,    "09:00", "15:00", true,  true)]  // this.Start inside other
    [InlineData(null,    "12:00", "09:00", "15:00", true,  true)]  // this.End inside other

    // Separation proofs with incomplete ranges
    [InlineData(null,    "15:00", "15:00", null,    true,  false)] // other.Start >= this.End => no overlap
    [InlineData("09:00", null,    null,    "09:00", true,  false)] // this.Start >= other.End => no overlap

    // Incomplete not separable => unknown
    [InlineData(null,    "15:00", "10:00", null,    false, false)]
    [InlineData("09:00", null,    "10:00", null,    false, false)]

    // Both incomplete unknown
    [InlineData(null,    null,    null,    null,    false, false)]
    public void TryOverlaps_Range_CoversAllDeterminableAndUnknownCases(
        string? aStart, string? aEnd,
        string? bStart, string? bEnd,
        bool expectedDecidable, bool expectedResult)
    {
        var a = R(aStart, aEnd);
        var b = R(bStart, bEnd);

        var decided = a.TryOverlaps(b, out var result);

        Assert.Equal(expectedDecidable, decided);
        Assert.Equal(expectedResult, result);
    }
}
