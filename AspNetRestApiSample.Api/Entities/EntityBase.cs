// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Entities
{
  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Represents a base of an entity.</summary>
  public abstract class EntityBase : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that represents a primary key of an entity.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets/sets an object that represents a partition key of an entity.</summary>
    public Guid TodoListId { get; set; }
  }
}
