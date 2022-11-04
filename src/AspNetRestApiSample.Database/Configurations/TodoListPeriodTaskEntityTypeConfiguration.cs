// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Database.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.Database.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class TodoListPeriodTaskEntityTypeConfiguration : IEntityTypeConfiguration<TodoListPeriodTaskEntity>
  {
    /// <summary>Configures the entity of type <see cref="AspNetRestApiSample.Database.Entities.TodoListPeriodTaskEntity"/>.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<TodoListPeriodTaskEntity> builder)
    {
      builder.HasBaseType<TodoListTaskEntityBase>();
      builder.HasPartitionKey(entity => entity.TodoListId);

      builder.Property(entity => entity.Begin).ToJsonProperty("begin");
      builder.Property(entity => entity.End).ToJsonProperty("end");
    }
  }
}
