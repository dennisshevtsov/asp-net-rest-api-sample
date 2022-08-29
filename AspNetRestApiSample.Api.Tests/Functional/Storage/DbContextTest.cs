// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Functional.Storage
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetRestApiSample.Api.Storage;

  [TestClass]
  public sealed class DbContextTest
  {
#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var scope = new ServiceCollection().AddDatabase(null)
                                         .BuildServiceProvider()
                                         .CreateScope();

      _dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
      _dbContext.Database.EnsureCreated();

      _disposable = scope;
    }

    [TestCleanup]
    public void CleanUp()
    {
      _dbContext.Database.EnsureDeleted();
      _disposable.Dispose();
    }
  }
}
