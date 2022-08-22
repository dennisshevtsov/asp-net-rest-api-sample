// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  /// <summary>Provides a simple API to an entity collection in a database.</summary>
  public sealed class TodoListEntityCollection : EntityCollectionBase<TodoListEntity>, ITodoListEntityCollection
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Storage.TodoListEntityCollection"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    public TodoListEntityCollection(DbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>Gets a collection of TODO lists.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity[]> GetDetachedTodoListsAsync(CancellationToken cancellationToken)
      => AsQueryable().AsNoTracking().ToArrayAsync(cancellationToken);
  }
}
