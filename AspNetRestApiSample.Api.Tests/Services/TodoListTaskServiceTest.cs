// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using AspNetRestApiSample.Api.Storage;
  using Microsoft.EntityFrameworkCore;
  using Moq;

  [TestClass]
  public sealed class TodoListTaskServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IEntityContainer> _entityContainer;
    private TodoListTaskService _todoListTaskService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _entityContainer = new Mock<IEntityContainer>();
      _todoListTaskService = new TodoListTaskService(_entityContainer.Object);
    }

    [TestMethod]
    public async Task GetAttachedTodoListTaskEntityAsync_Should_Return_Entity()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var testTodoListTaskEntity = new TodoListTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListTaskEntityCollection = new[]
      {
        testTodoListTaskEntity,
      };

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actulalTodoListTaskEntity =
        await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(query, _cancellationToken);

      Assert.IsNotNull(actulalTodoListTaskEntity);

      Assert.AreEqual(testTodoListTaskEntity.Id, actulalTodoListTaskEntity.Id);
      Assert.AreEqual(testTodoListTaskEntity.TodoListId, actulalTodoListTaskEntity.TodoListId);
      Assert.AreEqual(testTodoListTaskEntity.Title, actulalTodoListTaskEntity.Title);
      Assert.AreEqual(testTodoListTaskEntity.Description, actulalTodoListTaskEntity.Description);
    }

    [TestMethod]
    public async Task GetDetachedTodoListTaskEntityAsync_Should_Return_Entity()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var testTodoListTaskEntity = new TodoListTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListTaskEntityCollection = new[]
      {
        testTodoListTaskEntity,
      };

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actulalTodoListTaskEntity =
        await _todoListTaskService.GetDetachedTodoListTaskEntityAsync(query, _cancellationToken);

      Assert.IsNotNull(actulalTodoListTaskEntity);

      Assert.AreEqual(testTodoListTaskEntity.Id, actulalTodoListTaskEntity.Id);
      Assert.AreEqual(testTodoListTaskEntity.TodoListId, actulalTodoListTaskEntity.TodoListId);
      Assert.AreEqual(testTodoListTaskEntity.Title, actulalTodoListTaskEntity.Title);
      Assert.AreEqual(testTodoListTaskEntity.Description, actulalTodoListTaskEntity.Description);
    }

    [TestMethod]
    public void GetTodoListTask_Should_Return_Populated_Dto()
    {
      var todoListTaskEntity = new TodoListTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var getTodoListTaskResponseDto = _todoListTaskService.GetTodoListTask(todoListTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      Assert.AreEqual(todoListTaskEntity.Id, getTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, getTodoListTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListTaskEntity.Title, getTodoListTaskResponseDto.Title);
      Assert.AreEqual(todoListTaskEntity.Description, getTodoListTaskResponseDto.Description);
    }

    [TestMethod]
    public async Task SearchTodoListTasksAsync_Should_Return_Populated_Dtos()
    {
      var todoListTaskEntityCollection = new[]
      {
        new TodoListTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = Guid.NewGuid(),
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = Guid.NewGuid(),
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = Guid.NewGuid(),
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
      };

      var query = new SearchTodoListTasksRequestDto();

      var searchTodoListTaskRecordResponseDtos = await _todoListTaskService.SearchTodoListTasksAsync(
        query, _cancellationToken);

      Assert.IsNotNull(searchTodoListTaskRecordResponseDtos);
      Assert.AreEqual(todoListTaskEntityCollection.Length, searchTodoListTaskRecordResponseDtos.Length);

      for (int i = 0; i < todoListTaskEntityCollection.Length; i++)
      {
        var todoListTaskEntity = todoListTaskEntityCollection[i];
        var searchTodoListTaskRecordResponseDto = searchTodoListTaskRecordResponseDtos.FirstOrDefault(dto => dto.TodoListTaskId == todoListTaskEntity.Id);

        Assert.IsNotNull(searchTodoListTaskRecordResponseDto);

        Assert.AreEqual(todoListTaskEntity.TodoListId, searchTodoListTaskRecordResponseDto.TodoListId);
        Assert.AreEqual(todoListTaskEntity.Title, searchTodoListTaskRecordResponseDto.Title);
        Assert.AreEqual(todoListTaskEntity.Description, searchTodoListTaskRecordResponseDto.Description);
      }
    }
  }
}
