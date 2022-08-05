// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using Moq;

  using AspNetRestApiSample.Api.Controllers;
  using AspNetRestApiSample.Api.Indentities;

  [TestClass]
  public sealed class TodoListControllerTest
  {
#pragma warning disable CS8618
    private Mock<ITodoListService> _todoListServiceMock;
    private TodoListController _todoListController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _todoListServiceMock = new Mock<ITodoListService>();
      _todoListController = new TodoListController(_todoListServiceMock.Object);
    }

    [TestMethod]
    public async Task GetTodoList_Should_Return_Not_Found()
    {
      _todoListServiceMock.Setup(service => service.GetDetachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(default(TodoListEntity))
                          .Verifiable();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actionResult = await _todoListController.GetTodoList(query, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NotFoundResult);

      _todoListServiceMock.Verify(service => service.GetDetachedTodoListAsync(query, CancellationToken.None));
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetTodoList_Should_Return_Ok()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _todoListServiceMock.Setup(service => service.GetDetachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(todoListEntity)
                          .Verifiable();

      var getTodoListResponseDto = new GetTodoListResponseDto
      {
        TodoListId = todoListEntity.TodoListId,
        Title = todoListEntity.Title,
        Description = todoListEntity.Description,
      };

      _todoListServiceMock.Setup(service => service.GetTodoList(It.IsAny<TodoListEntity>()))
                          .Returns(getTodoListResponseDto)
                          .Verifiable();

      var query = new GetTodoListRequestDto
      {
        TodoListId = todoListId,
      };

      var actionResult = await _todoListController.GetTodoList(query, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(getTodoListResponseDto, okObjectResult.Value);

      _todoListServiceMock.Verify(service => service.GetDetachedTodoListAsync(query, CancellationToken.None));
      _todoListServiceMock.Verify(service => service.GetTodoList(todoListEntity));

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SearchTodoLists_Should_Return_Ok()
    {
      var searchTodoListsRecordResponseDtos = new[]
      {
        new SearchTodoListsRecordResponseDto { TodoListId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), },
        new SearchTodoListsRecordResponseDto { TodoListId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), },
        new SearchTodoListsRecordResponseDto { TodoListId = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString(), },
      };

      _todoListServiceMock.Setup(service => service.SearchTodoListsAsync(It.IsAny<SearchTodoListsRequestDto>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(searchTodoListsRecordResponseDtos)
                          .Verifiable();

      var query = new SearchTodoListsRequestDto();

      var actionResult = await _todoListController.SearchTodoLists(query, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(searchTodoListsRecordResponseDtos, okObjectResult.Value);

      _todoListServiceMock.Verify(service => service.SearchTodoListsAsync(query, CancellationToken.None));
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoList_Should_Return_Ok()
    {
      var addTodoListResponseDto = new AddTodoListResponseDto
      {
        TodoListId = Guid.NewGuid(),
      };

      _todoListServiceMock.Setup(service => service.AddTodoListAsync(It.IsAny<AddTodoListRequestDto>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(addTodoListResponseDto)
                          .Verifiable();

      var command = new AddTodoListRequestDto
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListController.AddTodoList(command, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(addTodoListResponseDto, okObjectResult.Value);

      _todoListServiceMock.Verify(service => service.AddTodoListAsync(command, CancellationToken.None));
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoList_Should_Return_Not_Found()
    {
      _todoListServiceMock.Setup(service => service.GetAttachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(default(TodoListEntity))
                          .Verifiable();

      var command = new UpdateTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListController.UpdateTodoList(command, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NotFoundResult);

      _todoListServiceMock.Verify(service => service.GetAttachedTodoListAsync(command, CancellationToken.None));
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoList_Should_Return_No_Content()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _todoListServiceMock.Setup(service => service.GetAttachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(todoListEntity)
                          .Verifiable();

      _todoListServiceMock.Setup(service => service.UpdateTodoListAsync(It.IsAny<UpdateTodoListRequestDto>(), It.IsAny<TodoListEntity>(), It.IsAny<CancellationToken>()))
                          .Returns(Task.CompletedTask)
                          .Verifiable();

      var command = new UpdateTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var actionResult = await _todoListController.UpdateTodoList(command, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NoContentResult);

      _todoListServiceMock.Verify(service => service.GetAttachedTodoListAsync(command, CancellationToken.None));
      _todoListServiceMock.Verify(service => service.UpdateTodoListAsync(command, todoListEntity, CancellationToken.None));

      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task DeleteTodoList_Should_Return_Not_Found()
    {
      _todoListServiceMock.Setup(service => service.GetAttachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(default(TodoListEntity))
                          .Verifiable();

      var command = new DeleteTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actionResult = await _todoListController.DeleteTodoList(command, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NotFoundResult);

      _todoListServiceMock.Verify(service => service.GetAttachedTodoListAsync(command, CancellationToken.None));
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task DeleteTodoList_Should_Return_No_Content()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      _todoListServiceMock.Setup(service => service.GetAttachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(todoListEntity)
                          .Verifiable();

      _todoListServiceMock.Setup(service => service.DeleteTodoListAsync(It.IsAny<TodoListEntity>(), It.IsAny<CancellationToken>()))
                          .Returns(Task.CompletedTask)
                          .Verifiable();

      var command = new DeleteTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actionResult = await _todoListController.DeleteTodoList(command, CancellationToken.None);

      _todoListServiceMock.Verify(service => service.GetAttachedTodoListAsync(command, CancellationToken.None));
      _todoListServiceMock.Verify(service => service.DeleteTodoListAsync(todoListEntity, CancellationToken.None));

      _todoListServiceMock.VerifyNoOtherCalls();
    }
  }
}
