// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  public interface IEntityContainer
  {
    public ITodoListEntityCollection TodoLists { get; }

    public ITodoListTaskEntityCollection TodoListTasks { get; }

    public Task CommitAsync(CancellationToken cancellationToken);
  }
}
