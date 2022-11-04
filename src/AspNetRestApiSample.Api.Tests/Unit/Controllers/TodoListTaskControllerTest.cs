// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using Moq;

  using AspNetRestApiSample.Api.Controllers;
  using AspNetRestApiSample.Indentities;

  [TestClass]
  public sealed class TodoListTaskControllerTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<ITodoListService> _todoListServiceMock;
    private Mock<ITodoListTaskService> _todoListTaskServiceMock;
    private TodoListTaskController _todoListTaskController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _todoListServiceMock = new Mock<ITodoListService>();
      _todoListTaskServiceMock = new Mock<ITodoListTaskService>();

      _todoListTaskController = new TodoListTaskController(
        _todoListServiceMock.Object,
        _todoListTaskServiceMock.Object);
    }

    [TestMethod]
    public async Task GetTodoListTask_Should_Return_Not_Found()
    {
      _todoListTaskServiceMock.Setup(service => service.GetDetachedTodoListTaskEntityAsync(It.IsAny<GetTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(TodoListTaskEntityBase))
                              .Verifiable();

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };

      var actionResult = await _todoListTaskController.GetTodoListTask(query, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NotFoundResult);

      _todoListTaskServiceMock.Verify(service => service.GetDetachedTodoListTaskEntityAsync(query, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetTodoListTask_Should_Return_Ok()
    {
      var todoListTaskId = Guid.NewGuid();
      var todoListId = Guid.NewGuid();
      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetDetachedTodoListTaskEntityAsync(It.IsAny<GetTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(todoListTaskEntity)
                              .Verifiable();

      var getTodoListTaskResponseDto = new GetTodoListDayTaskResponseDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetTodoListTask(It.IsAny<TodoListTaskEntityBase>()))
                              .Returns(getTodoListTaskResponseDto)
                              .Verifiable();

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actionResult = await _todoListTaskController.GetTodoListTask(query, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(getTodoListTaskResponseDto, okObjectResult.Value);

      _todoListTaskServiceMock.Verify(service => service.GetDetachedTodoListTaskEntityAsync(query, _cancellationToken));
      _todoListTaskServiceMock.Verify(service => service.GetTodoListTask(todoListTaskEntity));
      _todoListTaskServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SearchTodoListTasks_Should_Return_Ok()
    {
      var todoListId = Guid.NewGuid();
      var searchTodoListTasksRecordResponseDtos = new SearchTodoListTasksRecordResponseDtoBase[]
      {
        new SearchTodoListTasksDayRecordResponseDto { TodoListId = todoListId, TodoListTaskId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), Date = new DateTime(2022, 08, 22).Ticks, },
        new SearchTodoListTasksDayRecordResponseDto { TodoListId = todoListId, TodoListTaskId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), Date = new DateTime(2022, 08, 23).Ticks, },
        new SearchTodoListTasksPeriodRecordResponseDto { TodoListId = todoListId, TodoListTaskId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), Begin = new DateTime(2022, 08, 23, 12, 0, 0).Ticks, End = new DateTime(2022, 08, 22, 13, 0, 0).Ticks, },
      };

      _todoListTaskServiceMock.Setup(service => service.SearchTodoListTasksAsync(It.IsAny<SearchTodoListTasksRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(searchTodoListTasksRecordResponseDtos)
                              .Verifiable();

      var query = new SearchTodoListTasksRequestDto();

      var actionResult = await _todoListTaskController.SearchTodoListTasks(query, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(searchTodoListTasksRecordResponseDtos, okObjectResult.Value);

      _todoListTaskServiceMock.Verify(service => service.SearchTodoListTasksAsync(query, CancellationToken.None));
      _todoListTaskServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTask_Should_Return_Not_Found()
    {
      _todoListServiceMock.Setup(service => service.GetDetachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(default(TodoListEntity))
                          .Verifiable();

      var command = new AddTodoListDayTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListTaskController.AddTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task AddTodoListTask_Should_Return_Created()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        Id = todoListId,
      };

      _todoListServiceMock.Setup(service => service.GetDetachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(todoListEntity)
                          .Verifiable();

      var todoListTaskId = Guid.NewGuid();

      _todoListTaskServiceMock.Setup(service => service.AddTodoListTaskAsync(It.IsAny<AddTodoListTaskRequestDtoBase>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new AddTodoListTaskResponseDto
                              {
                                TodoListId = todoListId,
                                TodoListTaskId = todoListTaskId,
                              })
                              .Verifiable();

      var command = new AddTodoListDayTaskRequestDto
      {
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListTaskController.AddTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);

      var createdAtActionResult = actionResult as CreatedAtActionResult;

      Assert.IsNotNull(createdAtActionResult);
      Assert.IsNotNull(createdAtActionResult.Value);

      var addTodoListTaskResponseDto = createdAtActionResult.Value as AddTodoListTaskResponseDto;

      Assert.IsNotNull(addTodoListTaskResponseDto);
      Assert.AreEqual(todoListId, addTodoListTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListTaskId, addTodoListTaskResponseDto.TodoListTaskId);
    }

    [TestMethod]
    public async Task UpdateTodoListTask_Should_Return_Not_Found()
    {
      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<UpdateTodoListTaskRequestDtoBase>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(TodoListTaskEntityBase))
                              .Verifiable();

      UpdateTodoListTaskRequestDtoBase command = new UpdateTodoListDayTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListTaskController.UpdateTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoListTask_Should_Return_No_Content()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<UpdateTodoListTaskRequestDtoBase>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(todoListTaskEntity)
                              .Verifiable();

      _todoListTaskServiceMock.Setup(service => service.UpdateTodoListTaskAsync(It.IsAny<UpdateTodoListTaskRequestDtoBase>(), It.IsAny<TodoListTaskEntityBase>(), It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

      UpdateTodoListTaskRequestDtoBase command = new UpdateTodoListDayTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListTaskController.UpdateTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.Verify(service => service.UpdateTodoListTaskAsync(command, todoListTaskEntity, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task DeleteTodoListTask_Should_Return_Not_Found()
    {
      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<DeleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(TodoListTaskEntityBase))
                              .Verifiable();

      var command = new DeleteTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };

      var actionResult = await _todoListTaskController.DeleteTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.Verify();
    }

    [TestMethod]
    public async Task DeleteTodoListTask_Should_Return_No_Content()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<DeleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(todoListTaskEntity)
                              .Verifiable();

      _todoListTaskServiceMock.Setup(service => service.DeleteTodoListTaskAsync(It.IsAny<TodoListTaskEntityBase>(), It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

      var command = new DeleteTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actionResult = await _todoListTaskController.DeleteTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.Verify(service => service.DeleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.Verify();
    }

    [TestMethod]
    public async Task CompleteTodoListTask_Should_Return_Not_Found()
    {
      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<CompleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(TodoListTaskEntityBase))
                              .Verifiable();

      var command = new CompleteTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };

      var actionResult = await _todoListTaskController.CompleteTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task CompleteTodoListTask_Should_Return_No_Content()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<CompleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(todoListTaskEntity)
                              .Verifiable();

      _todoListTaskServiceMock.Setup(service => service.CompleteTodoListTaskAsync(It.IsAny<TodoListTaskEntityBase>(), It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

      var command = new CompleteTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actionResult = await _todoListTaskController.CompleteTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.Verify(service => service.CompleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UncompleteTodoListTask_Should_Return_Not_Found()
    {
      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<UncompleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(TodoListTaskEntityBase))
                              .Verifiable();

      var command = new UncompleteTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };

      var actionResult = await _todoListTaskController.UncompleteTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UncompleteTodoListTask_Should_Return_No_Content()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetAttachedTodoListTaskEntityAsync(It.IsAny<UncompleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(todoListTaskEntity)
                              .Verifiable();

      _todoListTaskServiceMock.Setup(service => service.CompleteTodoListTaskAsync(It.IsAny<TodoListTaskEntityBase>(), It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

      var command = new UncompleteTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actionResult = await _todoListTaskController.UncompleteTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

      _todoListTaskServiceMock.Verify(service => service.GetAttachedTodoListTaskEntityAsync(command, _cancellationToken));
      _todoListTaskServiceMock.Verify(service => service.UncompleteTodoListTaskAsync(todoListTaskEntity, _cancellationToken));
      _todoListTaskServiceMock.VerifyNoOtherCalls();

      _todoListServiceMock.VerifyNoOtherCalls();
    }
  }
}
