// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using Moq;

  using AspNetRestApiSample.Api.Storage;

  [TestClass]
  public sealed class TodoListTaskServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<ITodoListEntityCollection> _todoListEntityCollectionMock;
    private Mock<ITodoListTaskEntityCollection> _todoListTaskEntityCollectionMock;
    private Mock<IEntityContainer> _entityContainerMock;

    private TodoListTaskService _todoListTaskService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _todoListEntityCollectionMock = new Mock<ITodoListEntityCollection>();
      _todoListTaskEntityCollectionMock = new Mock<ITodoListTaskEntityCollection>();
      _entityContainerMock = new Mock<IEntityContainer>();

      _entityContainerMock.SetupGet(container => container.TodoLists)
                          .Returns(_todoListEntityCollectionMock.Object);

      _entityContainerMock.SetupGet(container => container.TodoListTasks)
                          .Returns(_todoListTaskEntityCollectionMock.Object);

      _todoListTaskService = new TodoListTaskService(_entityContainerMock.Object);
    }

    [TestMethod]
    public async Task GetAttachedTodoListTaskEntityAsync_Should_Return_Attached_Entity()
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

      _todoListTaskEntityCollectionMock.Setup(collection => collection.GetAttachedAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                       .ReturnsAsync(testTodoListTaskEntity)
                                       .Verifiable();

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actulalTodoListTaskEntity =
        await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(query, _cancellationToken);

      Assert.AreEqual(testTodoListTaskEntity, actulalTodoListTaskEntity);

      _todoListTaskEntityCollectionMock.Verify(collection => collection.GetAttachedAsync(todoListTaskId, todoListId, _cancellationToken));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityContainerMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetDetachedTodoListTaskEntityAsync_Should_Return_Detached_Entity()
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

      _todoListTaskEntityCollectionMock.Setup(collection => collection.GetDetachedAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                       .ReturnsAsync(testTodoListTaskEntity)
                                       .Verifiable();

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actulalTodoListTaskEntity =
        await _todoListTaskService.GetDetachedTodoListTaskEntityAsync(query, _cancellationToken);

      Assert.AreEqual(testTodoListTaskEntity, actulalTodoListTaskEntity);

      _todoListTaskEntityCollectionMock.Verify(collection => collection.GetDetachedAsync(todoListTaskId, todoListId, _cancellationToken));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityContainerMock.VerifyNoOtherCalls();
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
      var todoListId = Guid.NewGuid();
      
      var todoListTaskEntityCollection = new[]
      {
        new TodoListTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = todoListId,
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = todoListId,
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = todoListId,
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
      };

      _todoListTaskEntityCollectionMock.Setup(collection => collection.GetDetachedTodoListTasksAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                       .ReturnsAsync(todoListTaskEntityCollection)
                                       .Verifiable();

      var query = new SearchTodoListTasksRequestDto
      {
        TodoListId = todoListId,
      };

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

      _todoListTaskEntityCollectionMock.Verify(collection => collection.GetDetachedTodoListTasksAsync(todoListId, _cancellationToken));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityContainerMock.VerifyNoOtherCalls();
    }
  }
}
