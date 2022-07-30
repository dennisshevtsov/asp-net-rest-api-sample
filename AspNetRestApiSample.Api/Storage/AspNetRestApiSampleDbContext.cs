// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Configurations;

  /// <summary>Represents a session with the database and can be used to query and save instances of your entities.</summary>
  public sealed class AspNetRestApiSampleDbContext : DbContext
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Storage.AspNetRestApiSampleDbContext"/> class.</summary>
    /// <param name="options">An object that represents the options to be used by a <see cref="DbContext" />.</param>
    public AspNetRestApiSampleDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>Configure the model that was discovered by convention from the entity types.</summary>
    /// <param name="modelBuilder">Provides a simple API surface for configuring a <see cref="IMutableModel" /> that defines the shape of your entities, the relationships between them, and how they map to the database.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new TodoListEntityTypeConfiguration());
      modelBuilder.ApplyConfiguration(new TodoListTaskEntityTypeConfiguration());
    }
  }
}
