// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListEntityConfiguration : IEntityTypeConfiguration<TodoListEntity>
  {
    public void Configure(EntityTypeBuilder<TodoListEntity> builder)
    {
      builder.ToContainer("todo-list-container");
      builder.HasNoDiscriminator();

      builder.HasKey(entity => entity.TodoListId);
      builder.HasPartitionKey(entity => entity.UserId);

      builder.Property(entity => entity.TodoListId).ToJsonProperty("id");
      builder.Property(entity => entity.Title).ToJsonProperty("title");
      builder.Property(entity => entity.Description).ToJsonProperty("description");
      builder.Property(entity => entity.UserId).ToJsonProperty("userId");
    }
  }
}
