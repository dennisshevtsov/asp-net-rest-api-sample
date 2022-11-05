// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Infrascruture.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using AspNetRestApiSample.ApplicationCore.Entities;

  /// <summary>Allows configuration for an entity type.</summary>
  public sealed class TodoListTaskEntityTypeConfiguration : TodoListEntityTypeConfigurationBase<TodoListTaskEntityBase>
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.ApplicationCore.Database.Configurations.TodoListTaskEntityTypeConfiguration"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    public TodoListTaskEntityTypeConfiguration(string containerName) : base(containerName)
    {
    }

    /// <summary>Configures the entity of type <see cref="AspNetRestApiSample.ApplicationCore.Entities.TodoListTaskEntityBase"/>.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public override void Configure(EntityTypeBuilder<TodoListTaskEntityBase> builder)
    {
      base.Configure(builder);

      builder.Property(entity => entity.Completed).ToJsonProperty("completed");

      builder.HasOne(entity => entity.TodoList)
             .WithMany(entity => entity.Tasks)
             .HasPrincipalKey(entity => entity.Id)
             .HasForeignKey(entity => entity.TodoListId);
    }
  }
}
