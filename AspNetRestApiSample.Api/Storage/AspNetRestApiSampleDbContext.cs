// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Configurations;

  public sealed class AspNetRestApiSampleDbContext : DbContext
  {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new TodoListEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new TodoListTaskEntityTypeConfiguration());
    }
  }
}
