namespace PabloDispatch.Api.Exceptions;

public class RequestPipelineHandlerNotFoundException : Exception
{
    public RequestPipelineHandlerNotFoundException(Type requestType, string? message = null)
        : base(message ?? $"RequestPipelineHandler not found for request {requestType}.")
    {
    }

    public RequestPipelineHandlerNotFoundException(Type requestType, Type resultType, string? message = null)
        : base(message ?? $"RequestPipelineHandler not found for request {requestType} with result {resultType}.")
    {
    }
}