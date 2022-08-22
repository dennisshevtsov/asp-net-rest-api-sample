// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;
  using AspNetRestApiSample.Api.Storage;

  /// <summary>Provides a simple API to a storage of instances of the <see cref="AspNetRestApiSample.Api.Entities.TodoListEntity"/> class.</summary>
  public sealed class TodoListService : ITodoListService
  {
    private readonly IEntityDatabase _entityDatabase;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Services.TodoListService"/> class.</summary>
    /// <param name="entityDatabase">An object that provides a simple API to a database.</param>
    public TodoListService(IEntityDatabase entityDatabase)
    {
      _entityDatabase = entityDatabase ?? throw new ArgumentNullException(nameof(entityDatabase));
    }

    /// <summary>Gets a detached todo list entity.</summary>
    /// <param name="query">An object that represents an identity of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity?> GetDetachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken)
      => _entityDatabase.TodoLists.GetDetachedAsync(query.TodoListId, query.TodoListId, cancellationToken);

    /// <summary>Gets an attached todo list entity.</summary>
    /// <param name="query">An object that represents an identity of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity?> GetAttachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken)
      => _entityDatabase.TodoLists.GetAttachedAsync(query.TodoListId, query.TodoListId, cancellationToken);

    /// <summary>Gets a todo list response DTO.</summary>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public GetTodoListResponseDto GetTodoList(TodoListEntity todoListEntity)
      => new GetTodoListResponseDto
      {
        TodoListId = todoListEntity.TodoListId,
        Title = todoListEntity.Title,
        Description = todoListEntity.Description,
      };

    /// <summary>Gets a collection of todo lists that satisfy provided conditions.</summary>
    /// <param name="query">An object that represents conditions to query todo lists.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<SearchTodoListsRecordResponseDto[]> SearchTodoListsAsync(
      SearchTodoListsRequestDto query, CancellationToken cancellationToken)
    {
      var todoListTaskEntities = await _entityDatabase.TodoLists.GetDetachedTodoListsAsync(cancellationToken);
      var searchTodoListsRecordResponseDtos = new SearchTodoListsRecordResponseDto[todoListTaskEntities.Length];

      for (var i = 0; i < todoListTaskEntities.Length; ++i)
      {
        var todoListTaskEntity = todoListTaskEntities[i];

        searchTodoListsRecordResponseDtos[i] = new SearchTodoListsRecordResponseDto
        {
          TodoListId = todoListTaskEntity.TodoListId,
          Title = todoListTaskEntity.Title,
          Description = todoListTaskEntity.Description,
        };
      }

      return searchTodoListsRecordResponseDtos;
    }

    /// <summary>Creates a new todo list.</summary>
    /// <param name="command">An object that represents data to create a new todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<AddTodoListResponseDto> AddTodoListAsync(
      AddTodoListRequestDto command, CancellationToken cancellationToken)
    {
      var todoListEntity = _entityDatabase.TodoLists.Add(command);

      await _entityDatabase.CommitAsync(cancellationToken);

      return new AddTodoListResponseDto
      {
        TodoListId = todoListEntity.TodoListId,
      };
    }

    /// <summary>Updates an existing todo list.</summary>
    /// <param name="command">An object that represents data to update a todo list.</param>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListEntity todoListEntity,
      CancellationToken cancellationToken)
    {
      _entityDatabase.TodoLists.Update(command, todoListEntity);

      return _entityDatabase.CommitAsync(cancellationToken);
    }

    /// <summary>Deletes an existing todo list.</summary>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task DeleteTodoListAsync(
      TodoListEntity todoListEntity, CancellationToken cancellationToken)
    {
      _entityDatabase.TodoLists.Delete(todoListEntity);

      return _entityDatabase.CommitAsync(cancellationToken);
    }
  }
}
