using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Requests;
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
    public void Component_Set_RequestHandler_With_Void()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetRequestHandler<NullVoidRequest, NullVoidRequestHandler>();
        });

        var serviceProvider = fixture.ServiceProvider;

        var requestHandler = serviceProvider.GetService<IRequestHandler<NullVoidRequest>>();

        Assert.NotNull(requestHandler);
        Assert.IsType<NullVoidRequestHandler>(requestHandler);
    }

    [Fact]
    public void Component_Set_RequestHandler_With_Return()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetRequestHandler<NullReturnRequest, NullReturnRequestReturnModel, NullReturnRequestHandler>();
        });

        var serviceProvider = fixture.ServiceProvider;

        var requestHandler = serviceProvider.GetService<IRequestHandler<NullReturnRequest, NullReturnRequestReturnModel>>();

        Assert.NotNull(requestHandler);
        Assert.IsType<NullReturnRequestHandler>(requestHandler);
    }

    [Fact]
    public void Component_Set_PipelineHandler_With_Void()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetRequestHandler<NullVoidRequest, NullVoidRequestHandler>(pipeline =>
            {
                pipeline
                    .AddPreProcessor<NullVoidRequestFirstPipelineHandler>()
                    .AddPostProcessor<NullVoidRequestSecondPipelineHandler>();
            });
        });

        var serviceProvider = fixture.ServiceProvider;

        var firstPipelineHandler = serviceProvider.GetService(typeof(NullVoidRequestFirstPipelineHandler)) as IRequestPipelineHandler<NullVoidRequest>;
        var secondPipelineHandler = serviceProvider.GetService(typeof(NullVoidRequestSecondPipelineHandler)) as IRequestPipelineHandler<NullVoidRequest>;

        Assert.NotNull(firstPipelineHandler);
        Assert.NotNull(secondPipelineHandler);
        Assert.IsType<NullVoidRequestFirstPipelineHandler>(firstPipelineHandler);
        Assert.IsType<NullVoidRequestSecondPipelineHandler>(secondPipelineHandler);
    }

    [Fact]
    public void Component_Set_PipelineHandler_With_Return()
    {
        var fixture = new ComponentTestFixture(component =>
        {
            component.SetRequestHandler<NullReturnRequest, NullReturnRequestReturnModel, NullReturnRequestHandler>(pipeline =>
            {
                pipeline
                    .AddPreProcessor<NullReturnRequestFirstPipelineHandler>()
                    .AddPostProcessor<NullReturnRequestSecondPipelineHandler>();
            });
        });

        var serviceProvider = fixture.ServiceProvider;

        var firstPipelineHandler = serviceProvider.GetService(typeof(NullReturnRequestFirstPipelineHandler)) as IRequestPipelineHandler<NullReturnRequest, NullReturnRequestReturnModel>;
        var secondPipelineHandler = serviceProvider.GetService(typeof(NullReturnRequestSecondPipelineHandler)) as IRequestPipelineHandler<NullReturnRequest, NullReturnRequestReturnModel>;

        Assert.NotNull(firstPipelineHandler);
        Assert.NotNull(secondPipelineHandler);
        Assert.IsType<NullReturnRequestFirstPipelineHandler>(firstPipelineHandler);
        Assert.IsType<NullReturnRequestSecondPipelineHandler>(secondPipelineHandler);
    }
}