// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using Microsoft.EntityFrameworkCore;

  /// <summary>Provides a simple API to configure an application.S</summary>
  public static class ApplicationExtensions
  {
    /// <summary>Initializes a database.</summary>
    /// <param name="app">An object that defines a class that provides the mechanisms to configure an application's request pipeline.</param>
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        scope.ServiceProvider.GetRequiredService<DbContext>().Database.EnsureCreated();
      }
    }
  }
}
