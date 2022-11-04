// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.ValueGeneration
{
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using AspNetRestApiSample.Api.Entities;

  /// <summary>Generates values for properties when an entity is added to a context.</summary>
  public sealed class PartitionKeyValueGenerator : ValueGenerator
  {
    /// <summary>Gets a value indicating whether the values generated are temporary or are permanent.</summary>
    public override bool GeneratesTemporaryValues => false;

    /// <summary>Gets a value to be assigned to a property.</summary>
    /// <param name="entry">The change tracking entry of the entity for which the value is being generated.</param>
    /// <returns>The generated value.</returns>
    protected override object? NextValue(EntityEntry entry)
    {
      if (entry.Entity is TodoListEntity todoListEntity)
      {
        return todoListEntity.Id;
      }

      if (entry.Entity is TodoListTaskEntityBase todoListTaskEntity)
      {
        if (todoListTaskEntity.TodoListId != default)
        {
          return todoListTaskEntity.TodoListId;
        }

        if (todoListTaskEntity.TodoList != null &&
            todoListTaskEntity.TodoList.Id != default)
        {
          return todoListTaskEntity.TodoList.Id;
        }

        throw new InvalidOperationException("No provided todo list for a task.");
      }

      throw new InvalidOperationException("Not supported type of entity.");
    }
  }
}
