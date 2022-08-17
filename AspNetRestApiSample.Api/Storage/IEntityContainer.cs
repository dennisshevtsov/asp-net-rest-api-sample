// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  /// <summary>Provides a simple API to query entities from the database and to commit changes.</summary>
  public interface IEntityContainer
  {
    public ITodoListEntityCollection TodoLists { get; }

    public ITodoListTaskEntityCollection TodoListTasks { get; }

    public Task CommitAsync(CancellationToken cancellationToken);
  }
}
