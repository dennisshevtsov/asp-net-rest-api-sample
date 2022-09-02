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
    public void GetTodoListTask_Should_Return_Populated_Dto_For_Day_Task()
    {
      var todoListDayTaskEntity = new TodoListDayTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 8, 22),
      };

      var getTodoListTaskResponseDto = _todoListTaskService.GetTodoListTask(todoListDayTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      var todoListDayTaskResponseDto = getTodoListTaskResponseDto as GetTodoListDayTaskResponseDto;

      Assert.IsNotNull(todoListDayTaskResponseDto);

      Assert.AreEqual(todoListDayTaskEntity.Id, todoListDayTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListDayTaskEntity.TodoListId, todoListDayTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListDayTaskEntity.Title, todoListDayTaskResponseDto.Title);
      Assert.AreEqual(todoListDayTaskEntity.Description, todoListDayTaskResponseDto.Description);
      Assert.AreEqual(todoListDayTaskEntity.Completed, todoListDayTaskResponseDto.Completed);
      Assert.AreEqual(todoListDayTaskEntity.Date, todoListDayTaskResponseDto.Date);
    }

    [TestMethod]
    public void GetTodoListTask_Should_Return_Populated_Dto_For_Period_Task()
    {
      var todoListPeriodTaskEntity = new TodoListPeriodTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Beginning = new DateTime(2022, 8, 22, 18, 0, 0),
        End = new DateTime(2022, 8, 22, 19, 0, 0),
      };

      var getTodoListTaskResponseDto = _todoListTaskService.GetTodoListTask(todoListPeriodTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      var getTodoListPeriodTaskResponseDto = getTodoListTaskResponseDto as GetTodoListPeriodTaskResponseDto;

      Assert.IsNotNull(getTodoListPeriodTaskResponseDto);

      Assert.AreEqual(todoListPeriodTaskEntity.Id, getTodoListPeriodTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListPeriodTaskEntity.TodoListId, getTodoListPeriodTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListPeriodTaskEntity.Title, getTodoListPeriodTaskResponseDto.Title);
      Assert.AreEqual(todoListPeriodTaskEntity.Description, getTodoListPeriodTaskResponseDto.Description);
      Assert.AreEqual(todoListPeriodTaskEntity.Completed, getTodoListPeriodTaskResponseDto.Completed);
      Assert.AreEqual(todoListPeriodTaskEntity.Beginning, getTodoListPeriodTaskResponseDto.Beginning);
      Assert.AreEqual(todoListPeriodTaskEntity.End, getTodoListPeriodTaskResponseDto.End);
    }

    [TestMethod]
    public void GetTodoListTask_Should_Throw_Exception()
    {
      var todoListPeriodTaskEntity = new TestTodoListTaskEntity();

      Assert.ThrowsException<NotSupportedException>(
        () => _todoListTaskService.GetTodoListTask(todoListPeriodTaskEntity));
    }

    [TestMethod]
    public async Task SearchTodoListTasksAsync_Should_Return_Populated_Dtos()
    {
      var todoListId = Guid.NewGuid();

      var todoListTaskEntity0 = new TodoListDayTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 8, 22),
      };

      var todoListTaskEntity1 = new TodoListDayTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = false,
        Date = new DateTime(2022, 8, 25),
      };

      var todoListTaskEntity2 = new TodoListPeriodTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Beginning = new DateTime(2022, 8, 23, 12, 0, 0),
        End = new DateTime(2022, 8, 24, 23, 0, 0),
      };

      var todoListTaskEntityCollection = new TodoListTaskEntityBase[]
      {
        todoListTaskEntity0,
        todoListTaskEntity1,
        todoListTaskEntity2,
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

      var searchTodoListTaskRecordResponseDto0 = searchTodoListTaskRecordResponseDtos.FirstOrDefault(dto => dto.TodoListTaskId == todoListTaskEntity0.Id) as SearchTodoListTasksDayRecordResponseDto;

      Assert.IsNotNull(searchTodoListTaskRecordResponseDto0);

      Assert.AreEqual(todoListTaskEntity0.TodoListId, searchTodoListTaskRecordResponseDto0.TodoListId);
      Assert.AreEqual(todoListTaskEntity0.Title, searchTodoListTaskRecordResponseDto0.Title);
      Assert.AreEqual(todoListTaskEntity0.Description, searchTodoListTaskRecordResponseDto0.Description);
      Assert.AreEqual(todoListTaskEntity0.Completed, searchTodoListTaskRecordResponseDto0.Completed);
      Assert.AreEqual(todoListTaskEntity0.Date, searchTodoListTaskRecordResponseDto0.Date);

      var searchTodoListTaskRecordResponseDto1 = searchTodoListTaskRecordResponseDtos.FirstOrDefault(dto => dto.TodoListTaskId == todoListTaskEntity1.Id) as SearchTodoListTasksDayRecordResponseDto;

      Assert.IsNotNull(searchTodoListTaskRecordResponseDto1);

      Assert.AreEqual(todoListTaskEntity1.TodoListId, searchTodoListTaskRecordResponseDto1.TodoListId);
      Assert.AreEqual(todoListTaskEntity1.Title, searchTodoListTaskRecordResponseDto1.Title);
      Assert.AreEqual(todoListTaskEntity1.Description, searchTodoListTaskRecordResponseDto1.Description);
      Assert.AreEqual(todoListTaskEntity1.Completed, searchTodoListTaskRecordResponseDto1.Completed);
      Assert.AreEqual(todoListTaskEntity1.Date, searchTodoListTaskRecordResponseDto1.Date);

      var searchTodoListTaskRecordResponseDto2 = searchTodoListTaskRecordResponseDtos.FirstOrDefault(dto => dto.TodoListTaskId == todoListTaskEntity2.Id) as SearchTodoListTasksPeriodRecordResponseDto;

      Assert.IsNotNull(searchTodoListTaskRecordResponseDto2);

      Assert.AreEqual(todoListTaskEntity2.TodoListId, searchTodoListTaskRecordResponseDto2.TodoListId);
      Assert.AreEqual(todoListTaskEntity2.Title, searchTodoListTaskRecordResponseDto2.Title);
      Assert.AreEqual(todoListTaskEntity2.Description, searchTodoListTaskRecordResponseDto2.Description);
      Assert.AreEqual(todoListTaskEntity2.Completed, searchTodoListTaskRecordResponseDto2.Completed);
      Assert.AreEqual(todoListTaskEntity2.Beginning, searchTodoListTaskRecordResponseDto2.Beginning);
      Assert.AreEqual(todoListTaskEntity2.End, searchTodoListTaskRecordResponseDto2.End);

      _todoListTaskEntityCollectionMock.Verify(collection => collection.GetDetachedTodoListTasksAsync(todoListId, _cancellationToken));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SearchTodoListTasksAsync_Should_Return_Throw_Exception()
    {
      var todoListTaskEntityCollection = new TodoListTaskEntityBase[]
      {
        new TodoListDayTaskEntity(),
        new TodoListDayTaskEntity(),
        new TodoListPeriodTaskEntity(),
        new TestTodoListTaskEntity(),
      };

      _todoListTaskEntityCollectionMock.Setup(collection => collection.GetDetachedTodoListTasksAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                       .ReturnsAsync(todoListTaskEntityCollection)
                                       .Verifiable();

      var todoListId = Guid.NewGuid();
      var query = new SearchTodoListTasksRequestDto
      {
        TodoListId = todoListId,
      };

      await Assert.ThrowsExceptionAsync<NotSupportedException>(
        () => _todoListTaskService.SearchTodoListTasksAsync(query, _cancellationToken));

      _todoListTaskEntityCollectionMock.Verify(collection => collection.GetDetachedTodoListTasksAsync(todoListId, _cancellationToken));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTaskAsync_Should_Save_Todo_List_Day_Task()
    {
      TodoListTaskEntityBase? todoListTaskEntity = null;

      _todoListTaskEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<TodoListTaskEntityBase>()))
                                       .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new AddTodoListDayTaskRequestDto();

      var addTodoListTaskResponseDto =
        await _todoListTaskService.AddTodoListTaskAsync(command, _cancellationToken);

      Assert.IsNotNull(addTodoListTaskResponseDto);
      Assert.IsNotNull(todoListTaskEntity);

      Assert.AreEqual(todoListTaskEntity.Id, addTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, addTodoListTaskResponseDto.TodoListId);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Add(todoListTaskEntity));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTaskAsync_Should_Save_Todo_List_Period_Task()
    {
      TodoListTaskEntityBase? todoListTaskEntity = null;

      _todoListTaskEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<TodoListTaskEntityBase>()))
                                       .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new AddTodoListPeriodTaskRequestDto();

      var addTodoListTaskResponseDto =
        await _todoListTaskService.AddTodoListTaskAsync(command, _cancellationToken);

      Assert.IsNotNull(addTodoListTaskResponseDto);
      Assert.IsNotNull(todoListTaskEntity);

      Assert.AreEqual(todoListTaskEntity.Id, addTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, addTodoListTaskResponseDto.TodoListId);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Add(todoListTaskEntity));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTaskAsync_Should_Throw_Exception()
    {
      var command = new TestAddTodoListTaskRequestDto();

      await Assert.ThrowsExceptionAsync<NotSupportedException>(
        () => _todoListTaskService.AddTodoListTaskAsync(command, _cancellationToken));

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

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
