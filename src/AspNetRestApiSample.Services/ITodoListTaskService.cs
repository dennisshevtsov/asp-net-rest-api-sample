// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Services
{
  using AspNetRestApiSample.Dtos;
  using AspNetRestApiSample.Database.Entities;
  using AspNetRestApiSample.Indentities;

  /// <summary>Provides a simple API to a storage of the <see cref="AspNetRestApiSample.Database.Entities.TodoListTaskEntityBase"/> class.</summary>
  public interface ITodoListTaskService
  {
    /// <summary>Gets a attached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Database.Entities.TodoListTaskEntityBase"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase?> GetAttachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity;

    /// <summary>Gets a detached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Database.Entities.TodoListTaskEntityBase"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase?> GetDetachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity;

    /// <summary>Gets a todo list task response DTO.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public GetTodoListTaskResponseDtoBase GetTodoListTask(TodoListTaskEntityBase todoListTaskEntity);

    /// <summary>Gets a collection of TODO list tasks.</summary>
    /// <param name="query">An object that represents conditions to query TODO list tasks.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<SearchTodoListTasksRecordResponseDtoBase[]> SearchTodoListTasksAsync(
      SearchTodoListTasksRequestDto query, CancellationToken cancellationToken);

    /// <summary>Adds a new task for a TODO list.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<AddTodoListTaskResponseDto> AddTodoListTaskAsync(
      AddTodoListTaskRequestDtoBase command, CancellationToken cancellationToken);

    /// <summary>Updates an existing TODO list task.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UpdateTodoListTaskAsync(
      UpdateTodoListTaskRequestDtoBase command,
      TodoListTaskEntityBase todoListTaskEntity,
      CancellationToken cancellationToken);

    /// <summary>Deletes a task from a TODO list.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task DeleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken);

    /// <summary>Makes a task completed.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CompleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken);

    /// <summary>Makes a task uncompleted.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UncompleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken);
  }
}
