using Movement.Domain.Shifts.Entities;

namespace Movement.Application.Requests;

public abstract class ShiftCheckableCommand<TResponse>(Guid shiftId, string pinfl) : PinfCheckableCommand<TResponse>(pinfl)
{
    public Guid ShiftId { get; init; } = shiftId;

    public Shift? Shift { get; private set; }

    public void SetShift(Shift shift)
    {
        Shift = shift;
    }
}