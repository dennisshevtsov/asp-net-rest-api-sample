// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>Provides a simple API to register CORS policies.</summary>
  public static class CorsExtensions
  {
    /// <summary>Registers a default CORS policy.</summary>
    /// <param name="services"></param>
    /// <param name="configuration">An object that represents a set of key/value application configuration properties.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddCofiguredCors(
      this IServiceCollection services, IConfiguration configuration)
    {
      var settings = configuration.Get<CorsSettings>();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(policy =>
          policy.WithOrigins(settings.OriginCollection)
                .WithMethods(settings.MethodCollection)
                .WithHeaders(settings.HeaderCollection));
      });

      return services;
    }

    /// <summary>Represents CORS settings.</summary>
    public sealed class CorsSettings
    {
      /// <summary>Gets/sets an object that represents a string list of allowed origings.</summary>
      public string? Origins { get; set; }

      /// <summary>Gets an object that represents a collection of allowed origins.</summary>
      public string[] OriginCollection => CorsSettings.ToCollection(Origins);

      /// <summary>Gets/sets an object that represents a string list of allowed HTTP methods.</summary>
      public string? Methods { get; set; }

      /// <summary>Gets an object that represents a collection of allowed HTTP methods.</summary>
      public string[] MethodCollection => CorsSettings.ToCollection(Methods);

      /// <summary>Gets/sets an object that represents a string list of allowed HTTP request headers.</summary>
      public string? Headers { get; set; }

      /// <summary>Gets an object that represents a collection of allowed HTTP request headers.</summary>
      public string[] HeaderCollection => CorsSettings.ToCollection(Headers);

      /// <summary>Convers a string list to a collection.</summary>
      /// <param name="value">An object that represents a string list.</param>
      /// <returns>An object that represents a collection.</returns>
      public static string[] ToCollection(string? value)
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          return new string[0];
        }

        return value.Split(new[] { '\u002C' }, StringSplitOptions.RemoveEmptyEntries);
      }
    }
  }
}
