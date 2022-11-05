// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>Provides a simple API to register controllers.</summary>
  public static class SwaggerExtensions
  {
    /// <summary>Registers configured swagger services.</summary>
    /// <param name="services"></param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddCofiguredSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(options =>
      {
        options.DescribeAllParametersInCamelCase();
        options.UseOneOfForPolymorphism();
      });

      return services;
    }
  }
}
