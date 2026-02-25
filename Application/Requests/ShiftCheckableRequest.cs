using Movement.Domain.Shifts.Entities;

namespace Movement.Application.Requests;

public abstract class ShiftCheckableQuery<TResponse>(Guid shiftId, string pinfl) : PinfCheckableQuery<TResponse>(pinfl)
{
    public Guid ShiftId { get; init; } = shiftId;

    public Shift? Shift { get; private set; }

    public void SetShift(Shift shift)
    {
        Shift = shift;
    }
}