using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Requests;
using PabloDispatch.Api.Services;
using PabloDispatch.Domain.Providers;
using PabloDispatch.Domain.Services;
#pragma warning disable SA1401

namespace PabloDispatch.Configuration;

public class PabloDispatchComponent : IPabloDispatchComponent
{
    #region Services

    internal enum ServicesEnum
    {
        PabloDispatcher,
    }

    internal readonly Dictionary<ServicesEnum, ServiceDescriptor> Services = new()
    {
        [ServicesEnum.PabloDispatcher] = ServiceDescriptor.Transient<IPabloDispatcher, PabloDispatcher>(),
    };

    public IPabloDispatchComponent SetPabloDispatcher<T>()
        where T : class, IPabloDispatcher
    {
        Services[ServicesEnum.PabloDispatcher] = ServiceDescriptor.Transient<IPabloDispatcher, T>();
        return this;
    }

    #endregion

    #region Handlers

    /// <summary>
    /// Dictionary of added request handlers, with request request and result type tuple as key.
    /// </summary>
    internal readonly Dictionary<Type, ServiceDescriptor> RequestHandlers = new();

    /// <summary>
    /// List og added request pipeline handlers.
    /// </summary>
    internal readonly List<ServiceDescriptor> RequestPipelineHandlers = new();

    /// <summary>
    /// Dictionary of request pipeline providers, with request type as key.
    /// </summary>
    internal readonly Dictionary<Type, ServiceDescriptor> RequestPipelineProviders = new();

    public IPabloDispatchComponent SetRequestHandler<TRequest, TResult, TRequestHandler>(Action<IRequestPipeline<TRequest, TResult>>? pipelineConfig = null)
        where TRequest : IRequest<TResult>
        where TRequestHandler : class, IRequestHandler<TRequest, TResult>
    {
        RequestHandlers[typeof(Tuple<TRequest, TResult>)] = ServiceDescriptor.Transient<IRequestHandler<TRequest, TResult>, TRequestHandler>();

        // Set pipeline if configurator is not null
        if (pipelineConfig != null)
        {
            // Configure pipeline
            var pipeline = new ReturnRequestPipeline<TRequest, TResult>();
            pipelineConfig.Invoke(pipeline);
            var pipelineProvider = new ReturnRequestPipelineProvider<TRequest, TResult>(
                pipeline.GetPreProcessors(),
                pipeline.GetPostProcessors());

            // Register pipeline handlers
            RequestPipelineHandlers.AddRange(pipeline.GetPreProcessors().Select(x => ServiceDescriptor.Transient(x, x)));
            RequestPipelineHandlers.AddRange(pipeline.GetPostProcessors().Select(x => ServiceDescriptor.Transient(x, x)));

            // Register pipeline provider
            RequestPipelineProviders[typeof(TRequest)] = ServiceDescriptor.Singleton<IRequestPipelineProvider<TRequest, TResult>>(pipelineProvider);

            return this;
        }

        return this;
    }

    public IPabloDispatchComponent SetRequestHandler<TRequest, TRequestHandler>(Action<IRequestPipeline<TRequest>>? pipelineConfig = null)
        where TRequest : IRequest
        where TRequestHandler : class, IRequestHandler<TRequest>
    {
        RequestHandlers[typeof(Tuple<TRequest>)] = ServiceDescriptor.Transient<IRequestHandler<TRequest>, TRequestHandler>();

        // Set pipeline if configurator is not null
        if (pipelineConfig != null)
        {
            // Configure pipeline
            var pipeline = new VoidRequestPipeline<TRequest>();
            pipelineConfig.Invoke(pipeline);
            var pipelineProvider = new VoidRequestPipelineProvider<TRequest>(
                pipeline.GetPreProcessors(),
                pipeline.GetPostProcessors());

            // Register pipeline handlers
            RequestPipelineHandlers.AddRange(pipeline.GetPreProcessors().Select(x => ServiceDescriptor.Transient(x, x)));
            RequestPipelineHandlers.AddRange(pipeline.GetPostProcessors().Select(x => ServiceDescriptor.Transient(x, x)));

            // Register pipeline provider
            RequestPipelineProviders[typeof(TRequest)] = ServiceDescriptor.Singleton<IRequestPipelineProvider<TRequest>>(pipelineProvider);

            return this;
        }

        return this;
    }

    #endregion
}