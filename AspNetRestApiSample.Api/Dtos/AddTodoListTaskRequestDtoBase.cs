// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Represents types of a TODO list task.</summary>
  public enum TodoListTaskType : byte
  {
    /// <summary>A value that indicates if a TODO list task is a day task.</summary>
    Day = 0,

    /// <summary>A value that indicates if a TODO list task is a period task.</summary>
    Period = 1,
  }

  /// <summary>Represents data to add a task to a todo list.</summary>
  public abstract class AddTodoListTaskRequestDtoBase : ITodoListIdentity
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
