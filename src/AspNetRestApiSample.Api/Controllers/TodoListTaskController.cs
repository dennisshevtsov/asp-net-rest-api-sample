// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using System;

  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Defaults;
  using AspNetRestApiSample.Dtos;
  using AspNetRestApiSample.Services;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(Routing.TodoListTaskRoute)]
  [Produces(ContentType.Json)]
  public sealed class TodoListTaskController : ControllerBase
  {
    private readonly ITodoListService _todoListService;
    private readonly ITodoListTaskService _todoListTaskService;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Controllers.TodoListTaskController"/> class.</summary>
    /// <param name="todoListService">An object that provides a simple API to a storage of instances of the <see cref="AspNetRestApiSample.Entities.TodoListEntity"/> class.</param>
    /// <param name="todoListTaskService">An object that provides a simple API to a storage of the <see cref="AspNetRestApiSample.Entities.TodoListTaskEntityBase"/> class.</param>
    public TodoListTaskController(ITodoListService todoListService, ITodoListTaskService todoListTaskService)
    {
      _todoListService = todoListService ?? throw new ArgumentNullException(nameof(todoListService));
      _todoListTaskService = todoListTaskService ?? throw new ArgumentNullException(nameof(todoListTaskService));
    }

    /// <summary>Handles the get todo list task query request.</summary>
    /// <param name="query">An object that represents conditions to query a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(Routing.GetTodoListTaskRoute, Name = nameof(TodoListTaskController.GetTodoListTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetTodoListTaskResponseDtoBase), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodoListTask(
      GetTodoListTaskRequestDto query,
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
    [ProducesResponseType(typeof(SearchTodoListTasksRecordResponseDtoBase[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchTodoListTasks(
      SearchTodoListTasksRequestDto query,
      CancellationToken cancellationToken)
    {
      return Ok(await _todoListTaskService.SearchTodoListTasksAsync(query, cancellationToken));
    }

    /// <summary>Handles the add a task to a todo list command request.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Name = nameof(TodoListTaskController.AddTodoListTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(AddTodoListTaskResponseDto), StatusCodes.Status201Created)]
    [Consumes(typeof(AddTodoListTaskRequestDtoBase), ContentType.Json)]
    public async Task<IActionResult> AddTodoListTask(
      AddTodoListTaskRequestDtoBase command,
      CancellationToken cancellationToken)
    {
      var todoListEntity = await _todoListService.GetDetachedTodoListAsync(command, cancellationToken);

      if (todoListEntity == null)
      {
        return NotFound();
      }

      var addTodoListTaskResponseDto = await _todoListTaskService.AddTodoListTaskAsync(command, cancellationToken);
      var getTodoListTaskRequestDto = new GetTodoListTaskRequestDto
      {
        TodoListId = addTodoListTaskResponseDto.TodoListId,
        TodoListTaskId = addTodoListTaskResponseDto.TodoListTaskId,
      };

      return CreatedAtAction(nameof(TodoListTaskController.GetTodoListTask), getTodoListTaskRequestDto, addTodoListTaskResponseDto);
    }

    /// <summary>Handles the update a todo list task command request.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPut(Routing.UpdateTodoListTaskRoute, Name = nameof(TodoListTaskController.UpdateTodoListTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(typeof(UpdateTodoListTaskRequestDtoBase), ContentType.Json)]
    public async Task<IActionResult> UpdateTodoListTask(
      UpdateTodoListTaskRequestDtoBase command,
      CancellationToken cancellationToken)
    {
      var todoListTaskEntity = await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(command, cancellationToken);

      if (todoListTaskEntity == null)
      {
        return NotFound();
      }

      await _todoListTaskService.UpdateTodoListTaskAsync(command, todoListTaskEntity, cancellationToken);

      return NoContent();
    }

    /// <summary>Handles the delete a todo list task command request.</summary>
    /// <param name="command">An object that represents data to delete a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpDelete(Routing.DeleteTodoListTaskRoute, Name = nameof(TodoListTaskController.DeleteTodoListTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTodoListTask(
      DeleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      var todoListTaskEntity = await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(command, cancellationToken);

      if (todoListTaskEntity == null)
      {
        return NotFound();
      }

      await _todoListTaskService.DeleteTodoListTaskAsync(todoListTaskEntity, cancellationToken);

      return NoContent();
    }

    /// <summary>Handles the complete a todo list task command request.</summary>
    /// <param name="command">An object that represents data to complete a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Routing.CompleteTodoListTaskRoute, Name = nameof(TodoListTaskController.CompleteTodoListTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CompleteTodoListTask(
      CompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      var todoListTaskEntity = await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(command, cancellationToken);

      if (todoListTaskEntity == null)
      {
        return NotFound();
      }

      await _todoListTaskService.CompleteTodoListTaskAsync(todoListTaskEntity, cancellationToken);

      return NoContent();
    }

    /// <summary>Handles the uncomplete a todo list task command request.</summary>
    /// <param name="command">An object that represents data to uncomplete a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Routing.UncompleteTodoListTaskRoute, Name = nameof(TodoListTaskController.UncompleteTodoListTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UncompleteTodoListTask(
      UncompleteTodoListTaskRequestDto command,
      CancellationToken cancellationToken)
    {
      var todoListTaskEntity = await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(command, cancellationToken);

      if (todoListTaskEntity == null)
      {
        return NotFound();
      }

      await _todoListTaskService.UncompleteTodoListTaskAsync(todoListTaskEntity, cancellationToken);

      return NoContent();
    }
  }
}
