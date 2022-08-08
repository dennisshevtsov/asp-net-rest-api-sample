// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.Api.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class TodoListEntityTypeConfiguration : TodoListEntityTypeConfigurationBase<TodoListEntity>
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Configurations.TodoListEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public TodoListEntityTypeConfiguration(string containerName) : base(containerName)
    {
    }

    /// <summary>Configures the entity of type <see cref="AspNetRestApiSample.Api.Entities.TodoListEntity"/>.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public override void Configure(EntityTypeBuilder<TodoListEntity> builder)
    {
      base.Configure(builder);

      builder.HasMany(entity => entity.Tasks)
             .WithOne(entity => entity.TodoList)
             .HasPrincipalKey(entity => entity.Id)
             .HasForeignKey(entity => entity.TodoListId);
    }
  }
}
