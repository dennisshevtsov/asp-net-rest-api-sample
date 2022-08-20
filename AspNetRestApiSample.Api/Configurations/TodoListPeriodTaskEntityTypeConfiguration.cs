// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.Api.Entities;

  public class TodoListPeriodTaskEntityTypeConfiguration : IEntityTypeConfiguration<TodoListPeriodTaskEntity>
  {
    public void Configure(EntityTypeBuilder<TodoListPeriodTaskEntity> builder)
    {
      builder.HasBaseType<TodoListTaskEntityBase>();
    }
  }
}
