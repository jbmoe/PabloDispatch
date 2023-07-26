using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Queries;
using PabloDispatch.Api.Services;
using PabloDispatch.Configuration;
using PabloDispatch.Domain.Services;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.RequestHandlers;
using PabloDispatch.Tests.Mock.RequestPipelineHandlers;
using PabloDispatch.Tests.Mock.Requests;
using PabloDispatch.Tests.Mock.Services;
using Xunit;

namespace PabloDispatch.Tests.Configuration;

public class ComponentTests
{
    public class ComponentTestFixture : Fixture
    {
        public IServiceProvider ServiceProvider { get; set; }

        public ComponentTestFixture(Action<IPabloDispatchComponent>? componentConfig = null)
        {
            var services = new ServiceCollection();
            services.AddPabloDispatch(componentConfig);
            ServiceProvider = services.BuildServiceProvider();
        }
    }

    [Fact]
    public void Component_Configuration_Is_Invoked()
    {
        var invoked = false;

        new ComponentTestFixture(_ =>
        {
            invoked = true;
        });

        Assert.True(invoked);
    }

    [Fact]
    public void Component_Default_Implementations_Are_Registered()
    {
        var fixture = new ComponentTestFixture();

        var serviceProvider = fixture.ServiceProvider;

        var pabloDispatcher = serviceProvider.GetService<IPabloDispatcher>();

        Assert.NotNull(pabloDispatcher);
        Assert.IsType<PabloDispatcher>(pabloDispatcher);
    }

    [Fact]
    public void Component_Set_PabloDispatcher()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetPabloDispatcher<NullPabloDispatcher>();
        });

        var serviceProvider = fixture.ServiceProvider;

        var pabloDispatcher = serviceProvider.GetService<IPabloDispatcher>();

        Assert.NotNull(pabloDispatcher);
        Assert.IsType<NullPabloDispatcher>(pabloDispatcher);
    }

    [Fact]
    public void Component_Set_CommandHandler()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetCommandHandler<MockCommand, MockCommandHandler>();
        });

        var serviceProvider = fixture.ServiceProvider;

        var requestHandler = serviceProvider.GetService<ICommandHandler<MockCommand>>();

        Assert.NotNull(requestHandler);
        Assert.IsType<MockCommandHandler>(requestHandler);
    }

    [Fact]
    public void Component_Set_QueryHandler()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetQueryHandler<MockQuery, MockModel, MockQueryHandler>();
        });

        var serviceProvider = fixture.ServiceProvider;

        var requestHandler = serviceProvider.GetService<IQueryHandler<MockQuery, MockModel>>();

        Assert.NotNull(requestHandler);
        Assert.IsType<MockQueryHandler>(requestHandler);
    }

    [Fact]
    public void Component_Set_Command_PipelineHandler()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetCommandHandler<MockCommand, MockCommandHandler>(pipeline =>
            {
                pipeline
                    .AddPreProcessor<MockACommandPipelineHandler>()
                    .AddPostProcessor<MockBCommandPipelineHandler>();
            });
        });

        var serviceProvider = fixture.ServiceProvider;

        var firstPipelineHandler = serviceProvider.GetService(typeof(MockACommandPipelineHandler)) as ICommandPipelineHandler<MockCommand>;
        var secondPipelineHandler = serviceProvider.GetService(typeof(MockBCommandPipelineHandler)) as ICommandPipelineHandler<MockCommand>;

        Assert.NotNull(firstPipelineHandler);
        Assert.NotNull(secondPipelineHandler);
        Assert.IsType<MockACommandPipelineHandler>(firstPipelineHandler);
        Assert.IsType<MockBCommandPipelineHandler>(secondPipelineHandler);
    }

    [Fact]
    public void Component_Set_Query_PipelineHandler()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetQueryHandler<MockQuery, MockModel, MockQueryHandler>(pipeline =>
            {
                pipeline
                    .AddPreProcessor<MockAQueryPipelineHandler>()
                    .AddPostProcessor<MockBQueryPipelineHandler>();
            });
        });

        var serviceProvider = fixture.ServiceProvider;

        var firstPipelineHandler = serviceProvider.GetService(typeof(MockAQueryPipelineHandler)) as IQueryPipelineHandler<MockQuery, MockModel>;
        var secondPipelineHandler = serviceProvider.GetService(typeof(MockBQueryPipelineHandler)) as IQueryPipelineHandler<MockQuery, MockModel>;

        Assert.NotNull(firstPipelineHandler);
        Assert.NotNull(secondPipelineHandler);
        Assert.IsType<MockAQueryPipelineHandler>(firstPipelineHandler);
        Assert.IsType<MockBQueryPipelineHandler>(secondPipelineHandler);
    }
}