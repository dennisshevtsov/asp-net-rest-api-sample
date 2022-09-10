﻿using AspNetRestApiSample.Api.Indentities;

namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents a base data of a TODO list task.</summary>
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
