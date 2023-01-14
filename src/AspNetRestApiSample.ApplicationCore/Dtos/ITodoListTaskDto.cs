// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  using AspNetRestApiSample.ApplicationCore.Indentities;

  /// <summary>Represents a base of a TODO list task.</summary>
  public interface ITodoListTaskDto : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that represents a title of a todo list task.</summary>
    public string? Title { get; }

    /// <summary>Gets/sets an object that represents a description of a todo list task.</summary>
    public string? Description { get; }

    /// <summary>Gets/sets an object that represents a type of a TODO list task.</summary>
    public TodoListTaskType Type { get; }
  }
}
