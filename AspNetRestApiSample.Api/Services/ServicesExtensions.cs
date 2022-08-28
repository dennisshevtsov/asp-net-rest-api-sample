// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  /// <summary>Provides a simple API to register application services.</summary>
  public static class ServicesExtensions
  {
    /// <summary>Registers application servces.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that represents a set of key/value application configuration properties.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      services.AddScoped<ITodoListService, TodoListService>();
      services.AddScoped<ITodoListTaskService, TodoListTaskService>();

      return services;
    }
  }
}
