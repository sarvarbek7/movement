using Shared.Exceptions;

namespace Shared.ValueObjects;

public sealed class Location : IEquatable<Location>
{
    public Location(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new InvalidLatitudeException(latitude);
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new InvalidLongitudeException(longitude);
        }

        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }
    public double Longitude { get; }

    // Optional: distance calculation (Haversine formula)
    public double DistanceTo(Location other)
    {
        ArgumentNullException.ThrowIfNull(other);

        var R = 6371e3; // Earth radius in meters
        var latitudeInRads = Latitude * Math.PI / 180;
        var otherLatitudeInRads = other.Latitude * Math.PI / 180;
        var latitudeDiffirenceInRads = (other.Latitude - Latitude) * Math.PI / 180;
        var longitudeDiffirenceInRads = (other.Longitude - Longitude) * Math.PI / 180;

        var a = Math.Sin(latitudeDiffirenceInRads / 2) * Math.Sin(latitudeDiffirenceInRads / 2) +
                Math.Cos(latitudeInRads) * Math.Cos(otherLatitudeInRads) *
                Math.Sin(longitudeDiffirenceInRads / 2) * Math.Sin(longitudeDiffirenceInRads / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c; // distance in meters
    }

    public double DistanceToRoute(IReadOnlyList<Location> route)
    {
        ArgumentNullException.ThrowIfNull(route);
        if (route.Count == 0) return 0;

        double totalDistance = 0;
        Location previous = this;

        foreach (var point in route)
        {
            totalDistance += previous.DistanceTo(point);
            previous = point;
        }

        return totalDistance; // total distance along the route
    }

    // Value equality
    public override bool Equals(object? obj) => Equals(obj as Location);

    public bool Equals(Location? other)
    {
        if (other is null) return false;
        return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
    }

    public override int GetHashCode() => HashCode.Combine(Latitude, Longitude);

    public override string ToString() => $"({Latitude}, {Longitude})";
}