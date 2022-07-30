// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListEntityTypeConfiguration
    : EntityTypeConfigurationBase<TodoListEntity>
  {
    public override void Configure(EntityTypeBuilder<TodoListEntity> builder)
    {
      base.Configure(builder);

      builder.Property(entity => entity.Title).ToJsonProperty("title");
      builder.Property(entity => entity.Description).ToJsonProperty("description");
    }
  }
}
