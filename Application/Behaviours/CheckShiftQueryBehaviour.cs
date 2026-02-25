using Mediator;
using Movement.Application.Requests;

namespace Movement.Application.Behaviours;

public class CheckShiftQueryBehaviour<TRequest, TResponse>() :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : ShiftCheckableQuery<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        // TODO: Implement shift checking logic here, such as validating the shift ID and retrieving the associated shift from the database. If the shift is not found, throw a ShiftNotFoundException.
        // Additionally, you may want to check if the user associated with the request has the necessary permissions to access the shift information.

        Console.WriteLine($"Checking Shift ID: {message.ShiftId} for PINFL: {message.Pinfl}"); // Debug log for shift checking

        return await next(message, cancellationToken);
    }
}