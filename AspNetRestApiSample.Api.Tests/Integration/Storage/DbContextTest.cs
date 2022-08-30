﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private DbContext _dbContext;

    private IDisposable _disposable;
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

      _disposable = scope;
    }

    [TestCleanup]
    public void CleanUp()
    {
      _dbContext?.Database?.EnsureDeleted();
      _disposable?.Dispose();
    }

    [TestMethod]
    public async Task Add_Should_Save_Todo_List()
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

    [TestMethod]
    public async Task Update_Should_Save_Todo_List()
    {
      var todoListEntity = new TodoListEntity
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Add(todoListEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      Assert.IsTrue(todoListEntity.Id != default);
      Assert.IsTrue(todoListEntity.TodoListId != default);
      Assert.IsTrue(todoListEntity.Id == todoListEntity.TodoListId);

      todoListEntity.Title = Guid.NewGuid().ToString();
      todoListEntity.Description = Guid.NewGuid().ToString();

      await _dbContext.SaveChangesAsync(_cancellationToken);

      _dbContext.Entry(todoListEntity).State = EntityState.Detached;

      var dbTodoListEntity =
        await _dbContext.Set<TodoListEntity>()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListEntity);
      Assert.AreEqual(todoListEntity.Title, dbTodoListEntity.Title);
      Assert.AreEqual(todoListEntity.Description, dbTodoListEntity.Description);
    }

    [TestMethod]
    public async Task Delete_Should_Delete_Todo_List()
    {
      var todoListEntity = new TodoListEntity
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Add(todoListEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      Assert.IsTrue(todoListEntity.Id != default);
      Assert.IsTrue(todoListEntity.TodoListId != default);
      Assert.IsTrue(todoListEntity.Id == todoListEntity.TodoListId);

      _dbContext.Entry(todoListEntity).State = EntityState.Deleted;

      await _dbContext.SaveChangesAsync(_cancellationToken);

      var dbTodoListEntity =
        await _dbContext.Set<TodoListEntity>()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNull(dbTodoListEntity);
    }

    [TestMethod]
    public async Task Add_Should_Save_Todo_List_Day_Task()
    {
      var todoListEntity = new TodoListEntity
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Add(todoListEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      _dbContext.Entry(todoListEntity).State = EntityState.Detached;

      var todoListDayTaskEntity = new TodoListDayTaskEntity
      {
        TodoListId = todoListEntity.TodoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1),
      };

      _dbContext.Add(todoListDayTaskEntity);

      await _dbContext.SaveChangesAsync(_cancellationToken);

      var dbTodoListTaskEntity =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListDayTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListTaskEntity);
      Assert.AreEqual(todoListDayTaskEntity.Title, dbTodoListTaskEntity.Title);
      Assert.AreEqual(todoListDayTaskEntity.Description, dbTodoListTaskEntity.Description);

      var dbTodoListDayTaskEntity = dbTodoListTaskEntity as TodoListDayTaskEntity;

      Assert.IsNotNull(dbTodoListDayTaskEntity);
      Assert.AreEqual(todoListDayTaskEntity.Date, dbTodoListDayTaskEntity.Date);
    }
  }
}
