using Shared.Exceptions;
using Shared.ValueObjects;

namespace Movement.UnitTests.SharedTests;

public class TimeRangeTests
{
    [Fact]
    public void TimeRangeShouldThrowExceptionForInvalidRange()
    {
        // Arrange
        DateOnly start = new(2024, 6, 10);
        DateOnly end = start.AddDays(-1);

        // Act & Assert
        Assert.Throws<TimeRangeInvalidException>(() => new DateOnlyRange(start, end));
    }

    [Fact]
    public void TimeRangeShouldContainValueWithinRange()
    {
        // Arrange
        DateOnly start = new(2024, 6, 1);
        DateOnly end = new(2024, 6, 30);
        var range = new DateOnlyRange(start, end);
        DateOnly valueInside = new(2024, 6, 15);

        // Act
        bool contains = range.Contains(valueInside);

        // Assert
        Assert.True(contains);
    }

    [Fact]
    public void TimeRangeShouldNotContainValueOutsideRange()
    {
        // Arrange
        DateOnly start = new(2024, 6, 1);
        DateOnly end = new(2024, 6, 30);
        var range = new DateOnlyRange(start, end);
        DateOnly valueOutside = new(2024, 7, 1);

        // Act
        bool contains = range.Contains(valueOutside);

        // Assert
        Assert.False(contains);
    }

    [Theory]
    [InlineData("2024-06-10", "2024-06-20", "2024-06-15", "2024-06-25")]
    [InlineData("2024-06-01", "2024-06-15", "2024-06-10", "2024-06-20")]
    [InlineData("2026-01-01", "2026-01-13", "2025-12-15", "2026-01-05")]
    [InlineData("2024-06-01", "2024-06-30", "2024-06-01", "2024-06-30")]
    public void TimeRangeShouldOverlapWithAnotherRange(string range1Start, string range1End, string range2Start, string range2End)
    {
        // Arrange
        var range1 = new DateOnlyRange(DateOnly.Parse(range1Start), DateOnly.Parse(range1End));
        var range2 = new DateOnlyRange(DateOnly.Parse(range2Start), DateOnly.Parse(range2End));

        // Act
        bool overlaps = range1.Overlaps(range2);

        // Assert
        Assert.True(overlaps);
    }

    [Theory]
    [InlineData("2024-06-01", "2024-06-15", "2024-06-16", "2024-06-30")]
    [InlineData("2026-01-01", "2026-01-10", "2026-01-10", "2026-01-20")]
    public void TimeRangeShouldNotOverlapWithAnotherRange(string range1Start, string range1End, string range2Start, string range2End)
    {
        // Arrange
        var range1 = new DateOnlyRange(DateOnly.Parse(range1Start), DateOnly.Parse(range1End));
        var range2 = new DateOnlyRange(DateOnly.Parse(range2Start), DateOnly.Parse(range2End));

        // Act
        bool overlaps = range1.Overlaps(range2);

        // Assert
        Assert.False(overlaps);
    }

    [Fact]
    public void TimeRangeShouldContainAnotherRange()
    {
        // Arrange
        var outerRange = new DateOnlyRange(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 30));
        var innerRange = new DateOnlyRange(new DateOnly(2024, 6, 10), new DateOnly(2024, 6, 20));

        // Act
        bool contains = outerRange.Contains(innerRange);

        // Assert
        Assert.True(contains);
    }

    [Fact]
    public void TimeRangeShouldNotContainAnotherRange()
    {
        // Arrange
        var outerRange = new DateOnlyRange(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 15));
        var innerRange = new DateOnlyRange(new DateOnly(2024, 6, 10), new DateOnly(2024, 6, 20));

        // Act
        bool contains = outerRange.Contains(innerRange);

        // Assert
        Assert.False(contains);
    }
}