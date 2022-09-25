// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using System;

  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Defaults;
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Services;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(Routing.TodoListRoute)]
  [Produces(ContentType.Json)]
  public sealed class TodoListController : ControllerBase
  {
    private readonly ITodoListService _todoListService;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Controllers.TodoListController"/> class.</summary>
    /// <param name="todoListService">An object that provides a simple API to a storage of instances of the <see cref="AspNetRestApiSample.Api.Entities.TodoListEntity"/> class.</param>
    public TodoListController(ITodoListService todoListService)
    {
      _todoListService = todoListService ??
        throw new ArgumentNullException(nameof(todoListService));
    }

    /// <summary>Handles the get todo list query request.</summary>
    /// <param name="query">An object that represents conditions to query a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Routing.GetTodoListRoute, Name = nameof(TodoListController.GetTodoList))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetTodoListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodoList(
      GetTodoListRequestDto query,
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

    /// <summary>Handles the search todo lists query request.</summary>
    /// <param name="query">An object that represents conditions to query todo lists.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Name = nameof(TodoListController.SearchTodoLists))]
    [ProducesResponseType(typeof(SearchTodoListsRecordResponseDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchTodoLists(
      SearchTodoListsRequestDto query,
      CancellationToken cancellationToken)
    {
      return Ok(await _todoListService.SearchTodoListsAsync(query, cancellationToken));
    }

    /// <summary>Handles the add todo list command request.</summary>
    /// <param name="command">An object that represents data to create a new todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Name = nameof(TodoListController.AddTodoList))]
    [ProducesResponseType(typeof(AddTodoListResponseDto), StatusCodes.Status201Created)]
    [Consumes(typeof(AddTodoListRequestDto), ContentType.Json)]
    public async Task<IActionResult> AddTodoList(
      AddTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      var addTodoListResponseDto = await _todoListService.AddTodoListAsync(command, cancellationToken);
      var getTodoListRequestDto = new GetTodoListRequestDto
      {
        TodoListId = addTodoListResponseDto.TodoListId,
      };

      return CreatedAtAction(nameof(TodoListController.GetTodoList), getTodoListRequestDto, addTodoListResponseDto);
    }

    /// <summary>Handles the update todo list command request.</summary>
    /// <param name="command">An object that represents data to update a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPut(Routing.UpdateTodoListRoute, Name = nameof(TodoListController.UpdateTodoList))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes(typeof(UpdateTodoListRequestDto), ContentType.Json)]
    public async Task<IActionResult> UpdateTodoList(
      UpdateTodoListRequestDto command,
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

    /// <summary>Handles the delete todo list command request.</summary>
    /// <param name="command">An object that represents data to delete a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpDelete(Routing.DeleteTodoListRoute, Name = nameof(TodoListController.DeleteTodoList))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTodoList(
      DeleteTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      var todoListEntity =
        await _todoListService.GetAttachedTodoListAsync(command, cancellationToken);

      if (todoListEntity == null)
      {
        return NotFound();
      }

      await _todoListService.DeleteTodoListAsync(todoListEntity, cancellationToken);

      return NoContent();
    }
  }
}
