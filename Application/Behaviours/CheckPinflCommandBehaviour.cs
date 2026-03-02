using Mediator;
using Movement.Application.Requests;

namespace Movement.Application.Behaviours;

public class CheckPinfCommandBehaviour<TRequest, TResponse>() :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : PinfCheckableCommand<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        // TODO: Implement PINFL checking logic here, such as validating the PINFL format and retrieving the associated user from the database. If the user is not found, throw a UserNotFoundByPinflException.

        return await next(message, cancellationToken);
    }
}
