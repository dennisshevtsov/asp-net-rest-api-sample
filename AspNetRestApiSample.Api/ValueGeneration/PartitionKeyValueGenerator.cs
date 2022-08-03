// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.ValueGeneration
{
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using AspNetRestApiSample.Api.Entities;

  public sealed class PartitionKeyValueGenerator : ValueGenerator
  {
    public override bool GeneratesTemporaryValues => false;

    protected override object? NextValue(EntityEntry entry)
    {
      if (entry.Entity is TodoListEntity todoListEntity)
      {
        return todoListEntity.Id;
      }

      if (entry.Entity is TodoListTaskEntity todoListTaskEntity)
      {
        if (todoListTaskEntity.TodoListId != default)
        {
          return todoListTaskEntity.TodoListId;
        }

        if (todoListTaskEntity.TodoList != null)
        {
          return todoListTaskEntity.TodoList.Id;
        }

        throw new InvalidOperationException("No provided todo list for a task.");
      }

      throw new InvalidOperationException("Not supported type of entity.");
    }
  }
}
