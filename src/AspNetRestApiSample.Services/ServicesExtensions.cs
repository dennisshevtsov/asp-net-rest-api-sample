// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AspNetRestApiSample.Services;

  /// <summary>Provides a simple API to register application services.</summary>
  public static class ServicesExtensions
  {
    /// <summary>Registers application services.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      services.AddScoped<ITodoListService, TodoListService>();
      services.AddScoped<ITodoListTaskService, TodoListTaskService>();

      return services;
    }
  }
}
