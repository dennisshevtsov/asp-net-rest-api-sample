// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Infrascruture
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.ApplicationCore.Database;
  using AspNetRestApiSample.ApplicationCore.Entities;

  /// <summary>Provides a simple API to an entity collection in a database.</summary>
  public sealed class TodoListTaskEntityCollection : EntityCollectionBase<TodoListTaskEntityBase>, ITodoListTaskEntityCollection
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.ApplicationCore.Database.TodoListTaskEntityCollection"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public TodoListTaskEntityCollection(DbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>Gets a collection of detached entities that satisfy defined conditions.</summary>
    /// <param name="todoListId">An object that represents an ID of a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase[]> GetDetachedTodoListTasksAsync(
      Guid todoListId, CancellationToken cancellationToken)
      => AsQueryable().AsNoTracking()
                      .WithPartitionKey(todoListId.ToString())
                      .ToArrayAsync(cancellationToken);
  }
}
