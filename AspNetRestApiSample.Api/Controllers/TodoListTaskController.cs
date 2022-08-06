// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using System;

  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Services;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(TodoListTaskController.TodoListTaskRoute)]
  public sealed class TodoListTaskController : ControllerBase
  {
    private const string ContentType = "application/json";

    private const string TodoListTaskRoute = "api/todo-list/{todoListId}/task";
    private const string GetTodoListTaskRoute = "{todoListTaskId}";
    private const string UpdateTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute;
    private const string DeleteTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute;
    private const string CompleteTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute + "/complete";
    private const string UncompleteTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute + "/uncomplete";

    private readonly ITodoListTaskService _todoListTaskService;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Controllers.TodoListTaskController"/> class.</summary>
    /// <param name="todoListTaskService">An object that provides a simple API to a storage of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</param>
    public TodoListTaskController(ITodoListTaskService todoListTaskService)
    {
      _todoListTaskService = todoListTaskService ?? throw new ArgumentNullException(nameof(todoListTaskService));
    }

    /// <summary>Handles the get todo list task query request.</summary>
    /// <param name="query">An object that represents conditions to query a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(TodoListTaskController.GetTodoListTaskRoute, Name = nameof(TodoListTaskController.GetTodoListTask))]
    [Consumes(TodoListTaskController.ContentType)]
    public async Task<IActionResult> GetTodoListTask(
      [FromRoute] GetTodoListTaskRequestDto query,
      CancellationToken cancellationToken)
    {
      var todoListTaskEntity = await _todoListTaskService.GetDetachedTodoListTaskEntityAsync(
        query, cancellationToken);

      if (todoListTaskEntity == null)
      {
        return NotFound();
      }

      return Ok(_todoListTaskService.GetTodoListTask(todoListTaskEntity));
    }

    /// <summary>Handles the search todo list tasks query request.</summary>
    /// <param name="query">An object that represents conditions to query todo list tasks.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Name = nameof(TodoListTaskController.SearchTodoListTasks))]
    [Consumes(TodoListTaskController.ContentType)]
    public Task<IActionResult> SearchTodoListTasks(
      [FromRoute] SearchTodoListTasksRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Handles the add a task to a todo list command request.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Name = nameof(TodoListTaskController.AddTodoListTask))]
    [Consumes(TodoListTaskController.ContentType)]
    public Task<IActionResult> AddTodoListTask(
      [FromBody] AddTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Handles the update a todo list task command request.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPut(TodoListTaskController.UpdateTodoListTaskRoute, Name = nameof(TodoListTaskController.UpdateTodoListTask))]
    [Consumes(TodoListTaskController.ContentType)]
    public Task<IActionResult> UpdateTodoListTask(
      [FromBody] UpdateTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Handles the delete a todo list task command request.</summary>
    /// <param name="command">An object that represents data to delete a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpDelete(TodoListTaskController.DeleteTodoListTaskRoute, Name = nameof(TodoListTaskController.DeleteTodoListTask))]
    [Consumes(TodoListTaskController.ContentType)]
    public Task<IActionResult> DeleteTodoListTask(
      [FromRoute] DeleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Handles the delete a todo list task command request.</summary>
    /// <param name="command">An object that represents data to complete a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(TodoListTaskController.CompleteTodoListTaskRoute, Name = nameof(TodoListTaskController.CompleteTodoListTask))]
    [Consumes(TodoListTaskController.ContentType)]
    public Task<IActionResult> CompleteTodoListTask(
      [FromRoute] CompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Handles the delete a todo list task command request.</summary>
    /// <param name="command">An object that represents data to uncomplete a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(TodoListTaskController.UncompleteTodoListTaskRoute, Name = nameof(TodoListTaskController.UncompleteTodoListTask))]
    [Consumes(TodoListTaskController.ContentType)]
    public Task<IActionResult> UncompleteTodoListTask(
      [FromRoute] UncompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }
  }
}
