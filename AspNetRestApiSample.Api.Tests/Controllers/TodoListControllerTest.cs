// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Controllers
{
  using Moq;

  using AspNetRestApiSample.Api.Controllers;
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Services;
  using Microsoft.AspNetCore.Mvc;
  using AspNetRestApiSample.Api.Indentities;
  using AspNetRestApiSample.Api.Entities;

  [TestClass]
  public sealed class TodoListControllerTest
  {
    private Mock<ITodoListService> _todoListServiceMock;

    private TodoListController _todoListController;

    [TestInitialize]
    public void Initialize()
    {
      _todoListServiceMock = new Mock<ITodoListService>();
      _todoListController = new TodoListController(_todoListServiceMock.Object);
    }

    [TestMethod]
    public async Task GetTodoListTest_Should_Return_Not_Found()
    {
      _todoListServiceMock.Setup(service => service.GetDetachedTodoListAsync(It.IsAny<ITodoListIdentity>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(default(TodoListEntity))
                          .Verifiable();

      var actionResult = await _todoListController.GetTodoList(
        new GetTodoListRequestDto(), CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NotFoundResult);

      _todoListServiceMock.Verify();
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetTodoListTest_Should_Return_Ok()
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

      var actionResult = await _todoListController.GetTodoList(
        new GetTodoListRequestDto(), CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult) actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(getTodoListResponseDto, okObjectResult.Value);

      _todoListServiceMock.Verify();
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SearchTodoListsTest_Should_Return_Ok()
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

      var actionResult = await _todoListController.SearchTodoLists(
        new SearchTodoListsRequestDto(), CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
      Assert.AreEqual(searchTodoListsRecordResponseDtos, okObjectResult.Value);

      _todoListServiceMock.Verify();
      _todoListServiceMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListTest()
    {
      await _todoListController.AddTodoList(
        new AddTodoListRequestDto(), CancellationToken.None);
    }

    [TestMethod]
    public async Task UpdateTodoListTest()
    {
      await _todoListController.UpdateTodoList(
        new UpdateTodoListRequestDto(), CancellationToken.None);
    }

    [TestMethod]
    public async Task DeleteTodoListTest()
    {
      await _todoListController.DeleteTodoList(
        new DeleteTodoListRequestDto(), CancellationToken.None);
    }
  }
}
