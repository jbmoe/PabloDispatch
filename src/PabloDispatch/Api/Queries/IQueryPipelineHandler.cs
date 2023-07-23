namespace PabloDispatch.Api.Queries;

public interface IQueryPipelineHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}