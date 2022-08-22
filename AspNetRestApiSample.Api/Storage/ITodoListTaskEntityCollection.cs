// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using AspNetRestApiSample.Api.Entities;

  /// <summary>Provides a simple API to an entity collection in a database.</summary>
  public interface ITodoListTaskEntityCollection : IEntityCollection<TodoListTaskEntityBase>
  {
    /// <summary>Gets a collection of detached entities that satisfy defined conditions.</summary>
    /// <param name="todoListId">An object that represents an ID of a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListTaskEntityBase[]> GetDetachedTodoListTasksAsync(
      Guid todoListId, CancellationToken cancellationToken);
  }
}
