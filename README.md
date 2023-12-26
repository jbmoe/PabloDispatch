# PabloDispatch

## Introduction

PabloDispatch is a C# library designed to simplify and implement the Command Query Responsibility Segregation (CQRS) pattern in your application. CQRS is a design pattern that separates the read and write operations, treating them as distinct responsibilities.

## Table of content
- [How PabloDispatch Works](#how-pablodispatch-works)
- [How to Use PabloDispatch](#how-to-use-pablodispatch)
    - [Registration](#registration)
    - [Usage example](#usage-example)
    - [Caching query dispatching](#caching-query-dispatching)
- [Note on Alpha Version](#note-on-alpha-version)

## How PabloDispatch Works

PabloDispatch provides a set of interfaces and components that help you effectively organize your application using CQRS principles. Here's an overview of key components:

1. **Commands/Queries and Handlers**: The library includes marker interfaces `IQuery` and `ICommand` representing requests with and without a return type, respectively.
It also defines `IQueryHandler<TQuery, TResponse>` and `ICommandHandler<TCommand>` interfaces to define handlers for processing these requests.

2. **Pipeline Handlers**: PabloDispatch provides `ICommandPipelineHandler<TCommand>` and `IQueryPipelineHandler<TQuery>` interfaces for pre- and post-processing of requests. These pipeline handlers allow you to add additional behavior before and after the main request is processed.

3. **Dispatcher**: The `IDispatcher` interface represents a command and query dispatcher. It is responsible for identifying the appropriate handler for a given request and dispatching it for processing.

## How to Use PabloDispatch

### Registration

To start using the PabloDispatch library, you need to register its components in your application's dependency injection container. Here's how you can do it:

```csharp
// In your startup or composition root class:
public void ConfigureServices(IServiceCollection services)
{
    // Register the PabloDispatch components using the extension method.
    services.AddPabloDispatch(component =>
    {
        // Registering a command handler:
        component.SetCommandHandler<YourCommandType, YourCommandHandlerType>();

        // Registering a query handler:
        component.SetQueryHandler<YourQueryType, YourReturnType, YourQueryHandlerType>();

        // Registering a command handler with pre- and post-processing:
        component.SetCommandHandler<YourCommandType, YourCommandHandlerType>(pipeline =>
        {
            pipeline
                .AddPreProcessor<YourCommandPipelineHandlerType>()
                .AddPostProcessor<YourCommandPipelineHandlerType>();
        });
    });
}
```


### Usage example

Here is a simple example of how to implement an `IQueryHandler` to fetch a customer by email

- First, implement a query of `IQuery`

```csharp
public class FetchCustomerByEmailQuery : IQuery
{
    public string Email { get; set; }

    public FetchCustomerByEmailQuery(string email)
    {
        Email = email;
    }
}
```

- Then implement the `IQueryHandler<FetchCustomerByEmailQuery, Customer>` query handler. This is where we want to centralize the logic.

```csharp
public class FetchCustomerByEmailQueryHandler : IQueryHandler<FetchCustomerByEmailQuery, Customer>
{
    private readonly ICustomerRepository _customerRepository;

    public FetchCustomerByEmailQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> HandleAsync(FetchCustomerByEmailQuery query, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.FirstOrDefault(x => x.Email == query.Email);

        return customer;
    }
}
```

- Lastly, the `IDispatcher` interface can be used to fire the `FetchCustomerByEmailQuery` and the `Customer` will be returned.

```csharp
public class SomeClass
{
    private readonly IDispatcher _dispatcher;

    public SomeClass(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task SomeMethod(string customerEmail)
    {
        var query = new FetchCustomerByEmailQuery(customerEmail);

        var customer = await _dispatcher.DispatchAsync<FetchCustomerByEmailQuery, Customer>(query);

        // Rest of method
    }
}
```

Remember to set the query and the query handler as specified [here](#registration)

### Caching query dispatching

PabloDispatch provides a robust caching mechanism to optimize query dispatching performance using PabloCache. This section guides you through configuring during startup and using caching with PabloDispatch.

For more documentation on PabloCache [click here](https://github.com/jbmoe/PabloCache.Abstractions).

#### Configuring Caching during Startup

- **Initialize PabloDispatch Component:**   
  In your application's startup configuration, use the `AddPabloDispatch` method to initialize the `IPabloDispatchComponent`, like described in the [Registration](#registration) section.

- **Configure Caching with PabloCache:**
  Inside the AddPabloDispatch method, use the ConfigurePabloCache method to set up caching with PabloCache. This method takes a configurator action:
  ````csharp
  // In your startup or composition root class:
  public void ConfigureServices(IServiceCollection services)
  {
      // Register the PabloDispatch components using the extension method.
      services.AddPabloDispatch(component =>
      {
          component.ConfigurePabloCache(cacheComponent =>
          {
              // Configure caching options using cacheComponent.Use... methods
              cacheComponent.UseDistributedMemoryCache(services);
          });
      });
  }
  ````
  In this example, UseDistributedMemoryCache is used, but you can choose any PabloCache implementation that suits your application.

- **Set Up Query and Dispatcher:**
  Set up your query, query handler, and dispatcher as described in the [Registration](#registration) section. 

- **Set Caching Options for Query:**  
  Apply caching options for the specific query using the SetCacheOptions method in the query pipeline configuration:
  ```csharp
  // In your startup or composition root class:
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddPabloDispatch(component =>
      {
          // Registering a command handler with pre- and post-processing:
          component
              .ConfigurePabloCache(cacheComponent =>
              {
                  // Configure caching options using cacheComponent.Use... methods
                  cacheComponent.UseDistributedMemoryCache(services);
              })
              .SetQueryHandler<YourQueryType, YourReturnType, YourQueryHandlerType>(pipeline =>
              {
                  pipeline
                      .SetCacheOptions(new CacheOptions<YourQueryType>
                      {
                          CacheKeyFactory = query => $"YourQueryType_{query.Property}",
                          EnableCache = true,
                          TtlMinutes = 5,
                          CachedPipelines = CachedPipelines.All,
                      })
                      .AddPreProcessor<YourQueryPipelineHandlerType>()
                      .AddPostProcessor<YourQueryPipelineHandlerType>();
              });
      });
  }
  ```
- **CachedPipelines:**
  The CachedPipelines logic allows you to specify which pipelines (pre-processors and post-processors) should be cached. In the above example, CachedPipelines.All indicates that both pre and post-processors should be included in the cached result. Adjust the CachedPipelines value according to your application's needs.

#### Using Caching

Once configured during startup, caching is automatically applied when dispatching queries. Simply use the `IDispatcher` to dispatch your queries.

## Note on Alpha Version

Please be aware that PabloDispatch is currently in the alpha stage, which means it's still undergoing early testing and development. As a result, changes may occur in subsequent releases as the library is refined and improved.