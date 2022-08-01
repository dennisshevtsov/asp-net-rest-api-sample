﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Storage;

  [TestClass]
  public sealed class TodoListServiceTest
  {
#pragma warning disable CS8618
    private DbContext _dbContext;
    private TodoListService _todoListService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _dbContext = new AspNetRestApiSampleDbContext(
        new DbContextOptionsBuilder().UseInMemoryDatabase("test").Options);
      _todoListService = new TodoListService(_dbContext);
    }

    [TestCleanup]
    public void Cleanup() => _dbContext.Dispose();

    [TestMethod]
    public async Task GetDetachedTodoListAsync_Should_Return_Null()
    {
      var todoListId = Guid.NewGuid();
      var testTodoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(testTodoListEntity);
      await _dbContext.SaveChangesAsync();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actualTodoListEntity = await _todoListService.GetDetachedTodoListAsync(query, CancellationToken.None);

      Assert.IsNull(actualTodoListEntity);
    }

    [TestMethod]
    public async Task GetDetachedTodoListAsync_Should_Return_Detached_Entity()
    {
      var todoListId = Guid.NewGuid();
      var testTodoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(testTodoListEntity);
      await _dbContext.SaveChangesAsync();

      var query = new GetTodoListRequestDto
      {
        TodoListId = todoListId,
      };

      var actualTodoListEntity = await _todoListService.GetDetachedTodoListAsync(query, CancellationToken.None);

      Assert.IsNotNull(actualTodoListEntity);

      Assert.AreEqual(testTodoListEntity.Id, actualTodoListEntity.Id);
      Assert.AreEqual(testTodoListEntity.TodoListId, actualTodoListEntity.TodoListId);
      Assert.AreEqual(testTodoListEntity.Title, actualTodoListEntity.Title);
      Assert.AreEqual(testTodoListEntity.Description, actualTodoListEntity.Description);

      var actualTodoListEntityEntry = _dbContext.Entry(actualTodoListEntity);

      Assert.AreEqual(EntityState.Detached, actualTodoListEntityEntry.State);
    }

    [TestMethod]
    public async Task GetAttachedTodoListAsync_Should_Return_Null()
    {
      var todoListId = Guid.NewGuid();
      var testTodoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(testTodoListEntity);
      await _dbContext.SaveChangesAsync();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actualTodoListEntity = await _todoListService.GetAttachedTodoListAsync(query, CancellationToken.None);

      Assert.IsNull(actualTodoListEntity);
    }

    [TestMethod]
    public async Task GetAttachedTodoListAsync_Should_Return_Attached_Entity()
    {
      var todoListId = Guid.NewGuid();
      var testTodoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(testTodoListEntity);
      await _dbContext.SaveChangesAsync();

      var query = new GetTodoListRequestDto
      {
        TodoListId = todoListId,
      };

      var actualTodoListEntity = await _todoListService.GetAttachedTodoListAsync(query, CancellationToken.None);

      Assert.IsNotNull(actualTodoListEntity);

      Assert.AreEqual(testTodoListEntity.Id, actualTodoListEntity.Id);
      Assert.AreEqual(testTodoListEntity.TodoListId, actualTodoListEntity.TodoListId);
      Assert.AreEqual(testTodoListEntity.Title, actualTodoListEntity.Title);
      Assert.AreEqual(testTodoListEntity.Description, actualTodoListEntity.Description);

      var actualTodoListEntityEntry = _dbContext.Entry(actualTodoListEntity);

      Assert.AreEqual(EntityState.Unchanged, actualTodoListEntityEntry.State);
    }

    [TestMethod]
    public void GetTodoList_Should_Return_Filled_Dto()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListResponseDto = _todoListService.GetTodoList(todoListEntity);

      Assert.IsNotNull(todoListResponseDto);

      Assert.AreEqual(todoListEntity.Id, todoListResponseDto.TodoListId);
      Assert.AreEqual(todoListEntity.Title, todoListResponseDto.Title);
      Assert.AreEqual(todoListEntity.Description, todoListResponseDto.Description);
    }

    [TestMethod]
    public async Task SearchTodoListsAsync_Should_Return_Entity_Collection()
    {
      var todoListId0 = Guid.NewGuid();
      var todoListEntity0 = new TodoListEntity
      {
        Id = todoListId0,
        TodoListId = todoListId0,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(todoListEntity0);

      var todoListId1 = Guid.NewGuid();
      var todoListEntity1 = new TodoListEntity
      {
        Id = todoListId1,
        TodoListId = todoListId1,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(todoListEntity1);

      var todoListId2 = Guid.NewGuid();
      var todoListEntity2 = new TodoListEntity
      {
        Id = todoListId2,
        TodoListId = todoListId2,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _dbContext.Set<TodoListEntity>().Add(todoListEntity2);

      await _dbContext.SaveChangesAsync();

      var searchTodoListRecordRequestDtos = await _todoListService.SearchTodoListsAsync(new SearchTodoListsRequestDto(), CancellationToken.None);

      Assert.IsNotNull(searchTodoListRecordRequestDtos);
      Assert.AreEqual(3, searchTodoListRecordRequestDtos.Length);

      var searchTodoListRecordRequestDto0 = searchTodoListRecordRequestDtos.FirstOrDefault(dto => dto.TodoListId == todoListId0);

      Assert.IsNotNull(searchTodoListRecordRequestDto0);

      Assert.AreEqual(todoListEntity0.TodoListId, searchTodoListRecordRequestDto0.TodoListId);
      Assert.AreEqual(todoListEntity0.Title, searchTodoListRecordRequestDto0.Title);
      Assert.AreEqual(todoListEntity0.Description, searchTodoListRecordRequestDto0.Description);

      var searchTodoListRecordRequestDto1 = searchTodoListRecordRequestDtos.FirstOrDefault(dto => dto.TodoListId == todoListId1);

      Assert.IsNotNull(searchTodoListRecordRequestDto1);

      Assert.AreEqual(todoListEntity1.TodoListId, searchTodoListRecordRequestDto1.TodoListId);
      Assert.AreEqual(todoListEntity1.Title, searchTodoListRecordRequestDto1.Title);
      Assert.AreEqual(todoListEntity1.Description, searchTodoListRecordRequestDto1.Description);

      var searchTodoListRecordRequestDto2 = searchTodoListRecordRequestDtos.FirstOrDefault(dto => dto.TodoListId == todoListId2);

      Assert.IsNotNull(searchTodoListRecordRequestDto2);

      Assert.AreEqual(todoListEntity2.TodoListId, searchTodoListRecordRequestDto2.TodoListId);
      Assert.AreEqual(todoListEntity2.Title, searchTodoListRecordRequestDto2.Title);
      Assert.AreEqual(todoListEntity2.Description, searchTodoListRecordRequestDto2.Description);
    }

    [TestMethod]
    public async Task AddTodoListAsync_Should_Create_New_Todo_List()
    {
      var addTodoListRequestDto = new AddTodoListRequestDto
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var addTodoListResponseDto = await _todoListService.AddTodoListAsync(addTodoListRequestDto, CancellationToken.None);

      Assert.IsNotNull(addTodoListResponseDto);
      Assert.IsTrue(addTodoListResponseDto.TodoListId != default);

      var todoListEntity =
        await _dbContext.Set<TodoListEntity>()
                        .AsNoTracking()
                        .Where(entity => entity.TodoListId == addTodoListResponseDto.TodoListId)
                        .Where(entity => entity.Id == addTodoListResponseDto.TodoListId)
                        .FirstOrDefaultAsync(CancellationToken.None);

      Assert.IsNotNull(todoListEntity);
      Assert.AreEqual(addTodoListRequestDto.Title, todoListEntity.Title);
      Assert.AreEqual(addTodoListRequestDto.Description, todoListEntity.Description);
    }

    [TestMethod]
    public async Task UpdateTodoListAsync()
    {
      await _todoListService.UpdateTodoListAsync(new UpdateTodoListRequestDto(), new TodoListEntity(), CancellationToken.None);
    }

    [TestMethod]
    public async Task DeleteTodoListAsync()
    {
      await _todoListService.DeleteTodoListAsync(new TodoListEntity(), CancellationToken.None);
    }
  }
}
