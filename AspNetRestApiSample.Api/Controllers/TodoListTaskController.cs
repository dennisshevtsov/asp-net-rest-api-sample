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
    public Task<IActionResult> GetTodoListTask(
      [FromRoute] GetTodoListTaskRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

    public Task<IActionResult> SearchTodoListTasks(
      [FromRoute] SearchTodoListTasksRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.FromResult<IActionResult>(Ok());
    }

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
