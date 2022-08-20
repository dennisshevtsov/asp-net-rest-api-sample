// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;
  using AspNetRestApiSample.Api.Storage;

  /// <summary>Provides a simple API to a storage of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</summary>
  public sealed class TodoListTaskService : ITodoListTaskService
  {
    private readonly IEntityContainer _entityContainer;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Services.TodoListTaskService"/> class.</summary>
    /// <param name="entityContainer">An object that rrovides a simple API to query entities from the database and to commit changes.</param>
    public TodoListTaskService(IEntityContainer entityContainer)
    {
      _entityContainer = entityContainer ?? throw new ArgumentNullException(nameof(entityContainer));
    }

    /// <summary>Gets a attached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntity?> GetAttachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity
      => _entityContainer.TodoListTasks.GetAttachedAsync(
        query.TodoListTaskId, query.TodoListId, cancellationToken);

    /// <summary>Gets a detached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntity"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntity?> GetDetachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity
      => _entityContainer.TodoListTasks.GetDetachedAsync(
        query.TodoListTaskId, query.TodoListId, cancellationToken);

    /// <summary>Gets a todo list task response DTO.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public GetTodoListTaskResponseDto GetTodoListTask(TodoListTaskEntity todoListTaskEntity)
      => new GetTodoListTaskResponseDto
      {
        TodoListTaskId = todoListTaskEntity.Id,
        TodoListId = todoListTaskEntity.TodoListId,
        Title = todoListTaskEntity.Title,
        Description = todoListTaskEntity.Description,
      };

    /// <summary>Gets a collection of TODO list tasks.</summary>
    /// <param name="query">An object that represents conditions to query TODO list tasks.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<SearchTodoListTasksRecordResponseDto[]> SearchTodoListTasksAsync(
      SearchTodoListTasksRequestDto query, CancellationToken cancellationToken)
    {
      var todoListTaskEntityCollection =
        await _entityContainer.TodoListTasks.GetDetachedTodoListTasksAsync(
          query.TodoListId, cancellationToken);

      var searchTodoListTasksRecordResponseDtoCollection =
        new SearchTodoListTasksRecordResponseDto[todoListTaskEntityCollection.Length];

      for (int i = 0; i < todoListTaskEntityCollection.Length; ++i)
      {
        var todoListTaskEntity = todoListTaskEntityCollection[i];

        searchTodoListTasksRecordResponseDtoCollection[i] = new SearchTodoListTasksRecordResponseDto
        {
          TodoListId = todoListTaskEntity.TodoListId,
          TodoListTaskId = todoListTaskEntity.Id,
          Title = todoListTaskEntity.Title,
          Description = todoListTaskEntity.Description,
        };
      }

      return searchTodoListTasksRecordResponseDtoCollection;
    }

    /// <summary>Adds a new task for a TODO list.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<AddTodoListTaskResponseDto> AddTodoListTaskAsync(
      AddTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      var todoListTaskEntity = _entityContainer.TodoListTasks.Add(command);

      await _entityContainer.CommitAsync(cancellationToken);

      return new AddTodoListTaskResponseDto
      {
        TodoListId = todoListTaskEntity.TodoListId,
        TodoListTaskId = todoListTaskEntity.Id,
      };
    }

    /// <summary>Updates an existing TODO list task.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task UpdateTodoListTaskAsync(
      UpdateTodoListTaskRequestDto command,
      TodoListTaskEntity todoListTaskEntity,
      CancellationToken cancellationToken)
    {
      _entityContainer.TodoListTasks.Update(command, todoListTaskEntity);

      await _entityContainer.CommitAsync(cancellationToken);
    }

    /// <summary>Deletes a task from a TODO list.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task DeleteTodoListTaskAsync(TodoListTaskEntity todoListTaskEntity, CancellationToken cancellationToken)
    {
      _entityContainer.TodoListTasks.Delete(todoListTaskEntity);

      await _entityContainer.CommitAsync(cancellationToken);
    }

    /// <summary>Makes a task completed.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CompleteTodoListTaskAsync(TodoListTaskEntity todoListTaskEntity, CancellationToken cancellationToken)
    {
      if (todoListTaskEntity.Completed)
      {
        return Task.CompletedTask;
      }

      todoListTaskEntity.Completed = true;

      return _entityContainer.CommitAsync(cancellationToken);
    }

    /// <summary>Makes a task uncompleted.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UncompleteTodoListTaskAsync(TodoListTaskEntity todoListTaskEntity, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
