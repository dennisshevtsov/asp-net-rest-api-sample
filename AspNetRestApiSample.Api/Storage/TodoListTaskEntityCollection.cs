// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListTaskEntityCollection : EntityCollectionBase<TodoListTaskEntityBase>, ITodoListTaskEntityCollection
  {
    public TodoListTaskEntityCollection(DbContext dbContext) : base(dbContext)
    {
    }

    public Task<TodoListTaskEntityBase[]> GetDetachedTodoListTasksAsync(
      Guid todoListId, CancellationToken cancellationToken)
      => AsQueryable().AsNoTracking()
                      .WithPartitionKey(todoListId.ToString())
                      .ToArrayAsync(cancellationToken);
  }
}
