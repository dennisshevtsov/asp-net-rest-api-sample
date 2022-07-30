// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Provides a simple API to a storage of instances of the <see cref="AspNetRestApiSample.Api.Entities.TodoListEntity"/> class.</summary>
  public sealed class TodoListService : ITodoListService
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Services.TodoListService"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public TodoListService(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets a detached todo list entity.</summary>
    /// <param name="query">An object that represents an identity of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity?> GetDetachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken)
      => _dbContext.Set<TodoListEntity>()
                   .AsNoTracking()
                   .Where(entity => entity.TodoListId == query.TodoListId)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <summary>Gets an attached todo list entity.</summary>
    /// <param name="query">An object that represents an identity of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity?> GetAttachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken)
      => _dbContext.Set<TodoListEntity>()
                   .Where(entity => entity.TodoListId == query.TodoListId)
                   .FirstOrDefaultAsync(cancellationToken);

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
    public Task<SearchTodoListsRecordResponseDto[]> SearchTodoListsAsync(
      SearchTodoListsRequestDto query, CancellationToken cancellationToken)
      => _dbContext.Set<TodoListEntity>()
                   .AsNoTracking()
                   .Select(entity => new SearchTodoListsRecordResponseDto
                   {
                     TodoListId = entity.TodoListId,
                     Title = entity.Title,
                     Description = entity.Description,
                   })
                   .ToArrayAsync(cancellationToken);

    /// <summary>Creates a new todo list.</summary>
    /// <param name="command">An object that represents data to create a new todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<AddTodoListResponseDto> AddTodoListAsync(
      AddTodoListRequestDto command, CancellationToken cancellationToken)
    {
      var todoListEntity = new TodoListEntity();

      _dbContext.Attach(todoListEntity)
                .CurrentValues.SetValues(command);

      await _dbContext.SaveChangesAsync(cancellationToken);

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
      _dbContext.Entry(todoListEntity)
                .CurrentValues.SetValues(command);

      return _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>Deletes an existing todo list.</summary>
    /// <param name="todoListEntity">An object that represents data of a todo list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task DeleteTodoListAsync(
      TodoListEntity todoListEntity, CancellationToken cancellationToken)
    {
      _dbContext.Entry(todoListEntity).State = EntityState.Deleted;

      await _dbContext.SaveChangesAsync(cancellationToken);
    }
  }
}
