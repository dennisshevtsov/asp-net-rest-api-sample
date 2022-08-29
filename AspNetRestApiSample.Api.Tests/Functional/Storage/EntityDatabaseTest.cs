// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Functional.Storage
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetRestApiSample.Api.Storage;
  using System.Configuration;
  using Microsoft.Extensions.Configuration;

  [TestClass]
  public sealed class EntityDatabaseTest
  {
#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;
    private IEntityDatabase _entityDatabase;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                    .Build();

      var scope = new ServiceCollection().AddDatabase(configuration)
                                         .BuildServiceProvider()
                                         .CreateScope();

      _dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
      _dbContext.Database.EnsureCreated();

      _entityDatabase = scope.ServiceProvider.GetRequiredService<IEntityDatabase>();

      _disposable = scope;
    }

    [TestCleanup]
    public void CleanUp()
    {
      _dbContext.Database.EnsureDeleted();
      _disposable.Dispose();
    }

    [TestMethod]
    public void Test()
    {
      _dbContext.Add(new TodoListEntity
      {
        Title
      });
    }
  }
}
