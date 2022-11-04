// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Storage
{
  using AspNetRestApiSample.Entities;

  /// <summary>Provides a simple API to an entity collection in a database.</summary>
  public interface ITodoListEntityCollection : IEntityCollection<TodoListEntity>
  {
    /// <summary>Gets a collection of TODO lists.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TodoListEntity[]> GetDetachedTodoListsAsync(CancellationToken cancellationToken);
  }
}
