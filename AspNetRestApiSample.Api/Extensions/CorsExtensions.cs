// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  public static class CorsExtensions
  {
    public static IServiceCollection AddCofiguredCors(
      this IServiceCollection services, IConfiguration configuration)
    {
      var settings = configuration.Get<CorsSettings>();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(policy =>
        {
          for (int i = 0; i < settings.OriginCollection.Length; i++)
          {
            policy.WithMethods(settings.OriginCollection[i]);
          }

          for (int i = 0; i < settings.MethodCollection.Length; i++)
          {
            policy.WithMethods(settings.MethodCollection[i]);
          }

          for (int i = 0; i < settings.HeaderCollection.Length; i++)
          {
            policy.WithMethods(settings.HeaderCollection[i]);
          }
        });
      });

      return services;
    }

    public sealed class CorsSettings
    {
      public string? Origins { get; set; }

      public string[] OriginCollection => CorsSettings.ToCollection(Origins);

      public string? Methods { get; set; }

      public string[] MethodCollection => CorsSettings.ToCollection(Methods);

      public string? Headers { get; set; }

      public string[] HeaderCollection => CorsSettings.ToCollection(Headers);

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
