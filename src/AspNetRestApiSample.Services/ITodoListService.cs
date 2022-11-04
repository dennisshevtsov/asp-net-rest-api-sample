// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Services
{
  using AspNetRestApiSample.Dtos;
  using AspNetRestApiSample.Entities;
  using AspNetRestApiSample.Indentities;

  /// <summary>Provides a simple API to a storage of instances of the <see cref="AspNetRestApiSample.Entities.TodoListEntity"/> class.</summary>
  public interface ITodoListService
  {
    /// <summary>Gets a detached todo list entity.</summary>
    /// <param name="query">An object that represents an identity of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity?> GetDetachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken);

    /// <summary>Gets an attached todo list entity.</summary>
    /// <param name="query">An object that represents an identity of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity?> GetAttachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken);

    /// <summary>Gets a todo list response DTO.</summary>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public GetTodoListResponseDto GetTodoList(TodoListEntity todoListEntity);

    /// <summary>Gets a collection of todo lists that satisfy provided conditions.</summary>
    /// <param name="query">An object that represents conditions to query todo lists.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<SearchTodoListsRecordResponseDto[]> SearchTodoListsAsync(
      SearchTodoListsRequestDto query, CancellationToken cancellationToken);

    /// <summary>Creates a new todo list.</summary>
    /// <param name="command">An object that represents data to create a new todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<AddTodoListResponseDto> AddTodoListAsync(
      AddTodoListRequestDto command, CancellationToken cancellationToken);

    /// <summary>Updates an existing todo list.</summary>
    /// <param name="command">An object that represents data to update a todo list.</param>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListEntity todoListEntity,
      CancellationToken cancellationToken);

    /// <summary>Deletes an existing todo list.</summary>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task DeleteTodoListAsync(
      TodoListEntity todoListEntity, CancellationToken cancellationToken);
  }
}
