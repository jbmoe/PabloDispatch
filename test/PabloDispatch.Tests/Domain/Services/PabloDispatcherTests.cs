using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;
using PabloCache.DistributedMemoryCache.Configuration;
using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Options;
using PabloDispatch.Api.Services;
using PabloDispatch.Configuration;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.RequestHandlers;
using PabloDispatch.Tests.Mock.RequestPipelineHandlers;
using PabloDispatch.Tests.Mock.Requests;
using Xunit;

namespace PabloDispatch.Tests.Domain.Services;

public class PabloDispatcherTests
{
    public class PabloDispatcherTestFixture : Fixture
    {
        public IDispatcher Dispatcher { get; set; }

        public PabloDispatcherTestFixture(Action<IPabloDispatchComponent, IServiceCollection>? componentConfig = null)
        {
            Customize(new AutoNSubstituteCustomization());
            var services = new ServiceCollection();
            services.AddPabloDispatch(component => componentConfig?.Invoke(component, services));
            var serviceProvider = services.BuildServiceProvider();
            Dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        }
    }

    [Fact]
    public async Task Dispatcher_CommandHandler_NotFound_Throws()
    {
        var fixture = new PabloDispatcherTestFixture();

        var request = new MockCommand();

        await Assert.ThrowsAsync<CommandHandlerNotFoundException>(
            () => fixture.Dispatcher.DispatchAsync(request));
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_NotFound_Throws()
    {
        var fixture = new PabloDispatcherTestFixture();

        var request = new MockQuery();

        await Assert.ThrowsAsync<QueryHandlerNotFoundException>(
            () => fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(request));
    }

    [Fact]
    public async Task Dispatcher_CommandHandler_Found_Is_Called()
    {
        var fixture = new PabloDispatcherTestFixture((component, _) =>
        {
            component.SetCommandHandler<MockCommand, MockCommandHandler>();
        });

        var isInvoked = false;

        var command = new MockCommand(_ => isInvoked = true);

        await fixture.Dispatcher.DispatchAsync(command);

        Assert.True(isInvoked);
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_Found_Is_Called()
    {
        var fixture = new PabloDispatcherTestFixture((component, _) =>
        {
            component.SetQueryHandler<MockQuery, MockModel, MockQueryHandler>();
        });

        var isInvoked = false;

        var query = new MockQuery(_ => isInvoked = true);

        var result = await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);

        Assert.NotNull(result);
        Assert.IsType<MockModel>(result);
        Assert.True(isInvoked);
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_Is_Cached()
    {
        var fixture = new PabloDispatcherTestFixture((component, services) =>
        {
            component
                .ConfigurePabloCache(cacheComponent =>
                {
                    cacheComponent.UseDistributedMemoryCache(services);
                })
                .SetQueryHandler<MockQuery, MockModel, MockQueryHandler>(pipelineConfig =>
                {
                    pipelineConfig.SetCacheOptions(new CacheOptions<MockQuery>
                    {
                        CacheKeyFactory = query => $"{query}",
                        EnableCache = true,
                        TtlMinutes = 5,
                    });
                });
        });

        var invokedCount = 0;

        var query = new MockQuery(_ => invokedCount++);

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);
        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);

        Assert.Equal(1, invokedCount);
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_Is_Cached_Post_Processors_Are_Cached()
    {
        var fixture = new PabloDispatcherTestFixture((component, services) =>
        {
            component
                .ConfigurePabloCache(cacheComponent =>
                {
                    cacheComponent.UseDistributedMemoryCache(services);
                })
                .SetQueryHandler<MockQuery, MockModel, MockQueryHandler>(pipelineConfig =>
                {
                    pipelineConfig
                        .SetCacheOptions(new CacheOptions<MockQuery>
                        {
                            CacheKeyFactory = query => $"{query}",
                            EnableCache = true,
                            TtlMinutes = 5,
                            CachedPipelines = CachedPipelines.PostProcessors,
                        })
                        .AddPreProcessor<MockAQueryPipelineHandler>()
                        .AddPostProcessor<MockBQueryPipelineHandler>();
                });
        });

        var invokedCount = 0;

        var query = new MockQuery(_ => invokedCount++);

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);
        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);

        Assert.Equal(4, invokedCount);
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_Is_Cached_Pre_Processors_Are_Cached()
    {
        var fixture = new PabloDispatcherTestFixture((component, services) =>
        {
            component
                .ConfigurePabloCache(cacheComponent =>
                {
                    cacheComponent.UseDistributedMemoryCache(services);
                })
                .SetQueryHandler<MockQuery, MockModel, MockQueryHandler>(pipelineConfig =>
                {
                    pipelineConfig
                        .SetCacheOptions(new CacheOptions<MockQuery>
                        {
                            CacheKeyFactory = query => $"{query}",
                            EnableCache = true,
                            TtlMinutes = 5,
                            CachedPipelines = CachedPipelines.PreProcessors,
                        })
                        .AddPreProcessor<MockAQueryPipelineHandler>()
                        .AddPostProcessor<MockBQueryPipelineHandler>();
                });
        });

        var invokedCount = 0;

        var query = new MockQuery(_ => invokedCount++);

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);
        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);

        Assert.Equal(4, invokedCount);
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_Is_Cached_No_Processors_Are_Cached()
    {
        var fixture = new PabloDispatcherTestFixture((component, services) =>
        {
            component
                .ConfigurePabloCache(cacheComponent =>
                {
                    cacheComponent.UseDistributedMemoryCache(services);
                })
                .SetQueryHandler<MockQuery, MockModel, MockQueryHandler>(pipelineConfig =>
                {
                    pipelineConfig
                        .SetCacheOptions(new CacheOptions<MockQuery>
                        {
                            CacheKeyFactory = query => $"{query}",
                            EnableCache = true,
                            TtlMinutes = 5,
                            CachedPipelines = CachedPipelines.None,
                        })
                        .AddPreProcessor<MockAQueryPipelineHandler>()
                        .AddPostProcessor<MockBQueryPipelineHandler>();
                });
        });

        var invokedCount = 0;

        var query = new MockQuery(_ => invokedCount++);

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);
        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);

        Assert.Equal(5, invokedCount);
    }

    [Fact]
    public async Task Dispatcher_QueryHandler_Is_Cached_All_Processors_Are_Cached()
    {
        var fixture = new PabloDispatcherTestFixture((component, services) =>
        {
            component
                .ConfigurePabloCache(cacheComponent =>
                {
                    cacheComponent.UseDistributedMemoryCache(services);
                })
                .SetQueryHandler<MockQuery, MockModel, MockQueryHandler>(pipelineConfig =>
                {
                    pipelineConfig
                        .SetCacheOptions(new CacheOptions<MockQuery>
                        {
                            CacheKeyFactory = query => $"{query}",
                            EnableCache = true,
                            TtlMinutes = 5,
                            CachedPipelines = CachedPipelines.All,
                        })
                        .AddPreProcessor<MockAQueryPipelineHandler>()
                        .AddPostProcessor<MockBQueryPipelineHandler>();
                });
        });

        var invokedCount = 0;

        var query = new MockQuery(_ => invokedCount++);

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);
        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModel>(query);

        Assert.Equal(3, invokedCount);
    }
}