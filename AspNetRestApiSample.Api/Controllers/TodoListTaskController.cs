// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Dtos;

  [ApiController]
  [Route("api/todo-list/{todoListId}")]
  public sealed class TodoListTaskController : ControllerBase
  {
    /// <summary>Handles the get todo list task query request.</summary>
    /// <param name="query">An object that represents conditions to query a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet("task/{todoListTaskId}", Name = nameof(TodoListTaskController.GetTodoListTask))]
    [Consumes("application/json")]
    public Task<IActionResult> GetTodoListTask(
      [FromRoute] GetTodoListTaskRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    /// <summary>Handles the search todo list tasks query request.</summary>
    /// <param name="query">An object that represents conditions to query todo list tasks.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet("task", Name = nameof(TodoListTaskController.SearchTodoListTasks))]
    [Consumes("application/json")]
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
    [HttpPost("task", Name = nameof(TodoListTaskController.AddTodoListTask))]
    [Consumes("application/json")]
    public Task<IActionResult> AddTodoListTask(
      [FromBody] AddTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    public Task<IActionResult> UpdateTodoListTask(
      [FromBody] UpdateTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    public Task<IActionResult> DeleteTodoListTask(
      [FromRoute] DeleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    public Task<IActionResult> CompleteTodoListTask(
      [FromRoute] CompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    public Task<IActionResult> UncompleteTodoListTask(
      [FromRoute] UncompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }
  }
}
