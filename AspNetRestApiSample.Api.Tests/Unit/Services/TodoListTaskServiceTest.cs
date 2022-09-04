// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Services
{
  using AutoMapper;
  using Moq;

  using AspNetRestApiSample.Api.Storage;

  [TestClass]
  public sealed class TodoListTaskServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private Mock<ITodoListEntityCollection> _todoListEntityCollectionMock;
    private Mock<ITodoListTaskEntityCollection> _todoListTaskEntityCollectionMock;
    private Mock<IEntityDatabase> _entityDatabaseMock;

    private TodoListTaskService _todoListTaskService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _mapperMock = new Mock<IMapper>();

      _todoListEntityCollectionMock = new Mock<ITodoListEntityCollection>();
      _todoListTaskEntityCollectionMock = new Mock<ITodoListTaskEntityCollection>();
      _entityDatabaseMock = new Mock<IEntityDatabase>();

      _entityDatabaseMock.SetupGet(database => database.TodoLists)
                         .Returns(_todoListEntityCollectionMock.Object);

      _entityDatabaseMock.SetupGet(database => database.TodoListTasks)
                         .Returns(_todoListTaskEntityCollectionMock.Object);

      _todoListTaskService = new TodoListTaskService(_mapperMock.Object, _entityDatabaseMock.Object);
    }

    [TestMethod]
    public async Task GetAttachedTodoListTaskEntityAsync_Should_Return_Attached_Entity()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var testTodoListTaskEntity = new TodoListDayTaskEntity
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

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetDetachedTodoListTaskEntityAsync_Should_Return_Detached_Entity()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var testTodoListTaskEntity = new TodoListDayTaskEntity
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

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void GetTodoListTask_Should_Populate_Dto_With_Entity()
    {
      var todoListDayTaskEntity = new TodoListDayTaskEntity();

      _mapperMock.Setup(mapper => mapper.Map<GetTodoListTaskResponseDtoBase>(todoListDayTaskEntity))
                 .Returns(new GetTodoListDayTaskResponseDto())
                 .Verifiable();

      var getTodoListTaskResponseDto = _todoListTaskService.GetTodoListTask(todoListDayTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      _mapperMock.Verify(mapper => mapper.Map<GetTodoListTaskResponseDtoBase>(todoListDayTaskEntity));
      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SearchTodoListTasksAsync_Should_Return_Populated_Dtos()
    {
      var todoListTaskEntityCollection = new TodoListTaskEntityBase[0];

      _todoListTaskEntityCollectionMock.Setup(collection => collection.GetDetachedTodoListTasksAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                       .ReturnsAsync(todoListTaskEntityCollection)
                                       .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map<SearchTodoListTasksRecordResponseDtoBase[]>(It.IsAny<TodoListTaskEntityBase[]>()))
                 .Returns(new SearchTodoListTasksRecordResponseDtoBase[0])
                 .Verifiable();

      var todoListId = Guid.NewGuid();

      var query = new SearchTodoListTasksRequestDto
      {
        TodoListId = todoListId,
      };

      var searchTodoListTaskRecordResponseDtos = await _todoListTaskService.SearchTodoListTasksAsync(
        query, _cancellationToken);

      Assert.IsNotNull(searchTodoListTaskRecordResponseDtos);

      _mapperMock.Verify(mapper => mapper.Map<SearchTodoListTasksRecordResponseDtoBase[]>(todoListTaskEntityCollection));
      _mapperMock.VerifyNoOtherCalls();
      
      _todoListTaskEntityCollectionMock.Verify(collection => collection.GetDetachedTodoListTasksAsync(todoListId, _cancellationToken));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTaskAsync_Should_Save_Todo_List_Task()
    {
      var todoListTaskEntity = new TodoListDayTaskEntity();

      _mapperMock.Setup(mapper => mapper.Map<TodoListTaskEntityBase>(It.IsAny<AddTodoListTaskRequestDtoBase>()))
                 .Returns(todoListTaskEntity)
                 .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map<AddTodoListTaskResponseDto>(It.IsAny<TodoListDayTaskEntity>()))
                 .Returns(new AddTodoListTaskResponseDto())
                 .Verifiable();

      _todoListTaskEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<TodoListTaskEntityBase>()))
                                       .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new AddTodoListDayTaskRequestDto();

      var addTodoListTaskResponseDto =
        await _todoListTaskService.AddTodoListTaskAsync(command, _cancellationToken);

      Assert.IsNotNull(addTodoListTaskResponseDto);

      _mapperMock.Verify(mapper => mapper.Map<TodoListTaskEntityBase>(command));
      _mapperMock.Verify(mapper => mapper.Map<AddTodoListTaskResponseDto>(todoListTaskEntity));
      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Add(todoListTaskEntity));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoListTaskAsync_Should_Save_Todo_List_Task()
    {
      _todoListTaskEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<TodoListTaskEntityBase>()))
                                       .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      var command = new UpdateTodoListDayTaskRequestDto();

      await _todoListTaskService.UpdateTodoListTaskAsync(command, todoListTaskEntity, _cancellationToken);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Add(todoListTaskEntity));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task DeleteTodoListTaskAsync_Should_Delete_Todo_List_Task()
    {
      _todoListTaskEntityCollectionMock.Setup(collection => collection.Delete(It.IsAny<TodoListTaskEntityBase>()))
                                       .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var todoListTaskEntity = new TodoListDayTaskEntity();

      await _todoListTaskService.DeleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken);

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Delete(todoListTaskEntity));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task CompleteTodoListTaskAsync_Should_Update_Todo_List_Task()
    {
      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Completed = false,
      };

      await _todoListTaskService.CompleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken);

      Assert.IsTrue(todoListTaskEntity.Completed);

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task CompleteTodoListTaskAsync_Should_Not_Update_Todo_List_Task()
    {
      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Completed = true,
      };

      await _todoListTaskService.CompleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken);

      Assert.IsTrue(todoListTaskEntity.Completed);

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();
      _todoListEntityCollectionMock.VerifyNoOtherCalls();
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UncompleteTodoListTaskAsync_Should_Update_Todo_List_Task()
    {
      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Completed = true,
      };

      await _todoListTaskService.UncompleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken);

      Assert.IsFalse(todoListTaskEntity.Completed);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UncompleteTodoListTaskAsync_Should_Not_Update_Todo_List_Task()
    {
      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Completed = false,
      };

      await _todoListTaskService.UncompleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken);

      Assert.IsFalse(todoListTaskEntity.Completed);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    #region Test Classes

    private sealed class TestTodoListTaskEntity : TodoListTaskEntityBase
    {
    }

    private sealed class TestAddTodoListTaskRequestDto : AddTodoListTaskRequestDtoBase
    {
    }

    #endregion
  }
}
