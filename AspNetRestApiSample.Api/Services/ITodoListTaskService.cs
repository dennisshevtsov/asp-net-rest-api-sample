// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Provides a simple API to a storage of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</summary>
  public interface ITodoListTaskService
  {
    /// <summary>Gets a attached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntity?> GetAttachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity;

    /// <summary>Gets a detached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntity?> GetDetachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity;

    /// <summary>Gets a todo list task response DTO.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public GetTodoListTaskResponseDto GetTodoListTask(TodoListTaskEntity todoListTaskEntity);

    /// <summary>Gets a collection of TODO list tasks.</summary>
    /// <param name="query">An object that represents conditions to query TODO list tasks.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<SearchTodoListTasksRecordResponseDto[]> SearchTodoListTasksAsync(
      SearchTodoListTasksRequestDto query, CancellationToken cancellationToken);
  }
}
