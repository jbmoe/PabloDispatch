namespace PabloDispatch.Api.Exceptions;

public class RequestHandlerNotFoundException : Exception
{
    public RequestHandlerNotFoundException(Type requestType, Type resultType, string? message = null)
        : base(message ?? $"RequestHandler not found for request {requestType} with result {resultType}.")
    {
    }

    public RequestHandlerNotFoundException(Type requestType, string? message = null)
        : base(message ?? $"RequestHandler not found for request {requestType}.")
    {
    }
}