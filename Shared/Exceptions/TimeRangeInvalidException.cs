namespace Shared.Exceptions;

public sealed class TimeRangeInvalidException : ArgumentException
{
    public TimeRangeInvalidException()
        : base("Time range is invalid: end must not be earlier than start.")
    {
    }
}