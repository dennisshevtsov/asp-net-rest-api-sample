﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
    private Mock<IEntityDatabase> _entityDatabaseMock;

    private TodoListTaskService _todoListTaskService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _todoListEntityCollectionMock = new Mock<ITodoListEntityCollection>();
      _todoListTaskEntityCollectionMock = new Mock<ITodoListTaskEntityCollection>();
      _entityDatabaseMock = new Mock<IEntityDatabase>();

      _entityDatabaseMock.SetupGet(database => database.TodoLists)
                         .Returns(_todoListEntityCollectionMock.Object);

      _entityDatabaseMock.SetupGet(database => database.TodoListTasks)
                         .Returns(_todoListTaskEntityCollectionMock.Object);

      _todoListTaskService = new TodoListTaskService(_entityDatabaseMock.Object);
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
    public void GetTodoListTask_Should_Return_Populated_Dto()
    {
      var todoListTaskEntity = new TodoListDayTaskEntity
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
        new TodoListDayTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = todoListId,
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListDayTaskEntity
        {
          Id = Guid.NewGuid(),
          TodoListId = todoListId,
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListDayTaskEntity
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

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTaskAsync_Should_Save_Todo_List_Task()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      _todoListTaskEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<AddTodoListTaskRequestDto>()))
                                       .Returns(todoListTaskEntity)
                                       .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new AddTodoListTaskRequestDto();

      var addTodoListTaskResponseDto =
        await _todoListTaskService.AddTodoListTaskAsync(command, _cancellationToken);

      Assert.IsNotNull(addTodoListTaskResponseDto);

      Assert.AreEqual(todoListTaskEntity.Id, addTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, addTodoListTaskResponseDto.TodoListId);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Add(command));
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoListTaskAsync_Should_Save_Todo_List_Task()
    {
      _todoListTaskEntityCollectionMock.Setup(collection => collection.Update(It.IsAny<UpdateTodoListTaskRequestDto>(), It.IsAny<TodoListTaskEntityBase>()))
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

      var command = new UpdateTodoListTaskRequestDto();

      await _todoListTaskService.UpdateTodoListTaskAsync(command, todoListTaskEntity, _cancellationToken);

      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.Verify(collection => collection.Update(command, todoListTaskEntity));
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
  }
}
