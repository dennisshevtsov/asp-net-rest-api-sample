// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Dtos
{
  using AspNetRestApiSample.Indentities;

  /// <summary>Represents base data to update todo list task.</summary>
  public abstract class UpdateTodoListTaskRequestDtoBase : TodoListTaskDtoBase, ITodoListTaskIdentity, IRequestDto
  {
    /// <summary>Gets/sets an object that represents an ID of a todo list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
