namespace PabloDispatch.Api.Requests;

/// <summary>
/// Marker interface to represent a request with no return type.
/// </summary>
public interface IRequest
{
}

/// <summary>
/// Marker interface to represent a request with a return type
/// </summary>
/// <typeparam name="TResponse">Return type.</typeparam>
public interface IRequest<out TResponse>
{
}