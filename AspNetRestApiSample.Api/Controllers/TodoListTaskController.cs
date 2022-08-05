// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Dtos;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
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

    /// <summary>Handles the update a todo list task command request.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPut("task/{todoListTaskId}", Name = nameof(TodoListTaskController.UpdateTodoListTask))]
    [Consumes("application/json")]
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
    [HttpDelete("task/{todoListTaskId}", Name = nameof(TodoListTaskController.DeleteTodoListTask))]
    [Consumes("application/json")]
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
    [HttpPost("task/{todoListTaskId}/complete", Name = nameof(TodoListTaskController.CompleteTodoListTask))]
    [Consumes("application/json")]
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
    [HttpPost("task/{todoListTaskId}/uncomplete", Name = nameof(TodoListTaskController.UncompleteTodoListTask))]
    [Consumes("application/json")]
    public Task<IActionResult> UncompleteTodoListTask(
      [FromRoute] UncompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }
  }
}
