namespace Movement.Domain.Common.Exceptions;

public abstract class BaseException(string message = "Some unknown error has occurred",
                                    string code = Codes.InternalError,
                                    Types type = Types.InternalError,
                                    Exception? innerException = null,
                                    params object?[] arguments) : Exception(message,
                                                                            innerException)
{
    public string Code { get; init; } = code;
    public Types Type { get; init; } = type;
    public IEnumerable<object?> Arguments { get; init; } = arguments;
}