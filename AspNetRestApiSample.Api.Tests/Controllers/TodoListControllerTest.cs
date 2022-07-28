// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Controllers
{
  using Moq;

  using AspNetRestApiSample.Api.Controllers;
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Services;

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
    public async Task GetTodoListTest()
    {
      await _todoListController.GetTodoList(
        new GetTodoListRequestDto(), CancellationToken.None);
    }

    [TestMethod]
    public async Task SearchTodoListsTest()
    {
      await _todoListController.SearchTodoLists(
        new SearchTodoListsRequestDto(), CancellationToken.None);
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
