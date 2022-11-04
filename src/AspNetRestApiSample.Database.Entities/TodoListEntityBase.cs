// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Database.Entities
{
  using AspNetRestApiSample.Indentities;

  /// <summary>Represents a base of an entity.</summary>
  public abstract class TodoListEntityBase : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that represents a primary key of an entity.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets/sets an object that represents a partition key of an entity.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets an object that represents a title of a todo list.</summary>
    public string? Title { get; set; }

    /// <summary>Gets/sets an object that represents a description of a todo list.</summary>
    public string? Description { get; set; }
  }
}
