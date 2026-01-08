namespace Shared.Exceptions;

public sealed class InvalidLatitudeException(double latitude) : ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");