// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Integration.Storage
{
  using System.Threading;

  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using AspNetRestApiSample.Api.Storage;

  [TestClass]
  public sealed class DbContextTest
  {
#pragma warning disable CS8618
    private DbContext _dbContext;

    private IDisposable _disposable;
    private CancellationToken _cancellationToken;
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

      _disposable = scope;
      _cancellationToken = CancellationToken.None;
    }

    [TestCleanup]
    public void CleanUp()
    {
      _dbContext?.Database?.EnsureDeleted();
      _disposable?.Dispose();
    }

    [TestMethod]
    public async Task Add_Should_Save_Entity()
    {
      var title = Guid.NewGuid().ToString();
      var description = Guid.NewGuid().ToString();

      var todoListEntity = new TodoListEntity
      {
        Title = title,
        Description = description,
      };

      _dbContext.Add(todoListEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      Assert.IsTrue(todoListEntity.Id != default);
      Assert.IsTrue(todoListEntity.TodoListId != default);
      Assert.IsTrue(todoListEntity.Id == todoListEntity.TodoListId);
      Assert.AreEqual(title, todoListEntity.Title);
      Assert.AreEqual(description, todoListEntity.Description);

      _dbContext.Entry(todoListEntity).State = EntityState.Detached;

      var dbTodoListEntity =
        await _dbContext.Set<TodoListEntity>()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListEntity);
      Assert.AreEqual(title, dbTodoListEntity.Title);
      Assert.AreEqual(description, dbTodoListEntity.Description);
    }
  }
}
