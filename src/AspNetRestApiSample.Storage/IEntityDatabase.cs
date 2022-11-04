// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Storage
{
  /// <summary>Provides a simple API to a database.</summary>
  public interface IEntityDatabase
  {
    /// <summary>Gets an object that provides a simple API to an entity collection in a database.</summary>
    public ITodoListEntityCollection TodoLists { get; }

    /// <summary>Gets an object that provides a simple API to an entity collection in a database.</summary>
    public ITodoListTaskEntityCollection TodoListTasks { get; }

    /// <summary>Commits all tracking changes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CommitAsync(CancellationToken cancellationToken);
  }
}
