using Movement.Domain.Common.Exceptions;

namespace Movement.Domain.Users.Exceptions;

public class UserNotFoundByPinflException(string pinfl)
    : BaseException("No user found with the provided PINFL.",
                    Codes.UserNotFoundByPinflException,
                    Types.NotFound,
                    null,
                    pinfl)
{
}