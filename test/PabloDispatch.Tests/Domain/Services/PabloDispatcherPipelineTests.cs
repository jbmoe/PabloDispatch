using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Services;
using PabloDispatch.Configuration;
using Xunit;

namespace PabloDispatch.Tests.Domain.Services;

public class PabloDispatcherPipelineTests
{
    public class PabloDispatcherPipelineTestFixture : Fixture
    {
        public IPabloDispatcher PabloDispatcher { get; set; }

        public PabloDispatcherPipelineTestFixture(Action<IPabloDispatchComponent>? componentConfig = null)
        {
            Customize(new AutoNSubstituteCustomization());
            var services = new ServiceCollection();
            services.AddPabloDispatch(componentConfig);
            var serviceProvider = services.BuildServiceProvider();
            PabloDispatcher = serviceProvider.GetRequiredService<IPabloDispatcher>();
        }
    }

    [Fact]
    public void Dispatcher_RequestPipelineHandler_Found_With_Void()
    {
    }

    [Fact]
    public void Dispatcher_RequestPipelineHandler_Found_With_Return()
    {
    }

    [Fact]
    public void Dispatcher_RequestPipelineHandler_With_Void_Called_In_Order()
    {
    }

    [Fact]
    public void Dispatcher_RequestPipelineHandler_With_Return_Called_In_Order()
    {
    }
}