// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Integration.Storage
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetRestApiSample.Api.Storage;

  [TestClass]
  public sealed class EntityDatabaseTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private IDisposable _disposable;

    private DbContext _dbContext;
    private IEntityDatabase _entityDatabase;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

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
    public async Task TodoLists_AddOrUpdate_Should_Save_Todo_List()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _entityDatabase.TodoLists.Add(todoListEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      Assert.IsTrue(todoListEntity.Id != default);
      Assert.IsTrue(todoListEntity.Id == todoListEntity.TodoListId);

      var dbTodoListEntity0 =
        await _dbContext.Set<TodoListEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListEntity0);
      Assert.AreEqual(todoListEntity.Title, dbTodoListEntity0.Title);
      Assert.AreEqual(todoListEntity.Description, dbTodoListEntity0.Description);
    }
  }
}
