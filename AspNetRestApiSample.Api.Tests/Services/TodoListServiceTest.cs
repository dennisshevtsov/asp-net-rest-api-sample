// Copyright (c) Dennis Shevtsov. All rights reserved.
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
    public async Task GetDetachedTodoListAsync()
    {
      await _todoListService.GetDetachedTodoListAsync(null, CancellationToken.None);
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
    public async Task SearchTodoListsAsync()
    {
      await _todoListService.SearchTodoListsAsync(new SearchTodoListsRequestDto(), CancellationToken.None);
    }

    [TestMethod]
    public async Task AddTodoListAsync()
    {
      await _todoListService.AddTodoListAsync(new AddTodoListRequestDto(), CancellationToken.None);
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
