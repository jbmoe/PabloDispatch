using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Services;
using PabloDispatch.Configuration;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.RequestHandlers;
using PabloDispatch.Tests.Mock.RequestPipelineHandlers;
using PabloDispatch.Tests.Mock.Requests;
using Xunit;

namespace PabloDispatch.Tests.Domain.Services;

public class PabloDispatcherPipelineTests
{
    public class PabloDispatcherPipelineTestFixture : Fixture
    {
        public IDispatcher Dispatcher { get; set; }

        public PabloDispatcherPipelineTestFixture(Action<IPabloDispatchComponent>? componentConfig = null)
        {
            Customize(new AutoNSubstituteCustomization());
            var services = new ServiceCollection();
            services.AddPabloDispatch(componentConfig);
            var serviceProvider = services.BuildServiceProvider();
            Dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        }
    }

    [Fact]
    public async Task Dispatcher_CommandPipelineHandler_Found()
    {
        var fixture = new PabloDispatcherPipelineTestFixture(component =>
        {
            component.SetCommandHandler<MockCommand, MockCommandHandler>(pipeline =>
            {
                pipeline.AddPostProcessor<MockACommandPipelineHandler>();
            });
        });

        var callStack = new Queue<string>();

        var command = new MockCommand(code => callStack.Enqueue(code));

        await fixture.Dispatcher.DispatchAsync(command);

        Assert.Contains(MockACommandPipelineHandler.Code, callStack);
    }

    [Fact]
    public async Task Dispatcher_QueryPipelineHandler_Found()
    {
        var fixture = new PabloDispatcherPipelineTestFixture(component =>
        {
            component.SetQueryHandler<MockQuery, MockModelA, MockAQueryHandler>(pipeline =>
            {
                pipeline.AddPostProcessor<MockAQueryPipelineHandler>();
            });
        });

        var callStack = new Queue<string>();

        var query = new MockQuery(code => callStack.Enqueue(code));

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModelA>(query);

        Assert.Contains(MockAQueryPipelineHandler.Code, callStack);
    }

    [Fact]
    public async Task Dispatcher_CommandPipelineHandler_Called_In_Order()
    {
        var fixture = new PabloDispatcherPipelineTestFixture(component =>
        {
            component.SetCommandHandler<MockCommand, MockCommandHandler>(pipeline =>
            {
                pipeline
                    .AddPreProcessor<MockACommandPipelineHandler>()
                    .AddPostProcessor<MockBCommandPipelineHandler>();
            });
        });

        var callStack = new Queue<string>();

        var command = new MockCommand(code => callStack.Enqueue(code));

        await fixture.Dispatcher.DispatchAsync(command);

        Assert.Equal(MockACommandPipelineHandler.Code, callStack.Dequeue());
        Assert.Equal(MockCommandHandler.Code, callStack.Dequeue());
        Assert.Equal(MockBCommandPipelineHandler.Code, callStack.Dequeue());
    }

    [Fact]
    public async Task Dispatcher_QueryPipelineHandler_Called_In_Order()
    {
        var fixture = new PabloDispatcherPipelineTestFixture(component =>
        {
            component.SetQueryHandler<MockQuery, MockModelA, MockAQueryHandler>(pipeline =>
            {
                pipeline
                    .AddPreProcessor<MockAQueryPipelineHandler>()
                    .AddPostProcessor<MockBQueryPipelineHandler>();
            });
        });

        var callStack = new Queue<string>();

        var query = new MockQuery(code => callStack.Enqueue(code));

        await fixture.Dispatcher.DispatchAsync<MockQuery, MockModelA>(query);

        Assert.Equal(MockAQueryPipelineHandler.Code, callStack.Dequeue());
        Assert.Equal(MockAQueryHandler.Code, callStack.Dequeue());
        Assert.Equal(MockBQueryPipelineHandler.Code, callStack.Dequeue());
    }
}