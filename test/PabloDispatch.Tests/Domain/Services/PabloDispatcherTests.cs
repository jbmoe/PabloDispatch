using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Services;
using PabloDispatch.Configuration;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;
using Xunit;

namespace PabloDispatch.Tests.Domain.Services;

public class PabloDispatcherTests
{
    public class PabloDispatcherTestFixture : Fixture
    {
        public IPabloDispatcher PabloDispatcher { get; set; }

        public PabloDispatcherTestFixture(Action<IPabloDispatchComponent>? componentConfig = null)
        {
            Customize(new AutoNSubstituteCustomization());
            var services = new ServiceCollection();
            services.AddPabloDispatch(componentConfig);
            var serviceProvider = services.BuildServiceProvider();
            PabloDispatcher = serviceProvider.GetRequiredService<IPabloDispatcher>();
        }
    }

    [Fact]
    public async Task Dispatcher_RequestHandler_NotFound_With_Void()
    {
        var fixture = new PabloDispatcherTestFixture();

        var request = new NullVoidRequest();

        await Assert.ThrowsAsync<RequestHandlerNotFoundException>(() => fixture.PabloDispatcher.DispatchAsync(request));
    }

    [Fact]
    public async Task Dispatcher_RequestHandler_NotFound_With_Return()
    {
        var fixture = new PabloDispatcherTestFixture();

        var request = new NullReturnRequest();

        await Assert.ThrowsAsync<RequestHandlerNotFoundException>(
            () => fixture.PabloDispatcher.DispatchAsync<NullReturnRequest, NullReturnRequestReturnModel>(request));
    }

    [Fact]
    public void Dispatcher_RequestHandler_Found_With_Void()
    {
    }

    [Fact]
    public void Dispatcher_RequestHandler_Found_With_Return()
    {
    }
}