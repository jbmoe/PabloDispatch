# Changelog

All notable changes to the PabloDispatch library will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0-alpha] - 2023-12-26

### Added

- Caching for query dispatching.
- Usage example to README, including detailed instructions on configuring and using caching for query dispatching.

### Changed

- Change type constraint for `TResult` in `IDispatcher` and `IQueryHandler` interfaces in order to implement caching of query results.
- Changed type parameter TQuery of `IQueryPipeline` to be non-covariant
## [1.1.0-alpha] - 2023-07-26

### Added
- Usage example to README

### Changed
- Seperated requests with and without return values to their own interfaces: `IQuery` and `ICommand`
- Changed and seperated the below interfaces to match the above change
	- `IRequestHandler<TRequest, TResponse>`
	- `IRequestHandler<TRequest>`
	- `IRequestPipelineHandler<TRequest, TResult>`
	- `IRequestPipelineHandler<TRequest, TResult>`
- Renamed `IPabloDispatcher` to `IDispatcher`
- Changed README to accuratly describe the library according to the above changes

## [0.1.0-alpha] - 2023-07-18

### Added

- Initial release of the PabloDispatch library.
- Implemented marker interfaces for requests: `IRequest` and `IRequest<TResponse>`.
- Added interfaces for request handlers: `IRequestHandler<TRequest, TResponse>` and `IRequestHandler<TRequest>`.
- Included pipeline handler interfaces: `IRequestPipelineHandler<TRequest>` and `IRequestPipelineHandler<TRequest, TResult>`.
- Introduced the `IPabloDispatcher` interface for request dispatching.
- Included the `IPabloDispatchComponent` interface for configuration and registration.
- Added the `AddPabloDispatch` extension method for IServiceCollection to register the library components.

### Usage Instructions

- Documented the process for registering `IRequest` and `IRequestHandler` in the readme.
- Provided a simple example use case demonstrating how to use PabloDispatch in the readme.


## Note on Alpha Version

Please be aware that PabloDispatch is currently in the alpha stage, which means it's still undergoing early testing and development. As a result, changes may occur in subsequent releases as the library is refined and improved.