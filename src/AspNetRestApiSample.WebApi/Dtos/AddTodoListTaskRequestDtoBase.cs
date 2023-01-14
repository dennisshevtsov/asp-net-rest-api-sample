// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.WebApi.Dtos
{
  using System.ComponentModel.DataAnnotations;

  using AspNetRestApiSample.ApplicationCore.Dtos;

  /// <summary>Represents data to add a task to a todo list.</summary>
  public abstract class AddTodoListTaskRequestDtoBase : IAddTodoListTaskRequestDto
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets an object that represents a title of a todo list task.</summary>
    [Required]
    public string? Title { get; set; }

    /// <summary>Gets/sets an object that represents a description of a todo list task.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that represents a type of a TODO list task.</summary>
    public TodoListTaskType Type { get; set; }
  }
}
