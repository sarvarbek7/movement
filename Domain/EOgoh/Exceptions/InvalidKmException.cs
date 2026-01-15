using Movement.Domain.Common.Exceptions;

namespace Movement.Domain.EOgoh.Exceptions;

public class InvalidKmException(int? km = null)
    : BaseException("The provided Km value is invalid.",
                    Codes.InvalidKmException,
                    Types.ValidationError,
                    null,
                    km);