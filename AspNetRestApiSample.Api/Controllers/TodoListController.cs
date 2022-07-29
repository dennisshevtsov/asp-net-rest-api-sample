// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using System;

  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Services;

  [ApiController]
  [Route("api/todo-list")]
  public sealed class TodoListController : ControllerBase
  {
    private readonly ITodoListService _todoListService;

    public TodoListController(ITodoListService todoListService)
    {
      _todoListService = todoListService ??
        throw new ArgumentNullException(nameof(todoListService));
    }

    [HttpGet("{todoListId}", Name = nameof(TodoListController.GetTodoList))]
    [Consumes("application/json")]
    public async Task<IActionResult> GetTodoList(
      [FromRoute] GetTodoListRequestDto query,
      CancellationToken cancellationToken)
    {
      var todoListEntity =
        await _todoListService.GetDetachedTodoListAsync(query, cancellationToken);

      if (todoListEntity == null)
      {
        return NotFound();
      }

      return Ok(_todoListService.GetTodoList(todoListEntity));
    }

    [HttpGet(Name = nameof(TodoListController.SearchTodoLists))]
    [Consumes("application/json")]
    public async Task<IActionResult> SearchTodoLists(
      [FromRoute] SearchTodoListsRequestDto query,
      CancellationToken cancellationToken)
    {
      return Ok(await _todoListService.SearchTodoListsAsync(query, cancellationToken));
    }

    [HttpPost("{todoListId}", Name = nameof(TodoListController.AddTodoList))]
    [Consumes("application/json")]
    public async Task<AddTodoListResponseDto> AddTodoList(
      [FromBody] AddTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      return await _todoListService.AddTodoListAsync(command, cancellationToken);
    }

    [HttpPut("{todoListId}", Name = nameof(TodoListController.UpdateTodoList))]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateTodoList(
      [FromBody] UpdateTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      var todoListEntity =
        await _todoListService.GetAttachedTodoListAsync(command, cancellationToken);

      if (todoListEntity == null)
      {
        return NotFound();
      }

      await _todoListService.UpdateTodoListAsync(command, todoListEntity, cancellationToken);

      return NoContent();
    }

    [HttpDelete("{todoListId}", Name = nameof(TodoListController.DeleteTodoList))]
    [Consumes("application/json")]
    public async Task DeleteTodoList(
      [FromRoute] DeleteTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      await _todoListService.DeleteTodoListAsync(command, cancellationToken);
    }
  }
}
