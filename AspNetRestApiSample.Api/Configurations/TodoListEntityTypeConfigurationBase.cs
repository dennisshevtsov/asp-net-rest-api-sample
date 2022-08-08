// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Configurations
{
  using System;

  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.ValueGeneration;

  /// <summary>Allows configuration for an entity type.</summary>
  /// <typeparam name="TEntity">The entity type to be configured.</typeparam>
  public abstract class TodoListEntityTypeConfigurationBase<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : TodoListEntityBase
  {
    private const string DescriminatorPropertyName = "__type";

    private readonly string _containerName;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Configurations.TodoListEntityTypeConfigurationBase{TEntity}"/> class.</summary>
    /// <param name="containerName">An object that represents a name of a container.</param>
    protected TodoListEntityTypeConfigurationBase(string containerName)
    {
      _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
    }

    /// <summary>Configures the entity of type <typeparamref name="TEntity" />.</summary>
    /// <param name="builder">An object that provides a simple API to configure the entity type.</param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.ToContainer(_containerName);

      builder.HasKey(entity => entity.Id);
      builder.HasPartitionKey(entity => entity.TodoListId);

      builder.Property(typeof(string), TodoListEntityTypeConfigurationBase<TEntity>.DescriminatorPropertyName).HasValueGenerator<DescriminatorValueGenerator>();
      builder.HasDiscriminator(TodoListEntityTypeConfigurationBase<TEntity>.DescriminatorPropertyName, typeof(string));

      builder.Property(entity => entity.Id).ToJsonProperty("id").HasValueGenerator<GuidValueGenerator>();
      builder.Property(entity => entity.TodoListId).ToJsonProperty("todoListId").HasValueGenerator<PartitionKeyValueGenerator>();

      builder.Property(entity => entity.Title).ToJsonProperty("title");
      builder.Property(entity => entity.Description).ToJsonProperty("description");
    }
  }
}
