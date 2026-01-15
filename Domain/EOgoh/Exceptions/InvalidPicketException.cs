using Movement.Domain.Common.Exceptions;

namespace Movement.Domain.EOgoh.Exceptions;

public class InvalidPicketException(int? picket = null)
    : BaseException("The provided Picket value is invalid.",
                    Codes.InvalidPicketException,
                    Types.ValidationError,
                    null,
                    picket);