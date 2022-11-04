// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Dtos
{
  using AspNetRestApiSample.Indentities;

  /// <summary>Represents a base of a response for the get TODO list query.</summary>
  public abstract class GetTodoListTaskResponseDtoBase : TodoListTaskDtoBase, ITodoListTaskIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a TODO list task.</summary>
    public Guid TodoListTaskId { get; set; }

    /// <summary>Gets/sets an object that indicates if an TODO list is completed.</summary>
    public bool Completed { get; set; }
  }
}
