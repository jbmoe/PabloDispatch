namespace PabloDispatch.Api.Exceptions;

public class NoRequestResultException : Exception
{
    public NoRequestResultException(Type requestType, Type resultType, string? message = null)
        : base(message ?? $"No result was found for request {requestType} with result {resultType}.")
    {
    }
}