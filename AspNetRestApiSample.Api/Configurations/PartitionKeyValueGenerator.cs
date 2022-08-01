// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using AspNetRestApiSample.Api.Entities;

  public sealed class PartitionKeyValueGenerator : ValueGenerator
  {
    public override bool GeneratesTemporaryValues => false;

    protected override object? NextValue(EntityEntry entry)
    {
      var todoListEntity = (TodoListEntity)entry.Entity;

      return todoListEntity.Id;
    }
  }
}
