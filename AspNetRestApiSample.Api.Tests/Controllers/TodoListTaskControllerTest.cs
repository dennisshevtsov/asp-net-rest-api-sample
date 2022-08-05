// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Controllers;

  [TestClass]
  public sealed class TodoListTaskControllerTest
  {
#pragma warning disable CS8618
    private TodoListTaskController _todoListTaskController;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _todoListTaskController = new TodoListTaskController();
    }

    [TestMethod]
    public async Task GetTodoListTask_Should_Return_Not_Found()
    {
      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };

      var actionResult = await _todoListTaskController.GetTodoListTask(query, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is NotFoundResult);
    }

    [TestMethod]
    public async Task GetTodoListTask_Should_Return_Ok()
    {
      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
      };

      var actionResult = await _todoListTaskController.GetTodoListTask(query, CancellationToken.None);

      Assert.IsNotNull(actionResult);
      Assert.IsTrue(actionResult is OkObjectResult);

      var okObjectResult = (OkObjectResult)actionResult;

      Assert.IsNotNull(okObjectResult.Value);
    }
  }
}
