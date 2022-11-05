// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Services
{
  using AutoMapper;

  using AspNetRestApiSample.ApplicationCore.Dtos;
  using AspNetRestApiSample.ApplicationCore.Entities;
  using AspNetRestApiSample.ApplicationCore.Indentities;
  using AspNetRestApiSample.ApplicationCore.Database;

  /// <summary>Provides a simple API to a storage of the <see cref="AspNetRestApiSample.ApplicationCore.Entities.TodoListTaskEntityBase"/> class.</summary>
  public sealed class TodoListTaskService : ITodoListTaskService
  {
    private readonly IMapper _mapper;
    private readonly IEntityDatabase _entityDatabase;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.ApplicationCore.Services.TodoListTaskService"/> class.</summary>
    /// <param name="mapper">An object that provides a simple API to populate one object from another.</param>
    /// <param name="entityDatabase">An object that provides a simple API to a database.</param>
    public TodoListTaskService(
      IMapper mapper,
      IEntityDatabase entityDatabase)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _entityDatabase = entityDatabase ?? throw new ArgumentNullException(nameof(entityDatabase));
    }

    /// <summary>Gets a attached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.ApplicationCore.Entities.TodoListTaskEntityBase"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase?> GetAttachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity
      => _entityDatabase.TodoListTasks.GetAttachedAsync(
        query.TodoListTaskId, query.TodoListId, cancellationToken);

    /// <summary>Gets a detached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.ApplicationCore.Entities.TodoListTaskEntityBase"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase?> GetDetachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity
      => _entityDatabase.TodoListTasks.GetDetachedAsync(
        query.TodoListTaskId, query.TodoListId, cancellationToken);

    /// <summary>Gets a todo list task response DTO.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public GetTodoListTaskResponseDtoBase GetTodoListTask(TodoListTaskEntityBase todoListTaskEntity)
      => _mapper.Map<GetTodoListTaskResponseDtoBase>(todoListTaskEntity);

    /// <summary>Gets a collection of TODO list tasks.</summary>
    /// <param name="query">An object that represents conditions to query TODO list tasks.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<SearchTodoListTasksRecordResponseDtoBase[]> SearchTodoListTasksAsync(
      SearchTodoListTasksRequestDto query, CancellationToken cancellationToken)
    {
      var todoListTaskEntityCollection =
        await _entityDatabase.TodoListTasks.GetDetachedTodoListTasksAsync(
          query.TodoListId, cancellationToken);

      var searchTodoListTasksRecordResponseDtoCollection =
        _mapper.Map<SearchTodoListTasksRecordResponseDtoBase[]>(todoListTaskEntityCollection);

      return searchTodoListTasksRecordResponseDtoCollection;
    }

    /// <summary>Adds a new task for a TODO list.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<AddTodoListTaskResponseDto> AddTodoListTaskAsync(
      AddTodoListTaskRequestDtoBase command, CancellationToken cancellationToken)
    {
      var todoListTaskEntity = _mapper.Map<TodoListTaskEntityBase>(command);

      _entityDatabase.TodoListTasks.Attache(todoListTaskEntity);
      await _entityDatabase.CommitAsync(cancellationToken);

      return _mapper.Map<AddTodoListTaskResponseDto>(todoListTaskEntity);
    }

    /// <summary>Updates an existing TODO list task.</summary>
    /// <param name="command">An object that represents data to update todo list task.</param>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task UpdateTodoListTaskAsync(
      UpdateTodoListTaskRequestDtoBase command,
      TodoListTaskEntityBase todoListTaskEntity,
      CancellationToken cancellationToken)
    {
      if (command is UpdateTodoListDayTaskRequestDto    && todoListTaskEntity is TodoListDayTaskEntity ||
          command is UpdateTodoListPeriodTaskRequestDto && todoListTaskEntity is TodoListPeriodTaskEntity)
      {
        _mapper.Map(command, todoListTaskEntity);
      }
      else
      {
        var newTodoListTaskEntity = _mapper.Map<TodoListTaskEntityBase>(command);

        _entityDatabase.TodoListTasks.Attache(newTodoListTaskEntity);
      }

      await _entityDatabase.CommitAsync(cancellationToken);
    }

    /// <summary>Deletes a task from a TODO list.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task DeleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken)
    {
      _entityDatabase.TodoListTasks.Delete(todoListTaskEntity);

      return _entityDatabase.CommitAsync(cancellationToken);
    }

    /// <summary>Makes a task completed.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CompleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken)
    {
      if (!todoListTaskEntity.Completed)
      {
        todoListTaskEntity.Completed = true;

        return _entityDatabase.CommitAsync(cancellationToken);
      }

      return Task.CompletedTask;
    }

    /// <summary>Makes a task uncompleted.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UncompleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken)
    {
      if (todoListTaskEntity.Completed)
      {
        todoListTaskEntity.Completed = false;

        return _entityDatabase.CommitAsync(cancellationToken);
      }

      return Task.CompletedTask;
    }
  }
}
