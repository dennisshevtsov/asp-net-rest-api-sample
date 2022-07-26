﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Infrascruture.Test.Integration
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

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
    public async Task TodoLists_Add_Should_Add_New_Todo_List()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _entityDatabase.TodoLists.Attache(todoListEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      Assert.IsTrue(todoListEntity.Id != default);
      Assert.IsTrue(todoListEntity.Id == todoListEntity.TodoListId);

      var dbTodoListEntity =
        await _dbContext.Set<TodoListEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListEntity);
      Assert.AreEqual(todoListEntity.Title, dbTodoListEntity.Title);
      Assert.AreEqual(todoListEntity.Description, dbTodoListEntity.Description);
    }

    [TestMethod]
    public async Task CommitAsync_Should_Update_Existing_Todo_List()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListEntityEntry = _dbContext.Entry(todoListEntity);

      todoListEntityEntry.State = EntityState.Added;
      await _dbContext.SaveChangesAsync(_cancellationToken);
      todoListEntityEntry.State = EntityState.Detached;

      var dbTodoListEntity0 =
        await _entityDatabase.TodoLists.GetAttachedAsync(
          todoListEntity.Id, todoListEntity.TodoListId, _cancellationToken);

      Assert.IsNotNull(dbTodoListEntity0);

      var newTitle = Guid.NewGuid().ToString();
      var newDescription = Guid.NewGuid().ToString();

      dbTodoListEntity0.Title = newTitle;
      dbTodoListEntity0.Description = newDescription;

      await _entityDatabase.CommitAsync(_cancellationToken);

      var dbTodoListEntity1 =
        await _dbContext.Set<TodoListEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListEntity1);
      Assert.AreEqual(newTitle, dbTodoListEntity1.Title);
      Assert.AreEqual(newDescription, dbTodoListEntity1.Description);
    }

    [TestMethod]
    public async Task TodoLists_Delete_Should_Delete_Existing_Todo_List()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListEntityEntry = _dbContext.Entry(todoListEntity);

      todoListEntityEntry.State = EntityState.Added;
      await _dbContext.SaveChangesAsync(_cancellationToken);
      todoListEntityEntry.State = EntityState.Detached;

      var dbTodoListEntity0 =
        await _entityDatabase.TodoLists.GetAttachedAsync(
          todoListEntity.Id, todoListEntity.TodoListId, _cancellationToken);

      Assert.IsNotNull(dbTodoListEntity0);

      _entityDatabase.TodoLists.Delete(dbTodoListEntity0);
      await _entityDatabase.CommitAsync(_cancellationToken);

      var dbTodoListEntity1 =
        await _dbContext.Set<TodoListEntity>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNull(dbTodoListEntity1);
    }

    [TestMethod]
    public async Task TodoListTasks_Add_Should_Add_New_Todo_List_Task()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListEntityEntry = _dbContext.Entry(todoListEntity);

      todoListEntityEntry.State = EntityState.Added;
      await _dbContext.SaveChangesAsync(_cancellationToken);
      todoListEntityEntry.State = EntityState.Detached;

      var todoListDayTaskEntity = new TodoListDayTaskEntity()
      {
        TodoListId = todoListEntity.TodoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1).Ticks,
      };

      var todoListPeriodTaskEntity = new TodoListPeriodTaskEntity()
      {
        TodoListId = todoListEntity.TodoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Begin = new DateTime(2022, 9, 1, 12, 30, 0).Ticks,
        End = new DateTime(2022, 9, 1, 13, 30, 0).Ticks,
      };

      _entityDatabase.TodoListTasks.Attache(todoListDayTaskEntity);
      _entityDatabase.TodoListTasks.Attache(todoListPeriodTaskEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      Assert.IsTrue(todoListDayTaskEntity.Id != default);
      Assert.AreEqual(todoListEntity.TodoListId, todoListDayTaskEntity.TodoListId);

      Assert.IsTrue(todoListPeriodTaskEntity.Id != default);
      Assert.AreEqual(todoListEntity.TodoListId, todoListPeriodTaskEntity.TodoListId);

      var dbTodoListDayTaskEntity0 =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListDayTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListDayTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken) as TodoListDayTaskEntity;

      Assert.IsNotNull(dbTodoListDayTaskEntity0);

      _entityDatabase.TodoListTasks.Delete(todoListDayTaskEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      var dbTodoListDayTaskEntity1 =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListDayTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListDayTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken) as TodoListDayTaskEntity;

      Assert.IsNull(dbTodoListDayTaskEntity1);

      var dbTodoListPeriodTaskEntity0 =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListPeriodTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListPeriodTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken) as TodoListPeriodTaskEntity;

      Assert.IsNotNull(dbTodoListPeriodTaskEntity0);

      _entityDatabase.TodoListTasks.Delete(todoListPeriodTaskEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      var dbTodoListPeriodTaskEntity =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListPeriodTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListPeriodTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken) as TodoListPeriodTaskEntity;

      Assert.IsNull(dbTodoListPeriodTaskEntity);
    }

    [TestMethod]
    public async Task CommitAsync_Should_Update_Existing_Todo_List_Task()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListEntityEntry = _dbContext.Entry(todoListEntity);

      todoListEntityEntry.State = EntityState.Added;
      await _dbContext.SaveChangesAsync(_cancellationToken);
      todoListEntityEntry.State = EntityState.Detached;

      var todoListDayTaskEntity = new TodoListDayTaskEntity()
      {
        TodoListId = todoListEntity.TodoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1).Ticks,
      };

      var todoListPeriodTaskEntity = new TodoListPeriodTaskEntity()
      {
        TodoListId = todoListEntity.TodoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Begin = new DateTime(2022, 9, 1, 12, 30, 0).Ticks,
        End = new DateTime(2022, 9, 1, 13, 30, 0).Ticks,
      };

      _entityDatabase.TodoListTasks.Attache(todoListDayTaskEntity);
      _entityDatabase.TodoListTasks.Attache(todoListPeriodTaskEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      Assert.IsTrue(todoListDayTaskEntity.Id != default);
      Assert.AreEqual(todoListEntity.TodoListId, todoListDayTaskEntity.TodoListId);

      Assert.IsTrue(todoListPeriodTaskEntity.Id != default);
      Assert.AreEqual(todoListEntity.TodoListId, todoListPeriodTaskEntity.TodoListId);

      var dbTodoListDayTaskEntity0 =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListDayTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListDayTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken) as TodoListDayTaskEntity;

      Assert.IsNotNull(dbTodoListDayTaskEntity0);

      var dbTodoListPeriodTaskEntity =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListPeriodTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListPeriodTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken) as TodoListPeriodTaskEntity;

      Assert.IsNotNull(dbTodoListPeriodTaskEntity);
    }

    [TestMethod]
    public async Task TodoListTasks_Delete_Should_Delete_Existing_Todo_List_Task()
    {
      var todoListEntity = new TodoListEntity()
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListEntityEntry = _dbContext.Entry(todoListEntity);

      todoListEntityEntry.State = EntityState.Added;
      await _dbContext.SaveChangesAsync(_cancellationToken);
      todoListEntityEntry.State = EntityState.Detached;

      var todoListTaskEntity = new TodoListDayTaskEntity()
      {
        TodoListId = todoListEntity.TodoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1).Ticks,
      };

      _entityDatabase.TodoListTasks.Attache(todoListTaskEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      Assert.IsTrue(todoListTaskEntity.Id != default);
      Assert.AreEqual(todoListEntity.TodoListId, todoListTaskEntity.TodoListId);

      var dbTodoListTaskEntity0 =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNotNull(dbTodoListTaskEntity0);

      _entityDatabase.TodoListTasks.Delete(todoListTaskEntity);
      await _entityDatabase.CommitAsync(_cancellationToken);

      var dbTodoListTaskEntity1 =
        await _dbContext.Set<TodoListTaskEntityBase>()
                        .AsNoTracking()
                        .WithPartitionKey(todoListTaskEntity.TodoListId.ToString())
                        .Where(entity => entity.Id == todoListTaskEntity.Id)
                        .FirstOrDefaultAsync(_cancellationToken);

      Assert.IsNull(dbTodoListTaskEntity1);
    }
  }
}
