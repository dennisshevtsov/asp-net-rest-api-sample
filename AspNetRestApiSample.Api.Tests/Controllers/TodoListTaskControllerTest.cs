﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using Moq;

  using AspNetRestApiSample.Api.Controllers;
  using AspNetRestApiSample.Api.Indentities;

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
                              .ReturnsAsync(default(TodoListTaskEntity))
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
      var todoListTaskEntity = new TodoListTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetDetachedTodoListTaskEntityAsync(It.IsAny<GetTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(todoListTaskEntity)
                              .Verifiable();

      var getTodoListTaskResponseDto = new GetTodoListTaskResponseDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      _todoListTaskServiceMock.Setup(service => service.GetTodoListTask(It.IsAny<TodoListTaskEntity>()))
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
      var searchTodoListTasksRecordResponseDtos = new[]
      {
        new SearchTodoListTasksRecordResponseDto { TodoListId = todoListId, TodoListTaskId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), },
        new SearchTodoListTasksRecordResponseDto { TodoListId = todoListId, TodoListTaskId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), },
        new SearchTodoListTasksRecordResponseDto { TodoListId = todoListId, TodoListTaskId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), },
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

      var command = new AddTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListTaskController.AddTodoListTask(command, _cancellationToken);

      Assert.IsNotNull(actionResult);
      Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }
  }
}
