// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AspNetRestApiSample.WebApi.Binding;

  /// <summary>Provides a simple API to register controllers.</summary>
  public static class ControllerExtensions
  {
    /// <summary>Registers configured controllers.</summary>
    /// <param name="services"></param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddCofiguredControllers(this IServiceCollection services)
    {
      services.AddControllers(options => options.ModelBinderProviders.Insert(0, new RequestDtoBinderProvider(options)))
              .AddJsonSerialization();

      return services;
    }
  }
}
