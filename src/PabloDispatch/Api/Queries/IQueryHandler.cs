namespace PabloDispatch.Api.Queries;

/// <summary>
/// A query handler for handling queries of type <typeparamref name="TQuery"/> with return type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TQuery">The command to handle.</typeparam>
/// <typeparam name="TResult">The return type of the query.</typeparam>
public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}