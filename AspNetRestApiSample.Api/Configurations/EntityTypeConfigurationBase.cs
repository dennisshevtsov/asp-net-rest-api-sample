// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.Api.Entities;

  public abstract class EntityTypeConfigurationBase<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
  {
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.ToContainer("todo-list-container");

      builder.HasKey(entity => entity.Id);
      builder.HasPartitionKey(entity => entity.TodoListId);

      builder.Property(entity => entity.Id).ToJsonProperty("id");
      builder.Property(entity => entity.TodoListId).ToJsonProperty("todoListId");
    }
  }
}
