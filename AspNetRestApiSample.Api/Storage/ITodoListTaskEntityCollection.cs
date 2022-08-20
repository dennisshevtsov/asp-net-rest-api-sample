﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using AspNetRestApiSample.Api.Entities;

  public interface ITodoListTaskEntityCollection : IEntityCollection<TodoListTaskEntityBase>
  {
    public Task<TodoListTaskEntityBase[]> GetDetachedTodoListTasksAsync(
      Guid todoListId, CancellationToken cancellationToken);
  }
}
