// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;

  public sealed class TodoListService : ITodoListService
  {
    private readonly DbContext _dbContext;

    public TodoListService(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<TodoListEntity?> GetDetachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken)
      => _dbContext.Set<TodoListEntity>()
                   .AsNoTracking()
                   .Where(entity => entity.TodoListId == query.TodoListId)
                   .FirstOrDefaultAsync(cancellationToken);

    public Task<TodoListEntity?> GetAttachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken)
      => _dbContext.Set<TodoListEntity>()
                   .Where(entity => entity.TodoListId == query.TodoListId)
                   .FirstOrDefaultAsync(cancellationToken);

    public GetTodoListResponseDto GetTodoList(TodoListEntity todoListEntity)
      => new GetTodoListResponseDto
      {
        TodoListId = todoListEntity.TodoListId,
        Title = todoListEntity.Title,
        Description = todoListEntity.Description,
      };

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

    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListEntity todoListEntity,
      CancellationToken cancellationToken)
    {
      _dbContext.Entry(todoListEntity)
                .CurrentValues.SetValues(command);

      return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTodoListAsync(
      DeleteTodoListRequestDto command, CancellationToken cancellationToken)
    {
      var todoListEntity = await GetAttachedTodoListAsync(command, cancellationToken);

      if (todoListEntity != null)
      {
        _dbContext.Entry(todoListEntity).State = EntityState.Deleted;

        await _dbContext.SaveChangesAsync(cancellationToken);
      }
    }
  }
}
