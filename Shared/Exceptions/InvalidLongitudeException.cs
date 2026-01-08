namespace Shared.Exceptions;

public sealed class InvalidLongitudeException(double longitude) : ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180.");