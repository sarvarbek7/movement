using System.Diagnostics;
using Movement.Domain.EOgoh.Exceptions;

namespace Movement.Domain.EOgoh.ValueObjects;

[DebuggerDisplay("Km: {Km}, Picket: {Picket}")]
public record KmAndPicket
{
    public int? Km { get; init; }
    public byte? Picket { get; init; }

    public KmAndPicket(int? km, byte? picket)
    {
        if (km is < 0)
        {
            throw new InvalidKmException(km);
        }
        
        if (picket is < 0 or >= 10)
        {
            throw new InvalidPicketException(picket);
        }

        Km = km;
        Picket = picket;
    }

    public override string ToString() => $"Km: {Km}, Picket: {Picket}";
}