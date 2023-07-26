using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Services;
using PabloDispatch.Configuration;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.RequestHandlers;
using PabloDispatch.Tests.Mock.Requests;
using Xunit;

namespace PabloDispatch.Tests.Domain.Services;

public class PabloDispatcherTests
{
    public class PabloDispatcherTestFixture : Fixture
    {
        public IDispatcher Dispatcher { get; set; }

        public PabloDispatcherTestFixture(Action<IPabloDispatchComponent>? componentConfig = null)
        {
            Customize(new AutoNSubstituteCustomization());
            var services = new ServiceCollection();
            services.AddPabloDispatch(componentConfig);
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
        var fixture = new PabloDispatcherTestFixture(component =>
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
        var fixture = new PabloDispatcherTestFixture(component =>
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
}