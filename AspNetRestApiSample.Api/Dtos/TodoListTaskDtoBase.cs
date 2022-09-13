// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Represents a base of a TODO list task.</summary>
  public abstract class TodoListTaskDtoBase : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets an object that represents a title of a todo list task.</summary>
    public string? Title { get; set; }

    /// <summary>Gets/sets an object that represents a description of a todo list task.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that represents a type of a TODO list task.</summary>
    public TodoListTaskType Type { get; set; }
  }
}
