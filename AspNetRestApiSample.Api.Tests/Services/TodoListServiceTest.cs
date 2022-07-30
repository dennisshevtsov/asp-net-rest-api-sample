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
    public async Task GetAttachedTodoListAsync()
    {
      await _todoListService.GetAttachedTodoListAsync(null, CancellationToken.None);
    }

    [TestMethod]
    public void GetTodoList()
    {
      _todoListService.GetTodoList(new TodoListEntity());
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
