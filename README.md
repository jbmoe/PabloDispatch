# PabloDispatch Library Readme

## Introduction

PabloDispatch is a C# library designed to simplify and implement the Command Query Responsibility Segregation (CQRS) pattern in your application. CQRS is a design pattern that separates the read and write operations, treating them as distinct responsibilities.

> **Note:** Parts of this readme file was written with the help of ChatGPT, an AI language model developed by OpenAI.

## How PabloDispatch Works

PabloDispatch provides a set of interfaces and components that help you effectively organize your application using CQRS principles. Here's an overview of key components:

1. **Requests and Handlers**: The library includes marker interfaces `IRequest` and `IRequest<TResponse>` representing requests without and with a return type, respectively. It also defines `IRequestHandler<TRequest, TResponse>` and `IRequestHandler<TRequest>` interfaces to define handlers for processing these requests.

2. **Pipeline Handlers**: PabloDispatch provides `IRequestPipelineHandler<TRequest>` and `IRequestPipelineHandler<TRequest, TResult>` interfaces for pre- and post-processing of requests. These pipeline handlers allow you to add additional behavior before and after the main request is processed.

3. **Dispatcher**: The `IPabloDispatcher` interface represents a request dispatcher. It is responsible for identifying the appropriate handler for a given request and dispatching it for processing.

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
        component.SetRequestHandler<YourRequestType, YourRequestHandlerType>()
    });
}
```

## Note on Alpha Version

Please be aware that PabloDispatch is currently in the alpha stage, which means it's still undergoing early testing and development. As a result, changes may occur in subsequent releases as I refine and improve the library.