// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using System;
  using System.Net;

  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Defaults;
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Services;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route(TodoListTaskController.TodoListTaskRoute)]
  public sealed class TodoListTaskController : ControllerBase
  {
    private const string TodoListTaskRoute = "api/todo-list/{todoListId}/task";
    private const string GetTodoListTaskRoute = "{todoListTaskId}";
    private const string UpdateTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute;
    private const string DeleteTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute;
    private const string CompleteTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute + "/complete";
    private const string UncompleteTodoListTaskRoute = TodoListTaskController.GetTodoListTaskRoute + "/uncomplete";

    private readonly ITodoListService _todoListService;
    private readonly ITodoListTaskService _todoListTaskService;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Controllers.TodoListTaskController"/> class.</summary>
    /// <param name="todoListService">An object that provides a simple API to a storage of instances of the <see cref="AspNetRestApiSample.Api.Entities.TodoListEntity"/> class.</param>
    /// <param name="todoListTaskService">An object that provides a simple API to a storage of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntityBase"/> class.</param>
    public TodoListTaskController(ITodoListService todoListService, ITodoListTaskService todoListTaskService)
    {
      _todoListService = todoListService ?? throw new ArgumentNullException(nameof(todoListService));
      _todoListTaskService = todoListTaskService ?? throw new ArgumentNullException(nameof(todoListTaskService));
    }

    /// <summary>Handles the get todo list task query request.</summary>
    /// <param name="query">An object that represents conditions to query a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpGet(TodoListTaskController.GetTodoListTaskRoute, Name = nameof(TodoListTaskController.GetTodoListTask))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(GetTodoListTaskResponseDtoBase), (int)HttpStatusCode.OK, ContentType.Json)]
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
    [ProducesResponseType(typeof(SearchTodoListTasksRecordResponseDtoBase[]), (int)HttpStatusCode.OK, ContentType.Json)]
    public async Task<IActionResult> SearchTodoListTasks(
      [FromRoute] SearchTodoListTasksRequestDto query,
      CancellationToken cancellationToken)
    {
      return Ok(await _todoListTaskService.SearchTodoListTasksAsync(query, cancellationToken));
    }

    /// <summary>Handles the add a task to a todo list command request.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPost(Name = nameof(TodoListTaskController.AddTodoListTask))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(AddTodoListTaskResponseDto), (int)HttpStatusCode.OK, ContentType.Json)]
    [Consumes(typeof(AddTodoListTaskRequestDtoBase), ContentType.Json)]
    public async Task<IActionResult> AddTodoListTask(
      [FromBody] AddTodoListTaskRequestDtoBase command,
      CancellationToken cancellationToken)
    {
      var todoListEntity = await _todoListService.GetDetachedTodoListAsync(command, cancellationToken);

      if (todoListEntity == null)
      {
        return NotFound();
      }

      return Ok(await _todoListTaskService.AddTodoListTaskAsync(command, cancellationToken));
    }

    /// <summary>Handles the update a todo list task command request.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    [HttpPut(TodoListTaskController.UpdateTodoListTaskRoute, Name = nameof(TodoListTaskController.UpdateTodoListTask))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [Consumes(typeof(UpdateTodoListTaskRequestDtoBase), ContentType.Json)]
    public async Task<IActionResult> UpdateTodoListTask(
      [FromBody] UpdateTodoListTaskRequestDtoBase command,
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
    [HttpDelete(TodoListTaskController.DeleteTodoListTaskRoute, Name = nameof(TodoListTaskController.DeleteTodoListTask))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteTodoListTask(
      [FromRoute] DeleteTodoListTaskRequestDto command,
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
    [HttpPost(TodoListTaskController.CompleteTodoListTaskRoute, Name = nameof(TodoListTaskController.CompleteTodoListTask))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CompleteTodoListTask(
      [FromRoute] CompleteTodoListTaskRequestDto command,
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
    [HttpPost(TodoListTaskController.UncompleteTodoListTaskRoute, Name = nameof(TodoListTaskController.UncompleteTodoListTask))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UncompleteTodoListTask(
      [FromRoute] UncompleteTodoListTaskRequestDto command,
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
