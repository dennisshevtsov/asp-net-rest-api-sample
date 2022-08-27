// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;
  using AspNetRestApiSample.Api.Storage;

  /// <summary>Provides a simple API to a storage of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntityBase"/> class.</summary>
  public sealed class TodoListTaskService : ITodoListTaskService
  {
    private readonly IEntityDatabase _entityDatabase;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Services.TodoListTaskService"/> class.</summary>
    /// <param name="entityDatabase">An object that provides a simple API to a database.</param>
    public TodoListTaskService(IEntityDatabase entityDatabase)
    {
      _entityDatabase = entityDatabase ?? throw new ArgumentNullException(nameof(entityDatabase));
    }

    /// <summary>Gets a attached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntityBase"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase?> GetAttachedTodoListTaskEntityAsync<TQuery>(
      TQuery query, CancellationToken cancellationToken)
      where TQuery : ITodoListIdentity, ITodoListTaskIdentity
      => _entityDatabase.TodoListTasks.GetAttachedAsync(
        query.TodoListTaskId, query.TodoListId, cancellationToken);

    /// <summary>Gets a detached todo list task entity.</summary>
    /// <typeparam name="TQuery">A type of a query.</typeparam>
    /// <param name="query">An object that represents conditions to query an instance of the <see cref="AspNetRestApiSample.Api.Entities.TodoListTaskEntityBase"/> class.</param>
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
    {
      GetTodoListTaskResponseDtoBase responseDto;

      if (todoListTaskEntity is TodoListDayTaskEntity todoListDayTaskEntity)
      {
        responseDto = new GetTodoListDayTaskResponseDto
        {
          Date = todoListDayTaskEntity.Date,
        };
      }
      else if (todoListTaskEntity is TodoListPeriodTaskEntity todoListPeriodTaskEntity)
      {
        responseDto = new GetTodoListPeriodTaskResponseDto
        {
          Beginning = todoListPeriodTaskEntity.Beginning,
          End = todoListPeriodTaskEntity.End,
        };
      }
      else
      {
        throw new NotSupportedException($"Type {todoListTaskEntity.GetType()} is not supported.");
      }

      responseDto.TodoListTaskId = todoListTaskEntity.Id;
      responseDto.TodoListId = todoListTaskEntity.TodoListId;
      responseDto.Title = todoListTaskEntity.Title;
      responseDto.Description = todoListTaskEntity.Description;
      responseDto.Completed = todoListTaskEntity.Completed;

      return responseDto;
    }

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
        new SearchTodoListTasksRecordResponseDtoBase[todoListTaskEntityCollection.Length];

      for (int i = 0; i < todoListTaskEntityCollection.Length; ++i)
      {
        searchTodoListTasksRecordResponseDtoCollection[i] =
          TodoListTaskService.Convert(todoListTaskEntityCollection[i]);
      }

      return searchTodoListTasksRecordResponseDtoCollection;
    }

    /// <summary>Adds a new task for a TODO list.</summary>
    /// <param name="command">An object that represents data to add a task to a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<AddTodoListTaskResponseDto> AddTodoListTaskAsync(
      AddTodoListTaskRequestDtoBase command, CancellationToken cancellationToken)
    {
      TodoListTaskEntityBase todoListTaskEntity;

      if (command is AddTodoListDayTaskRequestDto)
      {
        todoListTaskEntity = new TodoListDayTaskEntity();
      }
      else if (command is AddTodoListPeriodTaskRequestDto)
      {
        todoListTaskEntity = new TodoListPeriodTaskEntity();
      }
      else
      {
        throw new NotSupportedException();
      }

      _entityDatabase.TodoListTasks.Update(command, todoListTaskEntity);
      await _entityDatabase.CommitAsync(cancellationToken);

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
      UpdateTodoListTaskRequestDtoBase command,
      TodoListTaskEntityBase todoListTaskEntity,
      CancellationToken cancellationToken)
    {
      _entityDatabase.TodoListTasks.Update(command, todoListTaskEntity);

      await _entityDatabase.CommitAsync(cancellationToken);
    }

    /// <summary>Deletes a task from a TODO list.</summary>
    /// <param name="todoListTaskEntity">An object that represents data of a todo list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task DeleteTodoListTaskAsync(TodoListTaskEntityBase todoListTaskEntity, CancellationToken cancellationToken)
    {
      _entityDatabase.TodoListTasks.Delete(todoListTaskEntity);

      await _entityDatabase.CommitAsync(cancellationToken);
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


    private static SearchTodoListTasksRecordResponseDtoBase Convert(TodoListTaskEntityBase todoListTaskEntity)
    {
      SearchTodoListTasksRecordResponseDtoBase responseDto = null;

      if (todoListTaskEntity is TodoListDayTaskEntity todoListDayTaskEntity)
      {
        responseDto = new SearchTodoListTasksDayRecordResponseDto
        {
          Date = todoListDayTaskEntity.Date,
        };
      }
      else if (todoListTaskEntity is TodoListPeriodTaskEntity todoListPeriodTaskEntity)
      {
        responseDto = new SearchTodoListTasksPeriodRecordResponseDto
        {
          Beginning = todoListPeriodTaskEntity.Beginning,
          End = todoListPeriodTaskEntity.End,
        };
      }
      else
      {
        throw new NotSupportedException($"Type {todoListTaskEntity.GetType()} is not supported.");
      }

      responseDto.TodoListId = todoListTaskEntity.TodoListId;
      responseDto.TodoListTaskId = todoListTaskEntity.Id;
      responseDto.Title = todoListTaskEntity.Title;
      responseDto.Description = todoListTaskEntity.Description;
      responseDto.Completed = todoListTaskEntity.Completed;

      return responseDto;
    }

  }
}
